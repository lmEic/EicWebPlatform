﻿using Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
    #region  FQC检验配置管理 Crud
    /// <summary>
    /// FQC 应用工厂
    /// </summary>
    internal class InspectionFqcItemConfigCrud : CrudBase<InspectionFqcItemConfigModel, IFqcInspectionItemConfigRepository>
    {
        public InspectionFqcItemConfigCrud() : base(new FqcInspectionItemConfigRepository(), "Fqc料物配置")
        { }
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, Add);
            this.AddOpItem(OpMode.Edit, Eidt);
            this.AddOpItem(OpMode.Delete, DeleteFqcInspection);
        }

        private OpResult DeleteFqcInspection(InspectionFqcItemConfigModel model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }

        private OpResult Eidt(InspectionFqcItemConfigModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }

        private OpResult Add(InspectionFqcItemConfigModel model)
        {
            if (irep.IsExist(e => e.MaterialId == model.MaterialId && e.InspectionItem == model.InspectionItem))
            {
                model.Id_Key = irep.Entities.FirstOrDefault(e => e.MaterialId == model.MaterialId && e.InspectionItem == model.InspectionItem).Id_Key;
                return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
            }
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }

        /// <summary>
        /// 查询FQC物料检验配置数据
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<InspectionFqcItemConfigModel> FindFqcInspectionItemConfigDatasBy(string materialId)
        {
            var datas = irep.Entities.Where(e => e.MaterialId == materialId).ToList();
            if (datas != null && datas.Count > 0) return datas.OrderBy(e => e.InspectionItemIndex).ToList();
            return datas;
        }
        /// <summary>
        /// 是否存在并且审核激活
        /// </summary>
        /// <param name="materailId"></param>
        /// <returns></returns>
        public bool IsExistFqcConfigmaterailId(string materailId)
        {
            return irep.IsExist(e => e.MaterialId == materailId&&e.IsActivate.ToUpper()== "TRUE"&&e.CheckStatus== "已审核");
        }
        public InspectionFqcItemConfigModel FindFirstOrDefaultDataBy(string MaterialId, string inspectionItem)
        {
            return irep.Entities.FirstOrDefault(e => e.MaterialId == MaterialId && e.InspectionItem == inspectionItem);
        }
        public OpResult StoreFqcItemConfigList(List<InspectionFqcItemConfigModel> modelList)
        {
            OpResult opResult = OpResult.SetErrorResult("未执行任何操作！");
            SetFixFieldValue(modelList, OpMode.Add);
            int i = 0;
            //如果存在 就修改   
            modelList.ForEach(m =>
            {
                if (this.irep.IsExist(e => e.Id_Key == m.Id_Key))
                { m.OpSign = "edit"; }
                else
                {
                    m.InspectionItemInPutDate = DateTime.Now.Date.ToDate();
                    m.CheckDate = DateTime.Now.Date.ToDate();
                    m.CheckStatus = "未审核";
                }
                opResult = this.Store(m);
                if (opResult.Result)
                    i = i + opResult.RecordCount;
            });
            opResult = i.ToOpResult(OpContext);
            if (i == modelList.Count) opResult.Entity = modelList;
            return opResult;
        }


        internal OpResult UPdateCheckInspectionItemConfigDatas(List<InspectionFqcItemConfigModel> modeldatas)
        {
            OpResult opResult = OpResult.SetErrorResult("未执行任何操作！");
            int i = 0;
            InspectionFqcItemConfigModel oldModel = null;
            string configVersion = "001";
            //如果存在 就修改   
            modeldatas.ForEach(m =>
            {
                if (this.irep.IsExist(e => e.MaterialId == m.MaterialId && e.InspectionItem == m.InspectionItem))
                {
                    oldModel = FindFirstOrDefaultDataBy(m.MaterialId, m.InspectionItem);
                    //如果版本变更 则添加新的版本信息
                    if (m.CheckStatus == "变更中" && oldModel.CheckStatus != null)
                    {
                      
                        configVersion = (m.ConfigVersion.Split('.').ToList().LastOrDefault().ToInt() + 1).ToString("000");
                        m.ConfigVersion = DateTime.Now.ToString("yy") + "." + DateTime.Now.ToString("MM") + "." + configVersion;
                        m.OpSign = OpMode.Add;
                        //}
                    }
                    else
                    {
                        m.InspectionItemInPutDate = DateTime.Now.Date.ToDate();
                        m.CheckDate = DateTime.Now.Date.ToDate();
                    }
                    opResult = this.Store(m);
                    i = (opResult.Result) ? i + opResult.RecordCount : i;
                }
            });
            opResult = i.ToOpResult(OpContext);
            if (i == modeldatas.Count) opResult.Entity = modeldatas;


            return opResult;


        }


        internal OpResult OpCheckInspectionItemConfigDates(InspectionItemConfigCheckModel modelData)
        {
            var datas = irep.Entities.Where(e => e.MaterialId == modelData.MaterialId).ToList();
            OpResult opResult = OpResult.SetSuccessResult("", false);
            if (datas != null)
            {
                string isActivate = "True";
                datas.ForEach(e =>
                {
                    e.CheckPerson = modelData.OpPerson;
                    e.CheckStatus = modelData.CheckStatus;
                    e.CheckDate = DateTime.Now.Date;
                    e.OpSign = OpMode.Edit;
                    e.IsActivate = isActivate;
                    e.ConfigVersion = modelData.ItemConfigVersion;
                });
                opResult = UPdateCheckInspectionItemConfigDatas(datas);
                opResult.Entity = datas;
            }
            return opResult;
        }

    }

    /// <summary>
    /// FQC详细表
    /// </summary>
    internal class InspectionFqcDatailCrud : CrudBase<InspectionFqcDetailModel, IFqcInspectionDatailRepository>
    {
        public InspectionFqcDatailCrud() : base(new FqcInspectionDatailRepository(), "FQC详细表单")
        { }
        #region Crud
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, Add);
        }

     


        private OpResult Add(InspectionFqcDetailModel model)
        {
            if (model == null) return new OpResult("保存文件不能为空", false);
            bool dd = IsExist(model.OrderId, model.OrderIdNumber, model.InspectionItem);
            //如果存在 (Id_key 已经赋值） 
            if (IsExist(model.OrderId, model.OrderIdNumber, model.InspectionItem))
            {
                if (model.Id_Key == 0)
                    model.Id_Key = irep.FirstOfDefault(e =>
                    e.OrderId == model.OrderId
                    && e.OrderIdNumber == model.OrderIdNumber
                    && e.InspectionItem == model.InspectionItem).Id_Key;
                return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
            }
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }



        internal   OpResult AddDetailModel(InspectionFqcDetailModel model)
        {
            if (model == null) return new OpResult("保存文件不能为空", false);
            //如果存在 (Id_key 已经赋值） 
            if (IsExist(model.OrderId, model.OrderIdNumber, model.InspectionItem))
            {
                if (model.Id_Key == 0)
                    model.Id_Key = irep.FirstOfDefault(e =>
                    e.OrderId == model.OrderId
                    && e.OrderIdNumber == model.OrderIdNumber
                    && e.InspectionItem == model.InspectionItem).Id_Key;
                return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
            }
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
        internal OpResult UploadFileFqcInspectionDetail(InspectionFqcDetailModel model, string siteRootPath)
        {
            if (model == null) return new OpResult("采集数据模型不能为NULL", false);
            var oldmodel = this.GetFqcOldDetailModelBy(model);
            if (oldmodel == null)
                return this.Add(model);//若不存在则直接添加
            model.Id_Key = oldmodel.Id_Key;
            oldmodel.DocumentPath.DeleteExistFile(model.DocumentPath, siteRootPath);//删除旧的文件
            this.SetFixFieldValue(model);
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);// 同时修改文件模型记录
        }
        #endregion


        #region find method

        internal List<InspectionFqcDetailModel> GetFqcInspectionDetailDatasBy(string orderId, int orderIdNumber)
        {
            try
            {
                return irep.Entities.Where(e => e.OrderId == orderId && e.OrderIdNumber == orderIdNumber).ToList();
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception(ex.InnerException.Message);
            }

        }

        internal InspectionFqcDetailModel GetFqcInspectionDetailDatasBy(string orderId, int orderIdNumber, string inspectionItem)
        {
            return irep.Entities.FirstOrDefault(e => e.OrderId == orderId && e.OrderIdNumber == orderIdNumber && e.InspectionItem == inspectionItem);
        }
        internal bool IsExist(string orderId, int orderIdNumber, string inspecitonItem)
        {
            try
            {
                return irep.IsExist(e => e.OrderId == orderId && e.OrderIdNumber == orderIdNumber && e.InspectionItem == inspecitonItem);
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.InnerException.Message);
            }

        }

        internal InspectionFqcDetailModel GetFqcOldDetailModelBy(InspectionFqcDetailModel newmodel)
        {
            try
            {
                if (newmodel == null) return null;
                return irep.Entities.FirstOrDefault(e => e.OrderId == newmodel.OrderId && e.OrderIdNumber == newmodel.OrderIdNumber && e.InspectionItem == newmodel.InspectionItem);
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception(ex.InnerException.Message);
            }

        }



        internal List<InspectionFqcDetailModel> GetFqcDetailDatasBy(string orderId)
        {
            try
            {
                return irep.Entities.Where(e => e.OrderId == orderId).ToList();
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception(ex.InnerException.Message);
            }

        }

        internal OpResult DeleteFqcInspectionDetailDatasBy(string orderId, int orderIdNumber)
        {
            return irep.Delete(e => e.OrderIdNumber == orderIdNumber && e.OrderId == orderId).ToOpResult_Delete(OpContext);
        }

        internal OpResult UpAuditDetailDataStatus(string orderId, int orderIdNumber, string Updatestring)
        {
            return irep.Update(e => e.OrderId == orderId && e.OrderIdNumber == orderIdNumber, u => new InspectionFqcDetailModel { InspectionItemStatus = Updatestring }).ToOpResult_Eidt(OpContext);
        }
        #endregion
    }
    /// <summary>
    /// FQC主表
    /// </summary>
    internal class InspectionFqcMasterCrud : CrudBase<InspectionFqcMasterModel, IFqcInspectionMasterRepository>
    {
        public InspectionFqcMasterCrud() : base(new FqcInspectionMasterRepository(), "FQC总表信息")
        { }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddFqcInspectionMaster);
            this.AddOpItem(OpMode.Edit, EidtFqcInspectionMaster);
            this.AddOpItem(OpMode.Delete, DeleteFqcInspectionMaster);
        }

        private OpResult DeleteFqcInspectionMaster(InspectionFqcMasterModel model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }
        internal OpResult DeleteFqcInspectionMasterBy(string orderId, int orderNumber)
        {
            return irep.Delete(e => e.OrderId == orderId && e.OrderIdNumber == orderNumber).ToOpResult_Delete(OpContext);
        }

        private OpResult EidtFqcInspectionMaster(InspectionFqcMasterModel model)
        {

            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        internal bool IsExsitStoreModel(InspectionFqcMasterModel newModel)
        {
            if (newModel == null) return false;
            return irep.IsExist(e => e.OrderId == newModel.OrderId && e.OrderIdNumber == newModel.OrderIdNumber && e.MaterialId == newModel.MaterialId);
        }

        internal InspectionFqcMasterModel GetStroeOldModel(string orderId, int orderIdNumber, string materialId)
        {
            try
            {
                if (!irep.IsExist(e => e.OrderId == orderId && e.OrderIdNumber == orderIdNumber && e.MaterialId == materialId)) return null;
                return irep.Entities.FirstOrDefault(e => e.OrderId == orderId && e.OrderIdNumber == orderIdNumber && e.MaterialId == materialId);
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception(ex.Message);
            }
        }
        private OpResult AddFqcInspectionMaster(InspectionFqcMasterModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
        internal List<InspectionFqcMasterModel> GetFqcInspectionMasterModelListBy(string orderId)
        {
            return irep.Entities.Where(e => e.OrderId == orderId).ToList();
        }
        internal List<string > GetFqcInspectionMasterOrderIdList(DateTime opDate)
        {
            DateTime dd = opDate.ToDate();
            return irep.Entities.Where(e=>e.OpDate== dd &&e.InspectionResult != "未完成").Select(m=>m.OrderId).Distinct().ToList();
        }
        /// <summary>
        /// 查询Fqc Master抽检数据
        /// </summary>
        /// <param name="dateFrom">开始日期</param>
        /// <param name="dateTo">结束日期</param>
        /// <param name="selectedDepartment">部门</param>
        /// <param name="formStatus">状态</param>
        /// <returns></returns>
        internal List<InspectionFqcMasterModel> GetFqcInspectionMasterModelListBy(DateTime dateFrom, DateTime dateTo, string selectedDepartment, string formStatus = null)
        {
            if ((formStatus == "全部")&& ( selectedDepartment != "全部"))
                return irep.Entities.Where(e => e.ProductDepartment == selectedDepartment && e.OpDate >= dateFrom && e.OpDate <= dateTo).OrderBy(f => f.OpDate).ToList();
           else
                if(( selectedDepartment != "全部") && (formStatus == "全部"))
                   return   irep.Entities.Where(e =>e.InspectionStatus == formStatus && e.OpDate >= dateFrom && e.OpDate <= dateTo).OrderBy(f => f.OpDate).ToList();
                 else 
                       if((selectedDepartment == "全部") && (formStatus == "全部"))
                        return irep.Entities.Where(e => e.OpDate >= dateFrom && e.OpDate <= dateTo).OrderBy(f => f.OpDate).ToList();
                        else 
                            if((selectedDepartment == "全部") && (formStatus != "全部"))
                            return irep.Entities.Where(e => e.InspectionStatus == formStatus && e.OpDate >= dateFrom && e.OpDate <= dateTo).OrderBy(f => f.OpDate).ToList();
            return irep.Entities.Where(e => e.ProductDepartment == selectedDepartment && e.InspectionStatus == formStatus && e.OpDate >= dateFrom && e.OpDate <= dateTo).OrderBy(f => f.OpDate).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        internal List<InspectionFqcMasterModel> GetFqcInspectionMasterListBy(string materialId)
        {
            return irep.Entities.Where(e => e.MaterialId == materialId).OrderByDescending(e=>e.Id_Key).ToList();
        }
        /// <summary>
        /// 更新状主要状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderIdNumber"></param>
        /// <param name="updateInspectionItems"></param>
        /// <param name="updateInspectionStatus"></param>
        /// <param name="updateInspectionResult"></param>
        /// <returns></returns>
        internal OpResult UpdateMasterData(string orderId, int orderIdNumber,
            string updateInspectionItems,
            string updateInspectionStatus,
            string updateInspectionResult)
        {
            return irep.Update(e => e.OrderId == orderId && e.OrderIdNumber == orderIdNumber, n => new InspectionFqcMasterModel
            {
                InspectionItems = updateInspectionItems,
                InspectionStatus = updateInspectionStatus,
                InspectionResult = updateInspectionResult
            }).ToOpResult_Eidt(OpContext);
        }
    }
    /// <summary>
    /// ORT物料配置
    /// </summary>
    internal class OrtMaterialConfigCrud : CrudBase<MaterialOrtConfigModel, IOrtMaterailConfigRepository>
    {
        public OrtMaterialConfigCrud() : base(new OrtMaterailConfigRepository(), "ORT配置") { }

        protected override void AddCrudOpItems()
        {
            throw new NotImplementedException();
        }
        internal MaterialOrtConfigModel FindOrtMaterialDatasBy(string masterialId)
        {
            return irep.Entities.FirstOrDefault(e => e.MaterialId == masterialId);
        }
        internal OpResult ChangeMaterialIsValid(string masterialId, string isValid)
        {
            return irep.Update(e => e.MaterialId == masterialId, u => new MaterialOrtConfigModel { IsValid = isValid }).ToOpResult_Eidt(OpContext);
        }
        internal OpResult StoreMaterialOrtConfigModel(MaterialOrtConfigModel data)
        {
            if (irep.IsExist(e => e.MaterialId == data.MaterialId))
            {
                return irep.Update(e => e.Id_Key == data.Id_Key, data).ToOpResult_Eidt(OpContext);
            }
            return irep.Insert(data).ToOpResult_Add(OpContext);
        }
    }
    #endregion
}