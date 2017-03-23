using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.Erp.Domain.QuantityModel;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
    public class InspectionFqcDataGather:InspectionDateGatherManageBase
    {
        #region  对外控件  Public
        /// <summary>
        /// 找到Mater列表数据
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<InspectionFqcMasterModel> GetFqcMasterOrderIdDatasBy(string orderId)
        {
            return InspectionManagerCrudFactory.FqcMasterCrud.GetFqcInspectionMasterModelListBy(orderId);
        }
        /// <summary>
        /// 生成FQC检验项目所有的信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<InspectionItemDataSummaryLabelModel> BuildingFqcInspectionSummaryDataBy(string orderId, double sampleCount)
        {
            List<InspectionItemDataSummaryLabelModel> returnList = new List<InspectionItemDataSummaryLabelModel>();
            ///一个工单 对应一个料号，有工单就是料号
            var orderMaterialInfo = GetPuroductSupplierInfo(orderId).FirstOrDefault();
            if (orderMaterialInfo == null) return returnList;
            ///得到需要检验的项目
            var fqcNeedInspectionsItemdatas = getFqcNeedInspectionItemDatas(orderMaterialInfo.ProductID);
            if (fqcNeedInspectionsItemdatas == null || fqcNeedInspectionsItemdatas.Count <= 0) return returnList;

            /// Master表中得到序号
            int orderIdNumber = 0;
            var FqcHaveInspectionAllOrderiDDatas = GetFqcMasterOrderIdDatasBy(orderId);
            if (FqcHaveInspectionAllOrderiDDatas == null || fqcNeedInspectionsItemdatas.Count <= 0) orderIdNumber = 1;
            else orderIdNumber = FqcHaveInspectionAllOrderiDDatas.Max(e => e.OrderNumber);
            ///处理数据
            returnList = DoBuildingSummaryDataLabelModel(sampleCount, orderIdNumber, orderMaterialInfo, fqcNeedInspectionsItemdatas);
            StoreBuildingFqcMaster(orderMaterialInfo, orderId, sampleCount, orderIdNumber);
            return returnList;
        }
        /// <summary>
        /// 得到FQC检验项目所有的信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderIdNumber"></param>
        /// <returns></returns>
        public List<InspectionItemDataSummaryLabelModel> FindFqcFqcInspectionSummaryDataBy(string orderId, int orderIdNumber)
        {

            ///一个工单 对应一个料号，有工单就是料号
            var orderMaterialInfo = GetPuroductSupplierInfo(orderId).FirstOrDefault();
            if (orderMaterialInfo == null)
                return new List<InspectionItemDataSummaryLabelModel>(); ;
            ///得到需要检验的项目
            var fqcHaveInspectionDatas = GetFqcInspectionDetailModeListlBy(orderId, orderIdNumber);
            if (fqcHaveInspectionDatas == null || fqcHaveInspectionDatas.Count <= 0)
                return new List<InspectionItemDataSummaryLabelModel>(); ;
            ///得到需要检验的项目
            var fqcInspectionsItemdatas = getFqcNeedInspectionItemDatas(orderMaterialInfo.ProductID);
            if (fqcInspectionsItemdatas == null || fqcInspectionsItemdatas.Count <= 0)
                return new List<InspectionItemDataSummaryLabelModel>(); ;
            return DoFindSummaryDataLabelModel(orderMaterialInfo, fqcHaveInspectionDatas, fqcInspectionsItemdatas);


        }
        /// <summary>
        /// 存储收集数据
        /// </summary>
        /// <param name="sumModel"></param>
        /// <returns></returns>
        public OpResult StoreFqcDataGather(InspectionItemDataSummaryLabelModel sumModel)
        {
            var returnOpResult = new OpResult("数据为空，保存失败", false);
            if (sumModel == null) return returnOpResult;
            var masterModel = new InspectionFqcMasterModel();
            var detailModel = new InspectionFqcDetailModel();
            SumDataToConvterData(sumModel, out masterModel, out detailModel);
            if (detailModel == null) return new OpResult("详表数据为空，保存失败", false);
            returnOpResult = storeInspectionDetial(detailModel);
            if (!returnOpResult.Result || masterModel == null) return new OpResult("主表数据为空，保存失败", false);
            returnOpResult = storeInspectionMasterial(masterModel);
            return returnOpResult;
        }



        #endregion



        #region  对内处理 Private
        /// <summary>
        /// </summary>
        /// <param name="orderIdAndNumber"></param>
        /// <param name="InspectionItem"></param>
        /// <returns></returns>
        private List<InspectionFqcDetailModel> GetFqcInspectionDetailModeListlBy(string orderId,int  orderIdNumber)
        {
            return InspectionManagerCrudFactory.FqcDetailCrud.GetFqcInspectionDetailModelListBy(orderId, orderIdNumber);
        }
        /// <summary>
        /// 加载所有的测试项目
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        private List<InspectionFqcItemConfigModel> getFqcNeedInspectionItemDatas(string materialId)
        {
            var needInsepctionItems = InspectionManagerCrudFactory.FqcItemConfigCrud.FindFqcInspectionItemConfigDatasBy(materialId);
            if (needInsepctionItems == null || needInsepctionItems.Count <= 0) return new List<InspectionFqcItemConfigModel>();
            return needInsepctionItems;
        }

      

        private List<InspectionItemDataSummaryLabelModel> DoBuildingSummaryDataLabelModel(double sampleCount, int orderIdNumber, MaterialModel orderMaterialInfo, List<InspectionFqcItemConfigModel> fqcNeedInspectionsItemdatas)
        {
            List<InspectionItemDataSummaryLabelModel> returnList = new List<InspectionItemDataSummaryLabelModel>();
            fqcNeedInspectionsItemdatas.ForEach(m =>
            {
                ///得到检验方式 “正常” “放宽” “加严”
                var inspectionMode = GetJudgeInspectionMode("FQC", m.MaterialId);
                ///得到检验方案
                var inspectionModeConfigModelData = this.GetInspectionModeConfigDataBy(m.InspectionLevel, m.InspectionAQL, sampleCount, inspectionMode);

                ///初始化 综合模块
                var model = new InspectionItemDataSummaryLabelModel()
                {
                    OrderId = orderMaterialInfo.OrderID,
                    Number = orderIdNumber,
                    MaterialId = orderMaterialInfo.ProductID,
                    MaterialName = orderMaterialInfo.ProductName,
                    MaterialSpec = orderMaterialInfo.ProductStandard,
                    MaterialSupplier = orderMaterialInfo.ProductSupplier,
                    MaterialDrawId = orderMaterialInfo.ProductDrawID,
                    MaterialInDate = orderMaterialInfo.ProduceInDate,
                    MaterialInCount = orderMaterialInfo.ProduceNumber,
                    MaterialCount = sampleCount,
                    InspectionItem = m.InspectionItem,
                    EquipmentId = m.EquipmentId,
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
                returnList.Add(model);
            });
            return returnList;
        }

        private OpResult StoreBuildingFqcMaster(MaterialModel orderMaterialInfo, string orderId, double sampleCount, int orderIdNumber)
        {
            InspectionFqcMasterModel masterModel = new InspectionFqcMasterModel()
            {
                OrderId = orderId,
                OrderNumber = orderIdNumber,
                MaterialId = orderMaterialInfo.ProductID,
                MaterialName = orderMaterialInfo.ProductName,
                MaterialSpec = orderMaterialInfo.ProductStandard,
                MaterialDrawId = orderMaterialInfo.ProductDrawID,
                MaterialSupplier = orderMaterialInfo.ProductSupplier,
                MaterialCount = orderMaterialInfo.ProduceNumber,
                MaterialInDate = orderMaterialInfo.ProduceInDate,
                InspectionMode = "正常",
                InspectionItems = string.Empty,
                FinishDate = DateTime.Now.Date,
                InspectionStatus = "待审核",
                InspectionResult = "未完成",
                InspectionCount = sampleCount,
                Department = string.Empty,
                OpPerson = "EIC",
                OpSign = "add"
            };
          return  storeInspectionMasterial(masterModel);
        }

       

        private List<InspectionItemDataSummaryLabelModel> DoFindSummaryDataLabelModel(MaterialModel orderMaterialInfo, List<InspectionFqcDetailModel> fqcHaveInspectionDatas, List<InspectionFqcItemConfigModel> fqcInspectionsItemdatas)
        {
            List<InspectionItemDataSummaryLabelModel> returnList = new List<InspectionItemDataSummaryLabelModel>();
            ///保存单头数据
            fqcHaveInspectionDatas.ForEach(m =>
            {
                ///初始化 综合模块
                var model = new InspectionItemDataSummaryLabelModel()
                {
                    OrderId = orderMaterialInfo.OrderID,
                    Number = m.OrderIdNumber,
                    MaterialId = orderMaterialInfo.ProductID,
                    MaterialName = orderMaterialInfo.ProductName,
                    MaterialSpec = orderMaterialInfo.ProductStandard,
                    MaterialSupplier = orderMaterialInfo.ProductSupplier,
                    MaterialDrawId = orderMaterialInfo.ProductDrawID,
                    MaterialInDate = orderMaterialInfo.ProduceInDate,
                    MaterialInCount = orderMaterialInfo.ProduceNumber,
                    InspectionItem = m.InspecitonItem,
                    EquipmentId = m.EquipmentId,
                    InspectionItemStatus = m.InspectionItemStatus,
                    ///检验方法
                    InspectionMethod = m.InspectionMode,
                    //数据采集类型
                    InspectionCount = m.InspectionCount,

                    InspectionItemDatas = m.InspectionItemDatas,

                    ///需要完成数量 得于 检验数
                    NeedFinishDataNumber = m.InspectionCount,
                    InsptecitonItemIsFinished = false,
                    HaveFinishDataNumber = this.DoHaveFinishDataNumber(m.InspectionItemResult, m.InspectionItemDatas, m.InspectionCount),
                    InspectionItemResult = m.InspectionItemResult,
                };

                var InspectionsItemdata = fqcInspectionsItemdatas.FirstOrDefault(e => e.InspectionItem == m.InspecitonItem);
                if (InspectionsItemdata != null)
                {
                    model.InspectionDataGatherType = InspectionsItemdata.InspectionDataGatherType;
                    model.SizeLSL = InspectionsItemdata.SizeLSL;
                    model.SizeUSL = InspectionsItemdata.SizeUSL;
                    model.SizeMemo = InspectionsItemdata.SizeMemo;

                }
                ///得到检验方案
                var inspectionModeConfigModelData = this.GetInspectionModeConfigDataBy(InspectionsItemdata.InspectionLevel, InspectionsItemdata.InspectionAQL, m.MaterialCount, m.InspectionMode);
                if (inspectionModeConfigModelData != null)
                {
                    if (model.InspectionMode == string.Empty)
                        model.InspectionMode = inspectionModeConfigModelData.InspectionMode;
                    model.InspectionLevel = inspectionModeConfigModelData.InspectionLevel;
                    model.InspectionAQL = inspectionModeConfigModelData.InspectionAQL;
                    model.InspectionCount = inspectionModeConfigModelData.InspectionCount;
                    model.AcceptCount = inspectionModeConfigModelData.AcceptCount;
                    model.RefuseCount = inspectionModeConfigModelData.RefuseCount;
                    //需要录入的数据个数 暂时为抽样的数量
                    model.NeedFinishDataNumber = inspectionModeConfigModelData.InspectionCount;
                }
                returnList.Add(model);
            });
            return returnList;
        }

 
        private void SumDataToConvterData(InspectionItemDataSummaryLabelModel sumModel, out InspectionFqcMasterModel masterModel, out InspectionFqcDetailModel detailModel)
        {
            masterModel = new InspectionFqcMasterModel()
            {
                OrderId = sumModel.OrderId,
                OrderNumber= sumModel.Number ,
                MaterialId = sumModel.MaterialId,
                MaterialName = sumModel.MaterialName,
                MaterialSpec = sumModel.MaterialSpec,
                MaterialDrawId = sumModel.MaterialDrawId,
                MaterialSupplier = sumModel.MaterialSupplier,
                MaterialCount = sumModel.MaterialInCount,
                MaterialInDate = sumModel.MaterialInDate,
                InspectionMode = sumModel.InspectionMode,
                InspectionItems = sumModel.InspectionItem,
                FinishDate = DateTime.Now.Date,
                InspectionStatus = "待审核",
                InspectionResult = "未完成",
                InspectionCount= sumModel.InspectionCount,
                Department=sumModel.MaterialSupplier,
                OpPerson=sumModel.OpPerson ,
                OpSign = "edit"
            };

            detailModel =  new InspectionFqcDetailModel()
            {
                OrderId = sumModel.OrderId,
                EquipmentId = sumModel.EquipmentId,
                MaterialCount = sumModel.MaterialInCount,
                InspecitonItem = sumModel.InspectionItem,
                InspectionAcceptCount = sumModel.AcceptCount,
                InspectionCount = sumModel.InspectionCount,
                InspectionRefuseCount = sumModel.RefuseCount,
                InspectionDate = DateTime.Now,
                InspectionItemDatas = sumModel.InspectionItemDatas,
                InspectionItemResult = sumModel.InspectionItemResult,
                InspectionItemStatus = sumModel.InspectionItemStatus,
                MaterialId = sumModel.MaterialId,
                MaterialInDate = sumModel.MaterialInDate,
                OpSign = "add",
                Memo = sumModel.Memo,
                OpPerson = sumModel.OpPerson,
                Id_Key = sumModel.Id_Key
            };
        }

       /// <summary>
       /// 存储主表信息
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
      private  OpResult storeInspectionMasterial(InspectionFqcMasterModel model)
        {
            return InspectionManagerCrudFactory.FqcMasterCrud.Store(model);
        }

        /// <summary>
        ///  存储副表信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult storeInspectionDetial(InspectionFqcDetailModel model)
        {
            if (InspectionManagerCrudFactory.FqcDetailCrud.GetFqcInspectionDetailModelBy(model.OrderId ,model.OrderIdNumber ,model.InspecitonItem))
            {
                model.OpSign = "edit";
            }
            return InspectionManagerCrudFactory.FqcDetailCrud.Store(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public InspectionFqcOrderIdModel FindFqcInspectionFqcOrderIdModel(string orderId)
        {
            var orderMaterialInfo = GetPuroductSupplierInfo(orderId).FirstOrDefault();
            double haveInspectionSumCount = GetFqcMasterHaveInspectionCountBy(orderId);
            InspectionFqcOrderIdModel returnModle = new InspectionFqcOrderIdModel()
            {
                OrderId=orderId,
                MaterialId= orderMaterialInfo.ProductID,
                MaterialName= orderMaterialInfo.ProductName,
                MaterialSpec=orderMaterialInfo.ProductStandard,
                MaterialDrawId=orderMaterialInfo.ProductDrawID,
                MaterialInCount=orderMaterialInfo.ProduceNumber,
                MaterialSupplier= orderMaterialInfo.ProductSupplier ,
                MaterialInDate=orderMaterialInfo.ProduceInDate,
                HaveInspectionSumCount = haveInspectionSumCount
            };
            return returnModle;
        }
        /// <summary>
        /// 得到工单数量的完成的状态字典
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        private Dictionary<int, string> GetOrderNumberStatusDictoanaryBy(string orderId)
        {
            Dictionary<int, string> ddd = new Dictionary<int, string>();
            var listdatas = GetFqcMasterOrderIdDatasBy(orderId);
            if ((listdatas == null || listdatas.Count <= 0)) return ddd;
            listdatas.ForEach(e => {
                if(ddd.Keys.Contains(e.OrderNumber))
                ddd.Add(e.OrderNumber, e.InspectionStatus);
            });
            return ddd;
        }
        
        /// <summary>
        /// 获取已经检验的数量
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        private double  GetFqcMasterHaveInspectionCountBy(string orderId)
        {
            var listdatas = GetFqcMasterOrderIdDatasBy(orderId);
            return (listdatas == null || listdatas.Count <=0)? 0: listdatas.Sum(e => e.InspectionCount);
        }
        private List<InspectionFqcMasterModel> GetFqcMasterModeListlBy(string MarterialId)
        {
            return InspectionManagerCrudFactory.FqcMasterCrud.GetFqcInspectionMasterListBy(MarterialId);
        }
        /// <summary>
        /// 判断是否按提正常还
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        private string GetJudgeInspectionMode(string inspectionClass,  string materialId)
        {
            ///3，比较 对比
            ///4，返回一个 转换的状态
            ///1,通过料号 和 抽检验项目  得到当前的最后一次抽检的状态
            string retrunstirng = "正常";
            var DetailModeList = GetFqcMasterModeListlBy(materialId); 
            if (DetailModeList == null || DetailModeList.Count <= 0) return retrunstirng;
            var currentStatus = DetailModeList.Last().InspectionMode;
            ///2，通当前状态 得到抽样规则 抽样批量  拒受数
            var modeSwithParameterList = InspectionManagerCrudFactory.InspectionModeSwithConfigCrud.GetInspectionModeSwithConfiglistBy(inspectionClass, currentStatus);
            if (modeSwithParameterList == null || modeSwithParameterList.Count <= 0) return retrunstirng;
            int sampleNumberVauleMin = modeSwithParameterList.FindAll(e => e.SwitchProperty == "SampleNumber").Select(e => e.SwitchVaule).Min();
            int AcceptNumberVauleMax = modeSwithParameterList.FindAll(e => e.SwitchProperty == "AcceptNumber").Select(e => e.SwitchVaule).Max();
            int sampleNumberVauleMax = modeSwithParameterList.FindAll(e => e.SwitchProperty == "SampleNumber").Select(e => e.SwitchVaule).Max();
            int AcceptNumberVauleMin = modeSwithParameterList.FindAll(e => e.SwitchProperty == "AcceptNumber").Select(e => e.SwitchVaule).Min();
            var getNumber = DetailModeList.Take(sampleNumberVauleMax).Count(e => e.InspectionResult == "NG");
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
                    if (getNumber >= AcceptNumberVauleMax) retrunstirng = "加严";
                    else retrunstirng = "正常";
                    break;
                default:
                    retrunstirng = "正常";
                    break;
            }
            return retrunstirng;
        }
        #endregion

    }
}
