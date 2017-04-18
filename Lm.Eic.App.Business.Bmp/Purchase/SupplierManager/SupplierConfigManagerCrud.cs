using System;
using System.Collections.Generic;
using System.Linq;
using Lm.Eic.App.DomainModel.Bpm.Purchase;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeExtension.Validation;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.App.DbAccess.Bpm.Repository.PurchaseRep.PurchaseSuppliesManagement;
using System.IO;

namespace Lm.Eic.App.Business.Bmp.Purchase.SupplierManager
{
    /// <summary>
    /// 供应商Curd管理工厂
    /// </summary>
    internal class SupplierCrudFactory
    {

        /// <summary>
        /// 供应商合格文件CRUD
        /// </summary>
        public static SupplierQualifiedCertificateCrud SupplierQualifiedCertificateCrud
        {
            get { return OBulider.BuildInstance<SupplierQualifiedCertificateCrud>(); }
        }
        /// <summary>
        /// 供应商信息
        /// </summary>
        public static SuppliersInfoCrud SuppliersInfoCrud
        {
            get { return OBulider.BuildInstance<SuppliersInfoCrud>(); }
        }
        /// <summary>
        /// 供应商季度审计考核表
        /// </summary>
        public static SuppliersSeasonAuditCrud SuppliersSeasonAuditCrud
        {
            get { return OBulider.BuildInstance<SuppliersSeasonAuditCrud>(); }
        }

        /// <summary>
        /// 季度审计实地辅导计划/执行
        /// </summary>
        public static SuppliersSeasonTutorCrud SuppliersSeasonTutorCrud
        {
            get { return OBulider.BuildInstance<SuppliersSeasonTutorCrud>(); }
        }

        /// <summary>
        /// 供应商自评复评明细表Crud
        /// </summary>
        public static SupplierGradeInfoCrud SupplierGradeInfoCrud
        {
            get { return OBulider.BuildInstance<SupplierGradeInfoCrud>(); }
        }
    }


    /// <summary>
    /// 供应商合格证书Curd
    /// </summary>
    public class SupplierQualifiedCertificateCrud : CrudBase<SupplierQualifiedCertificateModel, ISupplierQualifiedCertificateRepository>
    {
        public SupplierQualifiedCertificateCrud() : base(new SupplierQualifiedCertifcateRepository(), "供应商合格文件")
        { }


        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddSupplierQualifiedCertificate);
            this.AddOpItem(OpMode.UpDate, EidtSupplierQualifiedCertificate);
            this.AddOpItem(OpMode.Delete, DeleteSupplierQualifiedCertificate);

        }

        public OpResult StoreSupplierQualifiedCertificate(SupplierQualifiedCertificateModel model)
        {
            OpResult ReOpResult = OpResult.SetResult("采集数据模型不能为NULL", false);
            if (model == null) return ReOpResult;
            var oldmodel = this.GetOldQualifiedCertificateModelBy(model);
            if (oldmodel == null)
            {
                model.OpSign = OpMode.Add;//若不存在则直接添加
                return this.Store(model, true);
            }

            model.OpSign = OpMode.UpDate;
            model.Id_Key = oldmodel.Id_Key;
            //若不存在则直接添加
            return this.Store(model, true);

            //return OpResult.SetResult("文件重新上传成功！", true);
        }


        public OpResult DeleteFileSupplierQualifiedCertificate(SupplierQualifiedCertificateModel model, string siteRootPath)
        {
            OpResult ReOpResult = OpResult.SetResult("采集数据模型不能为NULL", false);
            if (model == null) return ReOpResult;
            var oldModel = this.GetOldQualifiedCertificateModelBy(model);
            if (oldModel == null) return OpResult.SetResult("不存在此数据", false);
            oldModel.OpSign = OpMode.Delete;
            ReOpResult = this.Store(oldModel, true);
            if (!ReOpResult.Result) return ReOpResult;
            //比对新旧文件是否一样,若不一样，则删除旧的文件
            oldModel.FilePath.DeleteExistFile(model.FilePath, siteRootPath);
            return ReOpResult;
        }

        private SupplierQualifiedCertificateModel GetOldQualifiedCertificateModelBy(SupplierQualifiedCertificateModel model)
        {
            try
            {
                if (model == null) return null;
                return irep.Entities.FirstOrDefault(e => e.SupplierId == model.SupplierId && e.EligibleCertificate == model.EligibleCertificate);
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception(ex.InnerException.Message);
            }
        }

        private OpResult DeleteSupplierQualifiedCertificate(SupplierQualifiedCertificateModel model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key, true).ToOpResult_Delete(OpContext);
        }

        private OpResult EidtSupplierQualifiedCertificate(SupplierQualifiedCertificateModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);

        }


        public OpResult AddSupplierQualifiedCertificate(SupplierQualifiedCertificateModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }

        /// <summary>
        /// / 添加供应商的合格文件记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult SavaSupplierEligible(SupplierQualifiedCertificateModel model)
        {
            try
            {
                model.OpSign = OpMode.Add;
                SetFixFieldValue(model);
                return irep.Insert(model).ToOpResult_Add(OpContext);
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }

        }
        /// <summary>
        /// 批量保存供应商的合格文件记录
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult SavaSupplierEligibleList(List<SupplierQualifiedCertificateModel> modelList)
        {

            try
            {
                DateTime date = DateTime.Now.ToDate();
                SetFixFieldValue(modelList, OpMode.Add, m =>
                {
                    m.OpDate = date;  //需要添加附加答条件
                });

                if (!modelList.IsNullOrEmpty())
                    return OpResult.SetResult("合格文件记录列表不能为空！ 保存失败");
                return irep.Insert(modelList).ToOpResult_Add(OpContext);
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }

        }
        /// <summary>
        /// 是否已经保存在证书
        /// </summary>
        /// <param name="CertificateFileName"></param>
        /// <returns></returns>
        public bool IsExistCertificateFileName(string CertificateFileName)
        {
            return irep.IsExist(e => e.CertificateFileName == CertificateFileName);
        }


        /// <summary>
        /// 获得供应商合格文件项目
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public List<SupplierQualifiedCertificateModel> GetQualifiedCertificateListBy(string supplierId)
        {
            try
            {
                return irep.Entities.Where(m => m.SupplierId == supplierId).ToList();
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
        }

    }
    /// <summary>
    /// 供应商信息Curd
    /// </summary>
    public class SuppliersInfoCrud : CrudBase<SupplierInfoModel, ISupplierInfoRepository>
    {

        public SuppliersInfoCrud()
            : base(new SupplierInfoRepository(), "供应商信息")
        { }

        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddSupplierInfo);
            this.AddOpItem(OpMode.Edit, EidtSupplierInfo);
            this.AddOpItem(OpMode.Delete, DeleteSupplierInfo);
        }

        public bool IsExistSupperid(string supplierId, out decimal findId_key)
        {
            if (irep.IsExist(e => e.SupplierId == supplierId))
            {
                findId_key = irep.Entities.Where(e => e.SupplierId == supplierId).ToList().FirstOrDefault().Id_Key;
                return true;
            }
            else
            { findId_key = 0; return false; }
        }

        /// <summary>
        /// 批量保存供应商信息
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult SavaSupplierInfoList(List<SupplierInfoModel> modelList)
        {
            try
            {
                DateTime date = DateTime.Now.ToDate();
                SetFixFieldValue(modelList, OpMode.Add, m =>
                {
                    m.OpDate = date;
                    //需要添加附加答条件
                });
                ///如查SupplierID号存在
                if (!modelList.IsNullOrEmpty())
                    return OpResult.SetResult("列表不能为空！ 保存失败");

                //return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt("修改完成");

                return irep.Insert(modelList).ToOpResult_Add(OpContext);
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
        }
        #region  Store
        /// <summary>
        /// 添加供应商信息
        /// </summary>
        /// <param name="model></param>
        /// <returns></returns>
        OpResult AddSupplierInfo(SupplierInfoModel model)
        {

            ///判断产品品号是否存在
            try
            {
                if (irep.IsExist(m => m.SupplierId == model.SupplierId))
                {
                    return OpResult.SetResult("此数据已存在！");
                }
                SetFixFieldValue(model);
                model.SupplierId = model.SupplierId.Trim();
                return irep.Insert(model).ToOpResult_Add(OpContext);
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }

        }
        internal OpResult InitSupplierInfo(SupplierInfoModel model)
        {
            try
            {
                SetFixFieldValue(model);
                model.SupplierId = model.SupplierId.Trim();
                model.OpSign = "init";
                return irep.Insert(model).ToOpResult_Add(OpContext);
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }

        }
        public OpResult EidtSupplierInfo(SupplierInfoModel model)
        {
            if (irep.IsExist(m => m.SupplierId == model.SupplierId))
            {
                if (model.OpSign == "editPurchaseType")
                    return irep.Update(u => u.SupplierId == model.SupplierId,
                        f => new SupplierInfoModel
                        {
                            SupplierProperty = model.SupplierProperty,
                            PurchaseType = model.PurchaseType,
                            OpSign = model.OpSign
                        }).ToOpResult_Eidt("修改供应商类别成功！");
                return irep.Update(m => m.SupplierId == model.SupplierId, model).ToOpResult_Eidt("修改成功");

            }
            else return OpResult.SetResult("此数据不存在！无法修改");


        }

        OpResult DeleteSupplierInfo(SupplierInfoModel model)
        {
            return irep.Delete(m => m.SupplierId == model.SupplierId).ToOpResult_Delete("删除成功");
        }
        #endregion
        /// <summary>
        /// 获取供应商信息
        /// </summary>
        /// <param name="supplierId">供应商ID</param>
        /// <returns></returns>
        public SupplierInfoModel GetSupplierInfoBy(string supplierId)
        {
            try
            {
                return irep.Entities.FirstOrDefault(m => m.SupplierId == supplierId);
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
        }
    }
    /// <summary>
    /// 供应商季度审查表Curd
    /// </summary>
    public class SuppliersSeasonAuditCrud : CrudBase<SupplierSeasonAuditModel, ISupplierSeasonAuditRepository>
    {
        public SuppliersSeasonAuditCrud()
            : base(new SupplierSeasonAuditRepository(), "供应商季度审计考核表")
        { }
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddSupplierSeasonAuditInfo);
            this.AddOpItem(OpMode.Edit, EditSupplierSeasonAuditInfo);
            this.AddOpItem(OpMode.Delete, DelteSupplierSeasonAuditInfo);
        }
        /// <summary>
        /// 得到限制总分内供应商信息
        /// </summary>
        /// <param name="seasonDateNum">季度</param>
        /// <param name="limitScore">限制的分数线</param>
        /// <returns></returns>
        public List<SupplierSeasonAuditModel> GetlimitScoreSupplierAuditInfo(string seasonDateNum, double limitScore)
        {
            return this.irep.Entities.Where(e => e.TotalCheckScore < limitScore && e.SeasonDateNum == seasonDateNum).ToList();
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="parameterKey"></param>
        /// <returns></returns>
        public SupplierSeasonAuditModel GetSupplierSeasonAuditInfo(string parameterKey)
        {
            var modelList = this.irep.Entities.Where(e => e.ParameterKey == parameterKey).ToList();
            if (modelList == null || modelList.Count == 0) return null;
            return modelList[0];
        }

        OpResult AddSupplierSeasonAuditInfo(SupplierSeasonAuditModel model)
        {
            model.ParameterKey = model.SupplierId.Trim() + "&&" + model.SeasonDateNum;
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
        OpResult DelteSupplierSeasonAuditInfo(SupplierSeasonAuditModel model)
        {
            return irep.Delete(model).ToOpResult_Add(OpContext);
        }

        OpResult EditSupplierSeasonAuditInfo(SupplierSeasonAuditModel model)
        {
            return irep.Update(e => e.ParameterKey == model.ParameterKey, model).ToOpResult_Add(OpContext); ;
        }

        public bool IsExist(string parameterKey)
        {
            return irep.IsExist(e => e.ParameterKey == parameterKey);
        }
    }

    /// <summary>
    /// 季度考核实地辅导计划/执行Crud
    /// </summary>

    public class SuppliersSeasonTutorCrud : CrudBase<SupplierSeasonTutorModel, ISupplierSeasonAuditTutorRepository>
    {
        public SuppliersSeasonTutorCrud() : base(new SupplierSeasonAuditTutorRepository(), "季度考核实地辅导计划/执行")
        { }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddSupplierSeasonAuditTutorInfo);
            this.AddOpItem(OpMode.Edit, EditSupplierSeasonAuditTutorInfo);
        }
        /// <summary>
        /// 通过parameterKey得到Model
        /// </summary>
        /// <param name="parameterKey"></param>
        /// <returns></returns>
        public SupplierSeasonTutorModel GetSupplierSeasonTutorModelBy(string parameterKey)
        {
            return irep.Entities.Where(e => e.ParameterKey == parameterKey).ToList().FirstOrDefault();
        }

        /// <summary>
        /// 添加季度辅导
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        OpResult AddSupplierSeasonAuditTutorInfo(SupplierSeasonTutorModel model)
        {
            model.ParameterKey = model.SupplierId.Trim() + "&&" + model.SeasonNum;
            model.YearMonth = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
        /// <summary>
        /// 编辑保存季度辅导
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        OpResult EditSupplierSeasonAuditTutorInfo(SupplierSeasonTutorModel model)
        {
            return irep.Update(e => e.ParameterKey == model.ParameterKey, model).ToOpResult_Add(OpContext); ;
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="parameterKey"></param>
        /// <returns></returns>
        public bool IsExist(string parameterKey)
        {
            return irep.IsExist(e => e.ParameterKey == parameterKey);
        }
    }


    /// <summary>
    /// 供应商自评复评明细表 Crud
    /// </summary>
    public class SupplierGradeInfoCrud : CrudBase<SupplierGradeInfoModel, ISupplierGradeInfoRepository>
    {
        public SupplierGradeInfoCrud() : base(new SupplierGradeInfoRepository(), "供应商自评复评明细表 ")
        { }
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddSupplierGradeInfo);
            this.AddOpItem(OpMode.Edit, EditSupplierGradeInfo);
        }


        OpResult AddSupplierGradeInfo(SupplierGradeInfoModel entity)
        {
            entity.LastPurchaseDate = DateTime.Now.Date;
            entity.PurchaseType = "主要";
            entity.PurchaseMaterial = "主要111";
            entity.SupplierProperty = "mmmm";

            return irep.Insert(entity).ToOpResult_Add(OpContext);
        }

        public List<SupplierGradeInfoModel> GetPurSupGradeInfoBy(string gradeYear)
        {
            return irep.Entities.Where(e => e.GradeYear == gradeYear).ToList();
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        OpResult EditSupplierGradeInfo(SupplierGradeInfoModel entity)
        {
            return irep.Update(e => e.SupplierId == entity.SupplierId, entity).ToOpResult_Add(OpContext); ;
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="parameterKey"></param>
        /// <returns></returns>
        public bool IsExist(string parameterKey)
        {
            return irep.IsExist(e => e.ParameterKey == parameterKey);
        }
    }

}
