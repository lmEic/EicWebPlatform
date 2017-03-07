using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.Erp.Bussiness.QmsManage;
using Lm.Eic.App.Erp.Bussiness.QuantityManage;
using Lm.Eic.App.Erp.Domain.QuantityModel;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
    /// <summary>
    /// 进料检验数据采集器
    /// </summary>
    public class InspectionIqcDataGather
    {
        /// <summary>
        /// IQC检验项目所有的信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<InspectionIqcItemDataSummaryLabelModel> GetIqcInspectionItemDataSummaryLabelListBy(string orderId, string materialId)
        {
            List<InspectionIqcItemDataSummaryLabelModel> returnList = new List<InspectionIqcItemDataSummaryLabelModel>();
            var iqcNeedInspectionsItemdatas = InspectionIqcManagerCrudFactory.IqcItemConfigCrud.FindIqcInspectionItemConfigDatasBy(materialId);
            if (iqcNeedInspectionsItemdatas == null || iqcNeedInspectionsItemdatas.Count <= 0)
                return returnList;
            double produceNumber = 0;
            //保存单头数据
            StoreIqcInspectionMasterModel(orderId, materialId, out produceNumber);
            /// 需是判断是放宽还是加严 
            string inspectionMode = "正常";
            string inspectionItems = string.Empty;
            List<DateTime> finishDateList = new List<DateTime>();
            iqcNeedInspectionsItemdatas.ForEach(m =>
            {
                ///得到检验方法数据
                var inspectionModeConfigModelData = GetInspectionModeConfigDataBy(m, produceNumber, inspectionMode);
                ///得到已经检验的数据  
                var iqcHaveInspectionData = InspectionService.DataGatherManager.IqcDataGather.GetIqcInspectionDetailModelBy(orderId, materialId, m.InspectionItem);
                ///初始化 综合模块
                var model = new InspectionIqcItemDataSummaryLabelModel()
                {
                    OrderId = orderId,
                    MaterialId = materialId,
                    InspectionItem = m.InspectionItem,
                    EquipmentId = m.EquipmentId,
                    MaterialInDate=DateTime .Now ,
                    MaterialInCount= produceNumber,
                    SizeLSL = m.SizeLSL,
                    SizeUSL = m.SizeUSL,
                    SizeMemo = m.SizeMemo,
                    InspectionAQL = string.Empty,
                    InspectionMode = string.Empty,
                    InspectionLevel = string.Empty,
                    InspectionCount = 0,
                    AcceptCount = 0,
                    RefuseCount = 0,
                    InspectionItemDatas = string.Empty,
                    InsptecitonItemIsFinished = false,
                    NeedFinishDataNumber = 0,
                    HaveFinishDataNumber = 0,
                    InspectionItemResult = string.Empty
                };
                if (inspectionModeConfigModelData != null)
                {
                    model.InspectionMode = inspectionModeConfigModelData.InspectionMode;
                    model.InspectionLevel = inspectionModeConfigModelData.InspectionLevel;
                    model.InspectionAQL = inspectionModeConfigModelData.InspectionAQL;
                    model.InspectionCount = inspectionModeConfigModelData.InspectionCount;
                    model.AcceptCount = inspectionModeConfigModelData.AcceptCount;
                    model.RefuseCount = inspectionModeConfigModelData.RefuseCount;
                    //需要录入的数据个数 暂时为抽样的数量
                    model.NeedFinishDataNumber = inspectionModeConfigModelData.InspectionCount;
                }
                if (iqcHaveInspectionData != null)
                {
                    model.InspectionItemDatas = iqcHaveInspectionData.InspectionItemDatas;
                    model.InspectionItemResult = iqcHaveInspectionData.InspectionItemResult;
                    model.EquipmentId = iqcHaveInspectionData.EquipmentId;
                    model.InsptecitonItemIsFinished = true;
                    model.Id_Key = iqcHaveInspectionData.Id_Key;
                    finishDateList.Add(iqcHaveInspectionData.InspectionDate);
                    model.HaveFinishDataNumber = GetHaveFinishDataNumber(iqcHaveInspectionData.InspectionItemDatas);
                }
                returnList.Add(model);
            });
           
            return returnList;
        }
        /// <summary>
        /// 存储数据到InspectionMaster中
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materialId"></param>
        /// <param name="produceNumber"></param>
        /// <returns></returns>
        private OpResult StoreIqcInspectionMasterModel(string orderId, string materialId, out double produceNumber)
        {
            produceNumber = 0;
           var orderMaterialInfo = GetPuroductSupplierInfo(orderId).FirstOrDefault(e => e.ProductID == materialId);
            if (orderMaterialInfo == null)
            { return null; }
            produceNumber = orderMaterialInfo.ProduceNumber;
            InspectionIqcMasterModel MasterModel = new InspectionIqcMasterModel()
            {
                OrderId = orderId,
                MaterialId = materialId,
                MaterialName = orderMaterialInfo.ProductName,
                MaterialDrawId = orderMaterialInfo.ProductDrawID,
                MaterialCount = orderMaterialInfo.ProduceNumber,
                MaterialSpec = orderMaterialInfo.ProductStandard,
                MaterialInDate = orderMaterialInfo.ProduceInDate,
                MaterialSupplier = orderMaterialInfo.ProductSupplier,
                InspectionMode = "正常",
                InspectionItems = string.Empty ,
                FinishDate = DateTime.Now ,
                InspectionStatus = "待审核",
                InspctionResult = "未判定",
                OpSign = "add"
            };
            if (!InspectionIqcManagerCrudFactory.IqcMasterCrud.IsExistOrderIdAndMaterailId(orderId, materialId))
            {
                return StoreIqcInspectionMasterModel(MasterModel);   // 得到需要检验的项目
            } 
              return  new OpResult("已经存在",true );
            
        }

        /// <summary>
        /// 分像数据
        /// </summary>
        /// <param name="inspectionDatas"></param>
        /// <returns></returns>
        private int GetHaveFinishDataNumber(string inspectionDatas)
        {
            if ((!inspectionDatas.Contains(",") )|| inspectionDatas == string.Empty || inspectionDatas == null) return 0;
            string[] mm = inspectionDatas.Split(',');
            return mm.Count();
        }
        /// <summary>
        /// 得到抽样物料信息
        /// </summary>
        /// <param name="orderId">ERP单号</param>
        /// <returns></returns>
        public List<MaterialModel> GetPuroductSupplierInfo(string orderId)
        {
            return QualityDBManager.OrderIdInpectionDb.FindMaterialBy(orderId);
        }
        
        /// <summary>
        /// 得到IQC物料料号得到相应的规格参数 
        /// </summary>
        /// <param name="orderId">ERP单号</param>
        /// <param name="sampleMaterialId">物料料号</param>
        /// <returns></returns>
        public InspectionIqCItemConfigModel GetIqcInspectionItemConfigDataBy(string sampleMaterialId,string inspectionItem)
        {
            return InspectionIqcManagerCrudFactory.IqcItemConfigCrud.FindIqcInspectionItemConfigDatasBy(sampleMaterialId).FirstOrDefault(e => e.InspectionItem == inspectionItem);
        }
       
        public InspectionIqcDetailModel GetIqcInspectionDetailModelBy(string orderId, string materailId,string inspecitonItem)
        {
            return InspectionIqcManagerCrudFactory.IqcDetailCrud.GetIqcInspectionDetailModelBy(orderId, materailId, inspecitonItem);
        }
        public List<InspectionIqcDetailModel> GetIqcInspectionDetailModelBy(string orderId, string materailId)
        {
            return InspectionIqcManagerCrudFactory.IqcDetailCrud.GetIqcInspectionDetailModelBy(orderId, materailId);
        }
        /// <summary>
        /// 存储Iqc检验数据
        /// </summary>
        /// <returns></returns>
       private  OpResult StoreIqcInspectionDetailModel(InspectionIqcDetailModel model)
        {
           if (model!=null && model.Id_Key >0)
            {  model.OpSign = "edit";}
            return InspectionIqcManagerCrudFactory.IqcDetailCrud.Store(model,true);
        }
        public OpResult StoreIqcInspectionItemDataSummary(InspectionIqcItemDataSummaryLabelModel model)
        {
            if (model == null) return new OpResult("数据为空，保存失败");
            InspectionIqcDetailModel datailModel = new InspectionIqcDetailModel()
            {
                OrderId = model.OrderId,
               
                EquipmentId = model.EquipmentId,
                MaterialCount = model.MaterialInCount ,
                InspecitonItem = model.InspectionItem,
                InspectionAcceptCount = model.AcceptCount,
                InspectionCount = model.InspectionCount,
                InspectionRefuseCount = model.RefuseCount,
                InspectionDate = DateTime.Now,
                InspectionItemDatas = model.InspectionItemDatas,
                InspectionItemResult = model.InspectionItemResult,
                InspectionItemSatus = model.InsptecitonItemIsFinished.ToString(),
                MaterialId=model.MaterialId,
                MaterialInDate=model.MaterialInDate,
                OpSign = "add",
                Id_Key = model.Id_Key
            };
           return StoreIqcInspectionDetailModel(datailModel);
        
        }


         
        /// <summary>
        /// 存储Iqc检验项次
        /// </summary>
        /// <returns></returns>
        public OpResult StoreIqcInspectionMasterModel(InspectionIqcMasterModel model)
        {
            return InspectionIqcManagerCrudFactory.IqcMasterCrud.Store(model, true);
        }
        /// <summary>
        /// 由检验项目得到检验方式模块
        /// </summary>
        /// <param name="iqcInspectionItemConfig"></param>
        /// <param name="inMaterialCount"></param>
        /// <returns></returns>
        public InspectionModeConfigModel GetInspectionModeConfigDataBy(InspectionIqCItemConfigModel iqcInspectionItemConfig, double inMaterialCount, string inspectionMode="正常")
        {
            var maxs = new List<Int64>(); var mins = new List<Int64>();
            double maxNumber; double minNumber;
            if (iqcInspectionItemConfig == null) return new InspectionModeConfigModel(); ;
            var models = InspectionIqcManagerCrudFactory.InspectionModeConfigCrud.GetInspectionStartEndNumberBy(
                inspectionMode,
                iqcInspectionItemConfig.InspectionLevel,
                iqcInspectionItemConfig.InspectionAQL).ToList();
            models.ForEach(e =>
            { maxs.Add(e.EndNumber); mins.Add(e.StartNumber); });
            if (maxs.Count > 0)
                maxNumber = GetMaxNumber(maxs, inMaterialCount);
            else
                maxNumber = 0;
            if (mins.Count > 0)
                minNumber = GetMinNumber(mins, inMaterialCount);
            else
                minNumber = 0;
            var model= models.FirstOrDefault(e => e.StartNumber == minNumber && e.EndNumber == maxNumber);
            if (model != null)
            {
                model.InspectionMode = inspectionMode;
                //如果为负数 则全检
                model.InspectionCount = model.InspectionCount < 0 ? Convert.ToInt32(inMaterialCount) : model.InspectionCount;
                return model;
            }
            else return null;
            // InspectionCount, AcceptCount, RefuseCount,
        }
        private Int64 GetMaxNumber(List<Int64> maxNumbers, double number)
        {
            List<Int64> IntMaxNumbers = new List<Int64>();
            foreach (var max in maxNumbers)
            {
                if (max != -1)
                {

                    if (max >= number)
                    {
                        IntMaxNumbers.Add(max);
                    }
                }
            }
            if (IntMaxNumbers.Count > 0)
            { return IntMaxNumbers.Min(); }
            else return -1;
        }
        private Int64 GetMinNumber(List<Int64> minNumbers, double mumber)
        {
            List<Int64> IntMinNumbers = new List<Int64>();
            foreach (var min in minNumbers)
            {
                if (min != -1)
                {

                    if (min <= mumber)
                    {
                        IntMinNumbers.Add(min);
                    }
                }
                else return -1;
            }
            return IntMinNumbers.Max();
        }
    }
}
