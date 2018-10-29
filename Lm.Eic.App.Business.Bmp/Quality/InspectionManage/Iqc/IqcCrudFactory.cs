﻿using Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExtension.Validation;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Lm.Eic.App.Business.Bmp.Quality;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
    #region  IQC  IQC物料检验配置 Crud
    /// <summary>
    /// IQC物料检验配置
    /// </summary>
    internal class InspectionIqcItemConfigCrud : CrudBase<InspectionIqcItemConfigModel, IIqcInspectionItemConfigRepository>
    {
        public InspectionIqcItemConfigCrud() : base(new IqcInspectionItemConfigRepository(), "IQC物料检验配置")
        { }
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddInspectionItemConfig);
            this.AddOpItem(OpMode.Edit, EidtInspectionItemConfig);
            this.AddOpItem(OpMode.Delete, DeleteInspectionItemConfig);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult DeleteInspectionItemConfig(InspectionIqcItemConfigModel model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult EidtInspectionItemConfig(InspectionIqcItemConfigModel model)
        {
            ///公用不能添加修改 
            if (model.SizeMemo == "条件ROHS检验") return OpResult.SetErrorResult("符合条件ROHS检验，不能修改操作!");
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult AddInspectionItemConfig(InspectionIqcItemConfigModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
        /// <summary>
        /// 在数据库中是否存在此料号
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>

        internal bool IsExistInspectionConfigmaterailId(string materailId)
        {
            return this.irep.IsExist(e => e.MaterialId == materailId);
        }
        /// <summary>
        /// 查询IQC物料检验配置数据
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<InspectionIqcItemConfigModel> FindIqcInspectionItemConfigDatasBy(string materialId)
        {
            return irep.Entities.Where(e => e.MaterialId == materialId&&e.IsActivate.ToUpper()=="TRUE").OrderBy(e => e.InspectionItemIndex).ToList();
        }



        public List<InspectionIqcItemConfigModel> FindAllIqcInspectionItemConfigDatasBy(string materialId)
        {
            return irep.Entities.Where(e => e.MaterialId == materialId).OrderBy(e => e.InspectionItemIndex).ToList();
        }
        public List<InspectionIqcItemConfigModel> FindIqcInspectionItemConfigDatasBy(string checkStatus, DateTime dateFrom, DateTime dateTo)
        {
            DateTime startOpdate = dateFrom.ToDate();
            DateTime endOpDate = dateTo.ToDate();
          return ( checkStatus==null|| checkStatus==string.Empty) ?
                 irep.Entities.Where(e => e.OpDate >= startOpdate && e.OpDate <= endOpDate).OrderBy(e => e.InspectionItemIndex).ToList():
            irep.Entities.Where(e => e.OpDate>= startOpdate && e.OpDate<=endOpDate & e.CheckStatus== checkStatus).OrderBy(e => e.InspectionItemIndex).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inspectionItem"></param>
        /// <returns></returns>
        public InspectionIqcItemConfigModel FindFirstOrDefaultDataBy(string inspectionItem)
        {
            return irep.Entities.FirstOrDefault(e => e.InspectionItem == inspectionItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inspectionItem"></param>
        /// <returns></returns>
        public InspectionIqcItemConfigModel FindFirstOrDefaultDataBy(string MaterialId, string inspectionItem)
        {
            return irep.Entities.FirstOrDefault(e =>e.MaterialId==MaterialId&& e.InspectionItem == inspectionItem);
        }
        /// <summary>
        /// 特殊条件
        /// </summary>
        /// <param name="sizeMemo"></param>
        /// <returns></returns>
        public List<InspectionIqcItemConfigModel> FindIqcSpecialItemConfigDatasBy(string sizeMemo)
        {
            return irep.Entities.Where(e => e.SizeMemo.Contains(sizeMemo)).OrderBy(e => e.InspectionItemIndex).ToList();
        }

        /// <summary>
        /// 批量保存 IQC检验项目数据
        /// </summary>
        /// <param name="modeldatas"></param>
        /// <returns></returns>
        internal OpResult StoreInspectionItemConfigDatas(List<InspectionIqcItemConfigModel> modeldatas)
        {
            OpResult opResult = OpResult.SetErrorResult("未执行任何操作！");
            //  InspectionItemInPutDate, CheckStatus, CheckPerson, CheckDate, ConfigVersion,
            SetFixFieldValue(modeldatas, OpMode.Add);
            int i = 0;
            //如果存在 就修改   
            modeldatas.ForEach(m =>
            {
                if(m.CheckStatus!= "变更中")
                {
                    if (this.irep.IsExist(e => e.MaterialId == m.MaterialId && e.InspectionItem == m.InspectionItem))
                    { m.OpSign = OpMode.Edit; }
                    else
                    {
                        m.InspectionItemInPutDate = DateTime.Now.Date.ToDate();
                        m.CheckDate = DateTime.Now.Date.ToDate();
                        m.CheckStatus = "未审核";
                    }
                    opResult = this.Store(m);
                    if (opResult.Result) i = i + opResult.RecordCount;
                }
                else
                {
                    m.InspectionItemInPutDate = DateTime.Now.Date.ToDate();
                    m.CheckDate = DateTime.Now.Date.ToDate();
                    m.CheckStatus = "未审核";
                    opResult = this.Store(m);
                    if (opResult.Result) i = i + opResult.RecordCount;
                };
               
            });
            opResult = i.ToOpResult(OpContext);
            if (i == modeldatas.Count) opResult.Entity = modeldatas;
            return opResult;
        }


        internal OpResult UPdateCheckInspectionItemConfigDatas(List<InspectionIqcItemConfigModel> modeldatas)
        {
            OpResult opResult = OpResult.SetErrorResult("未执行任何操作！");
            int i = 0;
            InspectionIqcItemConfigModel oldModel = null;
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
                        //opResult= irep.Update(e => e.Id_Key == oldModel.Id_Key,e=>new InspectionIqcItemConfigModel { IsActivate = "False" }).ToOpResult_Eidt("旧版失效");
                        //if (opResult.Result)
                        //{
                            configVersion = (m.ConfigVersion.Split('.').ToList().LastOrDefault().ToInt() + 1).ToString("000");
                            m.ConfigVersion = DateTime.Now.ToString("yy") + "." + DateTime.Now.ToString("MM") + "." + configVersion;
                            m.OpSign = OpMode.Add;
                        //}
                    }
                    else {
                        m.InspectionItemInPutDate = DateTime.Now.Date.ToDate();
                        m.CheckDate = DateTime.Now.Date.ToDate();
                    }
                    opResult = this.Store(m);
                    i = (opResult.Result)? i + opResult.RecordCount:i;
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
    /// 进料检验单（ERP）  物料检验项次
    /// </summary>
    internal class InspectionIqcMasterCrud : CrudBase<InspectionIqcMasterModel, IIqcInspectionMasterRepository>
    {
        public InspectionIqcMasterCrud() : base(new IqcInspectionMasterRepository(), "物料检验")
        {
        }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddIqcInspectionMaster);
            this.AddOpItem(OpMode.Edit, EidtIqcInspectionMaster);
            this.AddOpItem(OpMode.Delete, DeleteIqcInspectionMaster);
        }
        private OpResult DeleteIqcInspectionMaster(InspectionIqcMasterModel model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }

        private OpResult EidtIqcInspectionMaster(InspectionIqcMasterModel model)
        {

            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        public OpResult Update(InspectionIqcMasterModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        /// <summary>
        /// 更新（审核）信息详细列表SQl语句
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materialId"></param>
        /// <param name="inspectionStatus"></param>
        /// <returns></returns>
        internal OpResult UpAuditDetailData(string orderId, string materialId, string inspectionStatus)
        {
            // return irep.UpAuditDetailData(orderId, materialId, inspectionStatus).ToOpResult_Eidt(OpContext);
            return irep.Update(e => e.OrderId == orderId && e.MaterialId == materialId, u => new InspectionIqcMasterModel { InspectionStatus = inspectionStatus }).ToOpResult_Eidt(OpContext);
        }

        private OpResult AddIqcInspectionMaster(InspectionIqcMasterModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
        internal List<InspectionIqcMasterModel> GetIqcInspectionMasterDatasBy(string materialId)
        {
            return irep.Entities.Where(e => e.MaterialId == materialId && e.InspectionStatus== InspectionConstant.InspectionStatus.HaveCheck).OrderByDescending(e=>e.Id_Key).ToList();
        }
        internal List<InspectionIqcMasterModel> GetIqcMasterContainDatasBy(string orderId)
        {
            return irep.Entities.Where(e => e.OrderId.Contains(orderId)).ToList();
        }
        internal bool IsExistOrderIdAndMaterailId(string orderId, string materialId)
        {
            return irep.IsExist(e => e.OrderId == orderId && e.MaterialId == materialId);
        }
        internal List<InspectionIqcMasterModel> GetIqcMasterDatasBy(DateTime startTime, DateTime endTime)
        {
            return irep.Entities.Where(e => e.MaterialInDate >= startTime && e.MaterialInDate <= endTime).ToList();
        }
        internal List<InspectionIqcMasterModel> GetIqcMasterDatasBy(string orderid)
        {
            return irep.Entities.Where(e => e.OrderId== orderid).OrderBy(f=>f.Id_Key).ToList();
        }
        internal InspectionIqcMasterModel GetIqcMasterDatasBy(string orderid,string materialId)
        {
            return irep.FirstOfDefault(e => e.OrderId==orderid&&e.MaterialId == materialId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inspectionStatus"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>

        internal List<InspectionIqcMasterModel> GetIqcInspectionMasterModelListBy(string inspectionStatus, DateTime startTime, DateTime endTime, string inspectionResult = null)
        {
            if (inspectionResult == null)
                return irep.Entities.Where(e => e.InspectionStatus == inspectionStatus && e.MaterialInDate >= startTime && e.MaterialInDate <= endTime).ToList();
            else return irep.Entities.Where(e => e.InspectionStatus == inspectionStatus && e.MaterialInDate >= startTime && e.MaterialInDate <= endTime && e.InspectionResult == inspectionResult).ToList();
        }
        internal List<InspectionIqcMasterModel> GetIqcInspectionMasterModelListBy(DateTime startTime, DateTime endTime)
        {
            DateTime starttime = startTime.ToDate();
            DateTime endtime = endTime.ToDate();
            if (starttime == endtime)
                return irep.Entities.Where(e => e.MaterialInDate == starttime).OrderBy(e => e.MaterialInDate).ToList();
            return irep.Entities.Where(e => e.MaterialInDate >= starttime && e.MaterialInDate <= endtime).OrderBy(e => e.MaterialInDate).ToList();
        }

        internal List<InspectionIqcMasterModel> GetIqcInspectionMasterStatusDatasBy(DateTime startTime, DateTime endTime, string inspectionStatus)
        {
            DateTime starttime = startTime.ToDate();
            DateTime endtime = endTime.ToDate();
            if (starttime == endtime)
                return irep.Entities.Where(e => e.MaterialInDate == starttime && e.InspectionStatus == inspectionStatus).OrderBy(e => e.MaterialInDate).ToList();
            return irep.Entities.Where(e => e.MaterialInDate >= starttime && e.MaterialInDate <= endtime && e.InspectionStatus == inspectionStatus).OrderBy(e => e.MaterialInDate).ToList();
        }
        internal List<InspectionIqcMasterModel> GetIqcInspectionMasterMaterialIdDatasBy(DateTime startTime, DateTime endTime, string materialId)
        {
            DateTime starttime = startTime.ToDate();
            DateTime endtime = endTime.ToDate();
            if (starttime == endtime)
                return irep.Entities.Where(e => e.MaterialInDate == starttime && e.MaterialId == materialId).OrderBy(e => e.MaterialInDate).ToList();
            return irep.Entities.Where(e => e.MaterialInDate >= starttime && e.MaterialInDate <= endtime && e.MaterialId == materialId).OrderBy(e => e.MaterialInDate).ToList();
        }
        internal List<InspectionIqcMasterModel> GetIqcInspectionMasterMaterialSupplierDatasBy(DateTime startTime, DateTime endTime, string materialSupplier)
        {
            DateTime starttime = startTime.ToDate();
            DateTime endtime = endTime.ToDate();
            if (starttime == endtime)
                return irep.Entities.Where(e => e.MaterialInDate == starttime && e.MaterialSupplier.Contains(materialSupplier)).OrderBy(e => e.MaterialInDate).ToList();
            return irep.Entities.Where(e => e.MaterialInDate >= starttime && e.MaterialInDate <= endtime && e.MaterialSupplier.Contains(materialSupplier)).OrderBy(e => e.MaterialInDate).ToList();
        }
        internal List<InspectionIqcMasterModel> GetIqcInspectionMasterInspectionItemsDatasBy(DateTime startTime, DateTime endTime, string inspectionItems)
        {
            DateTime starttime = startTime.ToDate();
            DateTime endtime = endTime.ToDate();
            if (starttime == endtime)
                return irep.Entities.Where(e => e.MaterialInDate == starttime && e.InspectionItems.Contains(inspectionItems)).OrderBy(e => e.MaterialInDate).ToList();
            return irep.Entities.Where(e => e.MaterialInDate >= starttime && e.MaterialInDate <= endtime && e.InspectionItems.Contains(inspectionItems)).OrderBy(e => e.MaterialInDate).ToList();
        }

    }


    /// <summary>
    ///进料检验单（ERP） 物料检验项次录入数据
    /// </summary>
    internal class InspectionIqcDetailCrud : CrudBase<InspectionIqcDetailModel, IIqcInspectionDetailRepository>
    {
        public InspectionIqcDetailCrud() : base(new IqcInspectionDetailRepository(), "物料检验项次")
        { }
        #region   Crud
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddIqcInspectionDetail);
            this.AddOpItem(OpMode.Edit, EidtIqcInspectionDetail);
            this.AddOpItem(OpMode.Delete, DeleteIqcInspectionDetail);
        }

        private OpResult DeleteIqcInspectionDetail(InspectionIqcDetailModel model)
        {
            var oldmodel = GetIqcOldDetailModelBy(model);
            if (oldmodel == null) return OpResult.SetSuccessResult("此项不存在，删除失败", false);
            model.Id_Key = oldmodel.Id_Key;
            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }

        private OpResult EidtIqcInspectionDetail(InspectionIqcDetailModel model)
        {
            // 先前判定是否存在
            var oldmodel = GetIqcOldDetailModelBy(model);
            if (oldmodel == null) return OpResult.SetSuccessResult("此项不存在，修改失败", false);
            model.Id_Key = oldmodel.Id_Key;
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult AddIqcInspectionDetail(InspectionIqcDetailModel model)
        {

            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="model"></param>
        /// <param name="siteRootPath"></param>
        /// <returns></returns>
        internal OpResult UploadFileIqcInspectionDetail(InspectionIqcDetailModel model, string siteRootPath)
        {
            if (model == null) return new OpResult("保存文件不能为空", false);
            var oldmodel = GetIqcOldDetailModelBy(model);
            if (oldmodel == null)
                return this.AddIqcInspectionDetail(model);//若不存在则直接添加
            model.Id_Key = oldmodel.Id_Key;
            if (oldmodel.DocumentPath != model.DocumentPath && oldmodel.DocumentPath != string.Empty && oldmodel.DocumentPath != null)//比对新旧文件是否一样,若不一样，则删除旧的文件
            {
                if (siteRootPath != string.Empty && siteRootPath != null)
                {
                    string fileName = Path.Combine(siteRootPath, oldmodel.DocumentPath);
                    fileName = fileName.Replace("/", @"\");
                    if (File.Exists(fileName))
                        File.Delete(fileName);

                }//删除旧的文件
            }
            this.SetFixFieldValue(model);
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);// 同时修改文件模型记录
        }
        /// <summary>
        /// 更新审核（Done）
        /// </summary>
        /// <param name="newModel"></param>
        /// <returns></returns>
        internal OpResult UpAuditInspectionDetailData(string orderId, string materialId, bool isCheck)
        {
            int i = 0;
            string inspectionItemStatus = isCheck ? InspectionConstant.InspectionItemStatus.Done : InspectionConstant.InspectionItemStatus.Doing;
            string str = isCheck ? "【审核】" : "【撤审】";
            var modellist = irep.Entities.Where(e => e.OrderId == orderId && e.MaterialId == materialId).ToList();
            if (modellist != null && modellist.Count > 0)
                modellist.ForEach(m =>
                 {
                     m.InspectionItemStatus = inspectionItemStatus;
                     var sigleOpResult = irep.Update(e => e.Id_Key == m.Id_Key, m).ToOpResult_Eidt(OpContext);
                     if (sigleOpResult.Result) i++;
                 });
            return modellist.Count == i ? i.ToOpResult_Eidt(OpContext + str) : OpResult.SetResult(OpContext + "操作失败", false);

        }
        private InspectionIqcDetailModel GetIqcOldDetailModelBy(InspectionIqcDetailModel newModel)
        {
            try
            {
                if (newModel.IsNull()) return null;
                return irep.Entities.FirstOrDefault(e => e.OrderId == newModel.OrderId && e.MaterialId == newModel.MaterialId & e.InspecitonItem == newModel.InspecitonItem);
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception(ex.InnerException.Message);
            }

        }
        #endregion

        #region  Find
        internal bool isExiststroe(InspectionIqcDetailModel model)
        {
            return irep.IsExist(e => e.OrderId == model.OrderId && e.MaterialId == model.MaterialId && e.InspecitonItem == model.InspecitonItem);
        }
        /// <summary>
        /// 由单号和料号得到所有检验项目的数据
        /// </summary>
        /// <param name="orderid">单号</param>
        /// <param name="materialId">料号</param>
        /// <returns></returns>
        internal List<InspectionIqcDetailModel> GetIqcInspectionDetailModelDatasBy(string materialId, string inspectionItem)
        {
            try
            {
                return irep.Entities.Where(e => e.InspecitonItem == inspectionItem && e.MaterialId == materialId).ToList();
            }
            catch (Exception)
            {
                return null;
                throw;
            }

        }

        internal InspectionIqcDetailModel GetIqcInspectionDetailModelBy(string orderid, string materialId, string inspectionItem)
        {
            try
            {
                return irep.FirstOfDefault(e => e.OrderId == orderid && e.MaterialId == materialId && e.InspecitonItem == inspectionItem);
            }
            catch (Exception)
            {
                return null;
                throw;
            }

        }
        internal List<InspectionIqcDetailModel> GetIqcInspectionDetailOrderIdModelBy(string orderid, string materialId)
        {
            return irep.Entities.Where(e => e.OrderId == orderid && e.MaterialId == materialId).ToList();
        }
        internal List<InspectionIqcDetailModel> GetIqcInspectionDetailOrderIdModelBy(string orderid)
        {
            return irep.Entities.Where(e => e.OrderId == orderid).ToList();
        }

        internal bool isExsitOrderId(string orderid)
        {
            return irep.IsExist(e => e.OrderId == orderid);
        }
        internal List<InspectionIqcDetailModel> GetIqcInspectionDetailDatasBy(string orderid, string materialId,int takecount)
        {
            return irep.Entities.Where(e => e.MaterialId == materialId && e.OrderId != orderid).Distinct().OrderBy(f => f.MaterialInDate).Take(takecount).ToList();
        }
        /// <summary>
        /// 删除抽检项目
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="materialId"></param>
        /// <param name="inspectionItem"></param>
        /// <returns></returns>
        internal OpResult DeleteInspectionItems(string orderid, string materialId, string inspectionItem)
        {
            return irep.Delete(e => e.OrderId == orderid && e.MaterialId == materialId && e.InspecitonItem == inspectionItem).ToOpResult_Delete(OpContext);
        }
        ///// <summary>
        /////  判定些物料在二年内是否有录入记录 
        ///// </summary>
        ///// <param name="sampleMaterial">物料料号</param>
        ///// <returns></returns>
        //internal bool JudgeMaterialTwoYearIsRecord(string sampleMaterial)
        //{
        //    var nn = irep.Entities.Where(e => e.MaterialInDate >= DateTime.Now.AddYears(-2));
        //    if (nn != null && nn.Count() > 0)
        //        return true;
        //    else return false;
        //}

        #endregion

    }
    #endregion

}
