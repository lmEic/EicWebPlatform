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
    public class InspectionIqcDataGather:InspectionDateGatherManageBase 
    { 
      

        public OpResult StoreInspectionIqcModelForm(InspectionItemDataSummaryLabelModel model)
        {
            var opReulst = new OpResult("数据为空，保存失败", false);
            if (model == null) return opReulst;
            opReulst = StoreInspectionIqcDetailModelForm(model);
            if (opReulst.Result)
                opReulst = StoreInspectionIqcMasterModelForm(model);
            return opReulst;
        }
        /// <summary>
        /// 通过总表 存储Iqc检验详细数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreInspectionIqcDetailModelForm(InspectionItemDataSummaryLabelModel model)
        {
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
                //InspectionItemStatus = model.InsptecitonItemIsFinished.ToString(),
                InspectionItemStatus="doing",
                MaterialId = model.MaterialId,
                MaterialInDate = model.MaterialInDate,
                OpSign = "add",
                Memo = model.Memo,
                OpPerson = model.OpPerson,
                Id_Key = model.Id_Key
            };
            if (datailModel != null && model.Id_Key > 0)
            { datailModel.OpSign = "edit"; }
            return InspectionManagerCrudFactory.IqcDetailCrud.Store(datailModel, true);


        }

        private OpResult StoreInspectionIqcMasterModelForm(InspectionItemDataSummaryLabelModel model)
        {
            InspectionIqcMasterModel MasterModel = new InspectionIqcMasterModel()
            {
                OrderId = model.OrderId,
                MaterialId = model.MaterialId,
                MaterialName = model.MaterialName,
                MaterialSpec = model.MaterialSpec,
                MaterialDrawId = model.MaterialDrawId,
                MaterialSupplier = model.MaterialSupplier,
                MaterialCount = model.MaterialInCount,
                MaterialInDate = model.MaterialInDate,
                InspectionMode = model.InspectionMode,
                InspectionItems = model.InspectionItem,
                FinishDate = DateTime.Now.Date,
                InspectionStatus = "待审核",
                InspectionResult = "未完成",
                OpSign = "add"
            };
            if (InspectionManagerCrudFactory.IqcMasterCrud.IsExistOrderIdAndMaterailId(model.OrderId, model.MaterialId))
            {
                MasterModel = InspectionManagerCrudFactory.IqcMasterCrud.GetIqcInspectionMasterModelListBy(model.OrderId, model.MaterialId);
                if (!InspectionManagerCrudFactory.IqcMasterCrud.IsExistOrderIdAndMaterailId(model.OrderId, model.MaterialId, model.InspectionItem))
                    MasterModel.InspectionItems += "," + model.InspectionItem;
                if (model.InspectionItemSumCount == GetHaveFinishDataNumber(MasterModel.InspectionItems))
                {
                    MasterModel.InspectionResult = "已完成";
                }
                MasterModel.OpSign = "edit";
            }
            return InspectionManagerCrudFactory.IqcMasterCrud.Store(MasterModel, true); ;
        }
        /// <summary>
        /// 加载所有的测试项目
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        private List<InspectionIqcItemConfigModel> getIqcNeedInspectionItemDatas(string materialId, DateTime materialInDate)
        {
            var needInsepctionItems = InspectionManagerCrudFactory.IqcItemConfigCrud.FindIqcInspectionItemConfigDatasBy(materialId);
            if (needInsepctionItems == null || needInsepctionItems.Count <= 0) return new List<InspectionIqcItemConfigModel>();
            needInsepctionItems.ForEach(m =>
            {

                if (m.InspectionItem.Contains("盐雾"))
                {
                    if (!InspectionManagerCrudFactory.IqcDetailCrud.JudgeYwTest(materialId, materialInDate))
                        needInsepctionItems.Remove(m);
                }
                if (m.InspectionItem.Contains("全尺寸"))
                {
                    if (InspectionManagerCrudFactory.IqcDetailCrud.JudgeMaterialTwoYearIsRecord(m.MaterialId))
                        needInsepctionItems.Remove(m);
                }

            });
            return needInsepctionItems;
        }
        /// <summary>
        /// 生成IQC检验项目所有的信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<InspectionItemDataSummaryLabelModel> BuildingIqcInspectionItemDataSummaryLabelListBy(string orderId, string materialId)
        {
            List<InspectionItemDataSummaryLabelModel> returnList = new List<InspectionItemDataSummaryLabelModel>();
            var orderMaterialInfo = GetPuroductSupplierInfo(orderId).FirstOrDefault(e => e.ProductID == materialId);
            if (orderMaterialInfo == null) return new List<InspectionItemDataSummaryLabelModel>(); ;
            var iqcNeedInspectionsItemdatas = getIqcNeedInspectionItemDatas(materialId, orderMaterialInfo.ProduceInDate);
            if (iqcNeedInspectionsItemdatas == null || iqcNeedInspectionsItemdatas.Count <= 0) return new List<InspectionItemDataSummaryLabelModel>(); ;
            //保存单头数据
           return DoInspectionSummayDatas( orderMaterialInfo, iqcNeedInspectionsItemdatas);
        }
        /// <summary>
        /// 处理数据总表
        /// </summary>
        /// <param name="orderMaterialInfo"></param>
        /// <param name="iqcNeedInspectionsItemdatas"></param>
        /// <returns></returns>
        private List<InspectionItemDataSummaryLabelModel>  DoInspectionSummayDatas( MaterialModel orderMaterialInfo, List<InspectionIqcItemConfigModel> iqcNeedInspectionsItemdatas)
        {
            List<InspectionItemDataSummaryLabelModel> returnList = new List<InspectionItemDataSummaryLabelModel>();
            iqcNeedInspectionsItemdatas.ForEach(m =>
            {
                var inspectionMode = GetJudgeInspectionMode("IQC", m.MaterialId, m.InspectionItem);
                ///得到检验方法数据
                var inspectionModeConfigModelData = this.GetInspectionModeConfigDataBy(m.InspectionLevel, m.InspectionAQL, orderMaterialInfo.ProduceNumber, inspectionMode);
                ///得到已经检验的数据  
                var iqcHaveInspectionData = GetIqcInspectionDetailModelBy(orderMaterialInfo.OrderID, orderMaterialInfo.ProductID, m.InspectionItem);
                ///初始化 综合模块
                var model = new InspectionItemDataSummaryLabelModel()
                {
                    OrderId = orderMaterialInfo.OrderID,
                    MaterialId = orderMaterialInfo.ProductID,
                    InspectionItem = m.InspectionItem,
                    EquipmentId = m.EquipmentId,
                    MaterialInDate = orderMaterialInfo.ProduceInDate,
                    MaterialDrawId = orderMaterialInfo.ProductDrawID,
                    MaterialName = orderMaterialInfo.ProductName,
                    MaterialSpec = orderMaterialInfo.ProductStandard,
                    MaterialSupplier = orderMaterialInfo.ProductSupplier,
                    MaterialInCount = orderMaterialInfo.ProduceNumber,
                    InspectionItemSumCount = iqcNeedInspectionsItemdatas.Count,
                    InspectionItemStatus = "Doing",
                    ///检验方法
                    InspectionMethod = m.InspectionMethod,
                    //数据采集类型
                    InspectionDataGatherType = m.InspectionDataGatherType,
                    SizeLSL = m.SizeLSL,
                    SizeUSL = m.SizeUSL,
                    SizeMemo = m.SizeMemo,
                    InspectionAQL = string.Empty,
                    InspectionMode = string.Empty,
                    InspectionLevel = string.Empty,
                    Memo = string.Empty,
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
                    model.InspectionItemStatus = iqcHaveInspectionData.InspectionItemStatus;
                    model.InsptecitonItemIsFinished = true;
                    model.Id_Key = iqcHaveInspectionData.Id_Key;
                    model.Memo = iqcHaveInspectionData.Memo;
                    model.HaveFinishDataNumber = DoHaveFinishDataNumber(iqcHaveInspectionData.InspectionItemResult, iqcHaveInspectionData.InspectionItemDatas, model.NeedFinishDataNumber);
                }
                returnList.Add(model);
            });
            return returnList;
        }

        /// <summary>
        /// 判断是否按提正常还
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        private string GetJudgeInspectionMode(string InspectionClass, string materialId, string InspecitonItem)
        {
            ///3，比较 对比
            ///4，返回一个 转换的状态
            ///1,通过料号 和 抽检验项目  得到当前的最后一次抽检的状态
            string retrunstirng = "正常";
            var DetailModeList = GetIqcDetailModeListlBy(materialId, InspecitonItem).OrderByDescending(e => e.MaterialInDate).Take(100).ToList();
            if (DetailModeList == null || DetailModeList.Count <= 0) return retrunstirng;
         
            var currentStatus = DetailModeList.Last().InspectionMode;
            ///2，通当前状态 得到抽样规则 抽样批量  拒受数
            var modeSwithParameterList = InspectionManagerCrudFactory.InspectionModeSwithConfigCrud.GetInspectionModeSwithConfiglistBy(InspectionClass, currentStatus);
            if (modeSwithParameterList == null || modeSwithParameterList.Count <= 0) return retrunstirng;
            int sampleNumberVauleMin = modeSwithParameterList.FindAll(e => e.SwitchProperty == "SampleNumber").Select(e => e.SwitchVaule).Min();
            int AcceptNumberVauleMax = modeSwithParameterList.FindAll(e => e.SwitchProperty == "AcceptNumber").Select(e => e.SwitchVaule).Max();
            int sampleNumberVauleMax = modeSwithParameterList.FindAll(e => e.SwitchProperty == "SampleNumber").Select(e => e.SwitchVaule).Max();
            int AcceptNumberVauleMin = modeSwithParameterList.FindAll(e => e.SwitchProperty == "AcceptNumber").Select(e => e.SwitchVaule).Min();
            var getNumber = DetailModeList.Take(sampleNumberVauleMax).Count(e => e.InspectionItemResult == "NG");
            switch (currentStatus)
            {
                case "加严":
                    retrunstirng = (getNumber >= AcceptNumberVauleMin) ? "正常" : currentStatus;
                    break;
                case "放宽":
                    retrunstirng = (getNumber <= AcceptNumberVauleMin) ? "正常" : currentStatus;
                    break;
                case "正常":
                    if (getNumber <= AcceptNumberVauleMin) retrunstirng = "放宽";
                    else
                    { ///加严的数量
                        int getTheNumber = DetailModeList.Take(sampleNumberVauleMin).Count(e => e.InspectionItemResult == "NG");
                        retrunstirng = (getTheNumber >= AcceptNumberVauleMax) ? "加严" : currentStatus;
                    }
                    break;
                default:
                    break;
            }
            return retrunstirng;
        }
        /// 查找IQC检验项目所有的信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<InspectionItemDataSummaryLabelModel> FindIqcInspectionItemDataSummaryLabelListBy(string orderId, string materialId)
        {
            List<InspectionItemDataSummaryLabelModel> returnList = new List<InspectionItemDataSummaryLabelModel>();
            var iqcHaveInspectionData = InspectionService.DataGatherManager.IqcDataGather.GetIqcInspectionDetailModeListlBy(orderId, materialId);
            if (iqcHaveInspectionData == null || iqcHaveInspectionData.Count <= 0) return returnList;
            var iqcItemConfigdatas = InspectionManagerCrudFactory.IqcItemConfigCrud.FindIqcInspectionItemConfigDatasBy(materialId);
            if (iqcItemConfigdatas == null || iqcItemConfigdatas.Count <= 0) return returnList;
            iqcHaveInspectionData.ForEach(m =>
           {
                ///初始化 综合模块
                var model = new InspectionItemDataSummaryLabelModel()
               {
                   OrderId = orderId,
                   MaterialId = materialId,
                   InspectionItem = m.InspecitonItem,
                   EquipmentId = m.EquipmentId,
                   MaterialInDate = m.MaterialInDate,
                   MaterialInCount = m.MaterialCount,
                   SizeLSL = 0,
                   SizeUSL = 0,
                   InspectionItemStatus=m.InspectionItemStatus,
                   SizeMemo = string.Empty,
                   InspectionAQL = string.Empty,
                   InspectionLevel = string.Empty,
                   InspectionCount = Convert.ToInt16(m.InspectionCount),
                   AcceptCount = 0,
                   RefuseCount = 0,
                   InspectionItemDatas = m.InspectionItemDatas,
                   InsptecitonItemIsFinished = true,
                   NeedFinishDataNumber = Convert.ToInt16(m.InspectionCount),
                   HaveFinishDataNumber =this.GetHaveFinishDataNumber(m.InspectionItemDatas),
                   InspectionItemResult = m.InspectionItemResult,
                   Memo = m.Memo,
                   InspectionMethod = string.Empty,
                   InspectionMode = m.InspectionMode,
                   Id_Key = m.Id_Key,
               };
               var iqcItemConfigdata = iqcItemConfigdatas.FirstOrDefault(e => e.InspectionItem == m.InspecitonItem);
               if (iqcItemConfigdata != null)
               {
                   model.SizeLSL = iqcItemConfigdata.SizeLSL;
                   model.SizeUSL = iqcItemConfigdata.SizeUSL;
                   model.SizeMemo = iqcItemConfigdata.SizeMemo;
                   model.InspectionAQL = iqcItemConfigdata.InspectionAQL;
                   model.InspectionMethod = iqcItemConfigdata.InspectionMethod;
                   model.InspectionLevel = iqcItemConfigdata.InspectionLevel;
                    //数据采集类型
                    model.InspectionDataGatherType = iqcItemConfigdata.InspectionDataGatherType;
               }
               //检验方式
               var  inspectionMode = GetJudgeInspectionMode("IQC",m.MaterialId, m.InspecitonItem);

               var inspectionModeConfigModelData = this.GetInspectionModeConfigDataBy(iqcItemConfigdata.InspectionLevel, iqcItemConfigdata.InspectionAQL, m.MaterialCount, inspectionMode);

               if (inspectionModeConfigModelData != null)
               {
                   model.AcceptCount = inspectionModeConfigModelData.AcceptCount;
                   model.RefuseCount = inspectionModeConfigModelData.RefuseCount;

               }

               returnList.Add(model);
           });
            return returnList;
        }

        /// <summary>
        /// 得到副表的详细参数
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materailId"></param>
        /// <param name="inspecitonItem"></param>
        /// <returns></returns>
        private InspectionIqcDetailModel GetIqcInspectionDetailModelBy(string orderId, string materailId, string inspecitonItem)
        {
            return GetIqcInspectionDetailModeListlBy(orderId, materailId).FirstOrDefault(e => e.InspecitonItem == inspecitonItem);
        }
        /// <summary>
        ///  得到副表的详细参数List
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materailId"></param>
        /// <returns></returns>
        private List<InspectionIqcDetailModel> GetIqcInspectionDetailModeListlBy(string orderId, string materailId)
        {
            return InspectionManagerCrudFactory.IqcDetailCrud.GetIqcInspectionDetailModelBy(orderId, materailId);
        }
        private List<InspectionIqcDetailModel> GetIqcDetailModeListlBy(string materailId, string inspecitonItem)
        {
            return InspectionManagerCrudFactory.IqcDetailCrud.GetIqcInspectionDetailModelListBy(materailId, inspecitonItem);
        }


      
       
    }
}
