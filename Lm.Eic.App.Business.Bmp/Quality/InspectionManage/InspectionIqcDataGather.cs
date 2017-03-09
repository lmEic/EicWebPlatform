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
        /// 得到抽样物料信息
        /// </summary>
        /// <param name="orderId">ERP单号</param>
        /// <returns></returns>
        public List<MaterialModel> GetPuroductSupplierInfo(string orderId)
        {
            return QualityDBManager.OrderIdInpectionDb.FindMaterialBy(orderId);
        }

    
        /// <summary>
        /// 通过总表 存储Iqc检验详细数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreInspectionIqcDetailModelForm(InspectionIqcItemDataSummaryLabelModel model)
        {
            if (model == null) return new OpResult("数据为空，保存失败");
            InspectionIqcDetailModel datailModel = new InspectionIqcDetailModel()
            {
                OrderId = model.OrderId,
                EquipmentId = model.EquipmentId,
                MaterialCount = model.MaterialInCount,
                InspecitonItem = model.InspectionItem,
                InspectionAcceptCount = model.AcceptCount,
                InspectionCount = model.InspectionCount,
                InspectionRefuseCount = model.RefuseCount,
                InspectionDate = DateTime.Now,
                InspectionItemDatas = model.InspectionItemDatas,
                InspectionItemResult = model.InspectionItemResult,
                InspectionItemSatus = model.InsptecitonItemIsFinished.ToString(),
                MaterialId = model.MaterialId,
                MaterialInDate = model.MaterialInDate,
                OpSign = "add",
                Id_Key = model.Id_Key
            };
            return StoreInspectionIqcDetailModel(datailModel);

        }

        /// <summary>
        /// 生成IQC检验项目所有的信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<InspectionIqcItemDataSummaryLabelModel> BuildingIqcInspectionItemDataSummaryLabelListBy(string orderId, string materialId)
        {
            List<InspectionIqcItemDataSummaryLabelModel> returnList = new List<InspectionIqcItemDataSummaryLabelModel>();
            var iqcNeedInspectionsItemdatas = InspectionIqcManagerCrudFactory.IqcItemConfigCrud.FindIqcInspectionItemConfigDatasBy(materialId);
            if (iqcNeedInspectionsItemdatas == null || iqcNeedInspectionsItemdatas.Count <= 0)
                return returnList;
            var orderMaterialInfo = GetPuroductSupplierInfo(orderId).FirstOrDefault(e => e.ProductID == materialId);
            if (orderMaterialInfo == null)  return returnList;

            double produceNumber = orderMaterialInfo.ProduceNumber;
            //保存单头数据
            
            // 加载测试项目
            string inspectionItems = string.Empty;
            List<string> inspectionModeList = new List<string>() {"正常"};
            iqcNeedInspectionsItemdatas.ForEach(m =>
            {
                ///得到检验方法数据
                var inspectionModeConfigModelData = GetInspectionModeConfigDataBy(m, produceNumber);
                ///得到已经检验的数据  
                var iqcHaveInspectionData = InspectionService.DataGatherManager.IqcDataGather.GetIqcInspectionDetailModelBy(orderId, materialId, m.InspectionItem);
                ///初始化 综合模块
                var model = new InspectionIqcItemDataSummaryLabelModel()
                {
                    OrderId = orderId,
                    MaterialId = materialId,
                    InspectionItem = m.InspectionItem,
                    EquipmentId = m.EquipmentId,
                    MaterialInDate= orderMaterialInfo.ProduceInDate,
                    MaterialInCount= produceNumber,
                    ///检验方法
                    InspectionMethod=m.InspectionMethod ,
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
                    inspectionModeList.Add(inspectionModeConfigModelData.InspectionMode);
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
                    model.HaveFinishDataNumber = GetHaveFinishDataNumber(iqcHaveInspectionData.InspectionItemDatas);
                }
                inspectionItems +=m.InspectionItem+",";
                returnList.Add(model);
            });
            inspectionItems = inspectionItems.Substring(0, inspectionItems.Length -1);
            StoreIqcInspectionMasterModel(orderMaterialInfo, inspectionModeList.Last(), inspectionItems);
            return returnList;
        }


        /// <summary>
        /// 判断是否按提正常还
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        private string GetJudgeInspectionMode(string materialId ,string  InspecitonItem)
        {
            /// 宽到正常
            ///BroadenToGeneralSampleNumber
            ///BroadenToGeneralAcceptNumber
            /// 正常到严
            /// GeneralToTightenSampleNumber
            /// GeneralToTightenAcceptNumber
            /// 严到正常
            /// TightenToGeneralSampleNumber
            /// TightenToGeneralAcceptNumber
            /// 正常到宽
            /// GeneralToBroadenSampleNumber
            ///GeneralToBroadenAcceptNumber
            /// 
            /// 放宽 BroadenToTighten  加严 Tighten  正常 General
            return "正常";
        }
        /// 查找IQC检验项目所有的信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<InspectionIqcItemDataSummaryLabelModel> FindIqcInspectionItemDataSummaryLabelListBy(string orderId, string materialId)
        {
            List<InspectionIqcItemDataSummaryLabelModel> returnList = new List<InspectionIqcItemDataSummaryLabelModel>();
            var iqcHaveInspectionData = InspectionService.DataGatherManager.IqcDataGather.GetIqcInspectionDetailModeListlBy(orderId, materialId);
            if (iqcHaveInspectionData == null || iqcHaveInspectionData.Count <= 0) return returnList;
            var iqcItemConfigdatas = InspectionIqcManagerCrudFactory.IqcItemConfigCrud.FindIqcInspectionItemConfigDatasBy(materialId);
            if (iqcItemConfigdatas == null || iqcItemConfigdatas.Count <= 0) return returnList;
             iqcHaveInspectionData.ForEach(m =>
            { 
                ///初始化 综合模块
                var model = new InspectionIqcItemDataSummaryLabelModel()
                {
                    OrderId = orderId,
                    MaterialId = materialId,
                    InspectionItem = m.InspecitonItem,
                    EquipmentId = m.EquipmentId,
                    MaterialInDate =m.MaterialInDate,
                    MaterialInCount =m.MaterialCount,
                    SizeLSL = 0,
                    SizeUSL = 0,
                    SizeMemo = string.Empty ,
                    InspectionAQL = string.Empty,
                    InspectionLevel = string.Empty,
                    InspectionCount = Convert.ToInt16(m.InspectionCount),
                    AcceptCount = 0 ,
                    RefuseCount = 0,
                    InspectionItemDatas = m.InspectionItemDatas,
                    InsptecitonItemIsFinished = true,
                    NeedFinishDataNumber = Convert.ToInt16(m.InspectionCount),
                    HaveFinishDataNumber = GetHaveFinishDataNumber(m.InspectionItemDatas),
                    InspectionItemResult = m.InspectionItemResult,
                    InspectionMode="正常",
                    Id_Key = m.Id_Key,
                };
                var iqcItemConfigdata = iqcItemConfigdatas.FirstOrDefault(e => e.InspectionItem == m.InspecitonItem);
                if (iqcItemConfigdata != null)
                {
                    model.SizeLSL = iqcItemConfigdata.SizeLSL;
                    model.SizeUSL = iqcItemConfigdata.SizeUSL;
                    model.SizeMemo = iqcItemConfigdata.SizeMemo;
                    model.InspectionAQL = iqcItemConfigdata.InspectionAQL;
                    model.InspectionLevel = iqcItemConfigdata.InspectionLevel;
                }
                var inspectionModeConfigModelData = GetInspectionModeConfigDataBy(iqcItemConfigdata, m.MaterialCount);

                if (inspectionModeConfigModelData!=null )
                {
                    model.AcceptCount = inspectionModeConfigModelData.AcceptCount;
                    model.RefuseCount = inspectionModeConfigModelData.RefuseCount;
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
        private OpResult StoreIqcInspectionMasterModel(MaterialModel orderMaterialInfo, string inspectionMode, string inspectionItems)
        {
           
            InspectionIqcMasterModel MasterModel = new InspectionIqcMasterModel()
            {
                OrderId = orderMaterialInfo.OrderID,
                MaterialId = orderMaterialInfo.ProductID,
                MaterialName = orderMaterialInfo.ProductName,
                MaterialDrawId = orderMaterialInfo.ProductDrawID,
                MaterialCount = orderMaterialInfo.ProduceNumber,
                MaterialSpec = orderMaterialInfo.ProductStandard,
                MaterialInDate = orderMaterialInfo.ProduceInDate,
                MaterialSupplier = orderMaterialInfo.ProductSupplier,
                InspectionMode = inspectionMode,
                InspectionItems = inspectionItems,
                FinishDate = DateTime.Now.Date ,
                InspectionStatus = "待审核",
                InspctionResult = "未判定",
                OpSign = "add"
            };
            if (!InspectionIqcManagerCrudFactory.IqcMasterCrud.IsExistOrderIdAndMaterailId(orderMaterialInfo.OrderID, orderMaterialInfo.ProductID))
            {
                return StoreIqcInspectionMasterModel(MasterModel);   // 得到需要检验的项目
            } 
              return  new OpResult("已经存在",true );
            
        }

        /// <summary>
        /// 分解录入数据得到个数
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
       /// 得到副表的详细参数
       /// </summary>
       /// <param name="orderId"></param>
       /// <param name="materailId"></param>
       /// <param name="inspecitonItem"></param>
       /// <returns></returns>
      private  InspectionIqcDetailModel GetIqcInspectionDetailModelBy(string orderId, string materailId,string inspecitonItem)
        {
            return GetIqcInspectionDetailModeListlBy(orderId, materailId).FirstOrDefault(e => e.InspecitonItem == inspecitonItem);
        }
        /// <summary>
        ///  得到副表的详细参数List
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materailId"></param>
        /// <returns></returns>
       private  List<InspectionIqcDetailModel> GetIqcInspectionDetailModeListlBy(string orderId, string materailId)
        {
            return InspectionIqcManagerCrudFactory.IqcDetailCrud.GetIqcInspectionDetailModelBy(orderId, materailId);
        }
        /// <summary>
        ///  存储Iqc检验详细数据
        /// </summary>
        /// <returns></returns>
        private OpResult StoreInspectionIqcDetailModel(InspectionIqcDetailModel model)
        {
           if (model!=null && model.Id_Key >0)
            {  model.OpSign = "edit";}
            return InspectionIqcManagerCrudFactory.IqcDetailCrud.Store(model,true);
        }

        /// <summary>
        /// 存储Iqc检验项次主要数据
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
       private InspectionModeConfigModel GetInspectionModeConfigDataBy(InspectionIqCItemConfigModel iqcInspectionItemConfig, double inMaterialCount)
        {

           string  inspectionMode = GetJudgeInspectionMode(iqcInspectionItemConfig.MaterialId, iqcInspectionItemConfig.InspectionItem);
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
