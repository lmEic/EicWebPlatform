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

        #region crud


        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, Add);
            this.AddOpItem(OpMode.Edit, Eidt);
            this.AddOpItem(OpMode.Delete, Delete);
        }

        private OpResult Eidt(SupplierQualifiedCertificateModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);

        }


        private OpResult Add(SupplierQualifiedCertificateModel model)
        {
            if (!irep.IsExist(e => e.SupplierId == model.SupplierId && e.EligibleCertificate == model.EligibleCertificate))
                return irep.Insert(model).ToOpResult_Add(OpContext);
            return this.Eidt(model);

        }

        private OpResult Delete(SupplierQualifiedCertificateModel model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key, true).ToOpResult_Delete(OpContext);
        }


        #endregion

        public OpResult StoreSupplierQualifiedCertificate(SupplierQualifiedCertificateModel model)
        {
            OpResult ReOpResult = OpResult.SetErrorResult("供应商合格证书数据模型不能为NULL");
            if (model == null) return ReOpResult;
            var oldmodel = this.GetQualifiedCertificateModelBy(model.SupplierId, model.EligibleCertificate);
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


        internal OpResult DeleteSupplierQualifiedCertificateFile(SupplierQualifiedCertificateModel model, string siteRootPath)
        {
            OpResult ReOpResult = OpResult.SetErrorResult("删除出错");
            var oldModel = this.GetQualifiedCertificateModelBy(model.SupplierId, model.EligibleCertificate);
            if (oldModel == null) return OpResult.SetErrorResult("不存在此数据");
            oldModel.OpSign = OpMode.Delete;
            ReOpResult = this.Store(oldModel, true);
            if (!ReOpResult.Result) return ReOpResult;
            //则删除旧的文件
            oldModel.FilePath.DeleteExistFile(siteRootPath);
            return ReOpResult;
        }
        /// <summary>
        /// 上传证书文件
        /// </summary>
        /// <param name="model"></param>
        /// <param name="siteRootPath"></param>
        /// <returns></returns>
        internal OpResult UpdateQualifiedCertificateFile(SupplierQualifiedCertificateModel model, string siteRootPath)
        {
            OpResult ReOpResult = OpResult.SetErrorResult("上传出错");
            var oldModel = this.GetQualifiedCertificateModelBy(model.SupplierId, model.EligibleCertificate);
            if (oldModel == null)
            {
                model.OpSign = OpMode.Add;
                return this.Store(model, true);
            }
            model.OpSign = OpMode.Edit;
            model.Id_Key = oldModel.Id_Key;
            ReOpResult = this.Store(model, true);
            if (!ReOpResult.Result) return ReOpResult;
            //比对新旧文件是否一样,若不一样，则删除旧的文件
            oldModel.FilePath.DeleteExistFile(model.FilePath, siteRootPath);
            return ReOpResult;
        }




        #region query method
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// / 获得供应商合格文件项目
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="eligibleCertificate"></param>
        /// <returns></returns>
        internal SupplierQualifiedCertificateModel GetQualifiedCertificateModelBy(string supplierId, string eligibleCertificate)
        {
            var dataList = GetQualifiedCertificateListBy(supplierId);
            if (dataList == null || dataList.Count == 0) return null;
            return dataList.FirstOrDefault(e => e.EligibleCertificate == eligibleCertificate);
        }
        #endregion
    }
    /// <summary>
    /// 供应商信息Curd
    /// </summary>
    public class SuppliersInfoCrud : CrudBase<SupplierInfoModel, ISupplierInfoRepository>
    {

        public SuppliersInfoCrud()
            : base(new SupplierInfoRepository(), "供应商信息")
        { }
        #region  crud
        /// <summary>
        /// <param name="model"></param>
        /// </summary>
        /// <returns></returns>
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, Add);
        }
        /// <summary>
        /// 添加供应商信息
        /// </summary>
        /// <param name="model></param>
        /// <returns></returns>
        OpResult Add(SupplierInfoModel model)
        {
            ///判断产品品号是否存在
            try
            {
                if (irep.IsExist(m => m.SupplierId == model.SupplierId))
                    return OpResult.SetErrorResult("此数据已存在！");
                return irep.Insert(model).ToOpResult_Add(OpContext);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }

        }

        //public OpResult EidtSupplierInfo(SupplierInfoModel model)
        //{
        //    if (irep.IsExist(m => m.SupplierId == model.SupplierId))
        //    {
        //        if (model.OpSign == "editPurchaseType")
        //            return irep.Update(u => u.SupplierId == model.SupplierId,
        //                f => new SupplierInfoModel
        //                {
        //                    SupplierProperty = model.SupplierProperty,
        //                    PurchaseType = model.PurchaseType
        //                }).ToOpResult_Eidt("修改供应商类别成功！");
        //        return irep.Update(m => m.SupplierId == model.SupplierId, model).ToOpResult_Eidt("修改成功");

        //    }
        //    else return OpResult.SetErrorResult("此数据不存在！无法修改");


        //}
        /// <summary>
        /// 更新供应商信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal OpResult Update(SupplierInfoModel model)
        {
            try
            {
                return irep.Update(u => u.SupplierId == model.SupplierId,
                       f => new SupplierInfoModel
                       {
                           SupplierProperty = model.SupplierProperty,
                           PurchaseType = model.PurchaseType,
                           PurchaseUser = model.PurchaseUser,
                           SupplierAddress = model.SupplierAddress,
                           Remark = model.Remark,
                           PayCondition = model.PayCondition,
                           SupplierPrincipal = model.SupplierPrincipal,
                           SupplierEmail = model.SupplierEmail,
                           SupplierFaxNo = model.SupplierFaxNo,
                           SupplierTel = model.SupplierTel,
                           SupplierUser = model.SupplierUser,
                           OpSign = model.OpSign
                       }).ToOpResult_Eidt("修改供应商类别成功！");
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }

        }

        internal OpResult Init(SupplierInfoModel model)
        {
            try
            {
                SetFixFieldValue(model);
                model.SupplierId = model.SupplierId.Trim();
                model.OpSign = "init";
                return irep.Insert(model).ToOpResult_Add(OpContext);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        #endregion

        #region query
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
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion
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
        public List<SupplierSeasonAuditModel> GetlimitScoreSupplierAuditInfo(string seasonDateNum, double limitTotalCheckScore, double limitQualityCheck)
        {
            return this.irep.Entities.Where(e => (e.TotalCheckScore < limitTotalCheckScore || e.QualityCheck < limitQualityCheck) && e.SeasonDateNum == seasonDateNum).OrderBy(e => e.SupplierId).ToList();
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="parameterKey"></param>
        /// <returns></returns>
        public SupplierSeasonAuditModel GetSupplierSeasonAuditInfo(string parameterKey)
        {
            return this.irep.FirstOfDefault(e => e.ParameterKey == parameterKey);
        }

        OpResult AddSupplierSeasonAuditInfo(SupplierSeasonAuditModel model)
        {
            model.ParameterKey = model.SupplierId.Trim() + "&" + model.SeasonDateNum;
            if (!irep.IsExist(e => e.ParameterKey == model.ParameterKey))
                return irep.Insert(model).ToOpResult_Add(OpContext);
            return irep.Update(e => e.ParameterKey == model.ParameterKey, f => new SupplierSeasonAuditModel
            {
                QualityCheck = model.QualityCheck,
                ActionLiven = model.ActionLiven,
                AuditPrice = model.AuditPrice,
                CheckLevel = model.CheckLevel,
                DeliveryDate = model.DeliveryDate,
                ManagerRisk = model.ManagerRisk,
                MaterialGrade = model.MaterialGrade,
                SubstitutionSupplierId = model.SubstitutionSupplierId,
                HSFGrade = model.HSFGrade,
                TotalCheckScore = model.TotalCheckScore,
                OpPserson = model.OpPserson,
                Remark = model.Remark
            }).ToOpResult_Eidt(OpContext);
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
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Add(OpContext); ;
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
    /// 供应商稽核自评复评明细表 Crud
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
            entity.ParameterKey = entity.SupplierId + "&" + entity.GradeYear + "&" + entity.SupGradeType;
            if (!IsExist(entity.ParameterKey))
                return irep.Insert(entity).ToOpResult_Add(OpContext);
            return EditSupplierGradeInfo(entity);
        }


        public SupplierGradeInfoModel GetPurSupGradeInfoBy(string parameterKey)
        {
            return irep.FirstOfDefault(e => e.ParameterKey == parameterKey);
        }
        public List<SupplierGradeInfoModel> GetPurSupGradeInfoBy(string supplierId, string gradeYear)
        {
            return irep.Entities.Where(e => e.SupplierId == supplierId & e.GradeYear == gradeYear).ToList();
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        OpResult EditSupplierGradeInfo(SupplierGradeInfoModel entity)
        {

            entity.GradeYear = entity.FirstGradeDate.Year.ToString();
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
