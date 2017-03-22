using Lm.Eic.App.DomainModel.Bpm.Quanity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
    public class InspectionFqcDataGather:InspectionDateGatherManageBase
    {

        private List<InspectionFqcDetailModel> GetFqcInspectionDetailModeListlBy(string orderIdAndNumber, string InspectionItem)
        {
            return InspectionManagerCrudFactory.FqcDetailCrud.GetFqcInspectionDetailModelListBy(orderIdAndNumber, InspectionItem);
        }

        private List<InspectionFqcDetailModel> GetFqcDetailInspectionAllOrderIDDatasBy(string OrderId)
        {
            return InspectionManagerCrudFactory.FqcDetailCrud.GetFqcInspectionDetailModelListBy(OrderId);
        }


       public  List<InspectionFqcMasterModel> GetFqcMasterInspectionAllOrderIdDatasBy(string orderId)
        {
            return InspectionManagerCrudFactory.FqcMasterCrud.GetFqcInspectionMasterModelListBy(orderId);
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

        /// <summary>
        /// 生成IQC检验项目所有的信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<InspectionIqcItemDataSummaryLabelModel> BuildingFqcInspectionItemDataSummaryLabelListBy(string orderId, double inspectionCount)
        {
            List<InspectionIqcItemDataSummaryLabelModel> returnList = new List<InspectionIqcItemDataSummaryLabelModel>();
            ///一个工单 对应一个料号，有工单就是料号
            var orderMaterialInfo = GetPuroductSupplierInfo(orderId).FirstOrDefault();
            if (orderMaterialInfo == null) return returnList;
            ///得到需要检验的项目
            var fqcNeedInspectionsItemdatas = getFqcNeedInspectionItemDatas(orderMaterialInfo.ProductID);
            if (fqcNeedInspectionsItemdatas == null || fqcNeedInspectionsItemdatas.Count <= 0) return returnList;
          
            /// Master表中得到序号
            int  orderIdNumber = 0;
            var FqcHaveInspectionAllOrderiDDatas = GetFqcMasterInspectionAllOrderIdDatasBy(orderId);
            if (FqcHaveInspectionAllOrderiDDatas == null || fqcNeedInspectionsItemdatas.Count <= 0) orderIdNumber = 1;
            else orderIdNumber = FqcHaveInspectionAllOrderiDDatas.Max(e=>e.OrderNumber);
           ///保存单头数据
            fqcNeedInspectionsItemdatas.ForEach(m =>
            {
                ///得到检验方式 “正常” “放宽” “加严”
                var inspectionMode = GetJudgeInspectionMode("FQC", m.MaterialId, m.InspectionItem);
                ///得到检验方案
                var inspectionModeConfigModelData = this.GetInspectionModeConfigDataBy(m.InspectionLevel,m.InspectionAQL, inspectionCount, inspectionMode);
               
                ///初始化 综合模块
                var model = new InspectionIqcItemDataSummaryLabelModel()
                {
                    OrderId = orderId,
                    Number= orderIdNumber,
                    MaterialId = orderMaterialInfo.ProductID,
                    MaterialName = orderMaterialInfo.ProductName,
                    MaterialSpec = orderMaterialInfo.ProductStandard,
                    MaterialSupplier = orderMaterialInfo.ProductSupplier,
                    MaterialDrawId = orderMaterialInfo.ProductDrawID,
                    MaterialInDate = orderMaterialInfo.ProduceInDate,
                    MaterialInCount = orderMaterialInfo.ProduceNumber,
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public InspectionFqcOrderIdModel FindFqcInspectionFqcOrderIdModel(string orderId)
        {
            var orderMaterialInfo = GetPuroductSupplierInfo(orderId).FirstOrDefault();
            double haveInspectionSumCount = GetFqcMasterHaveInspectionCountBy(orderId);
            var orderNumberList = GetFqcMasterHaveInspectionOrderNumberBy(orderId);
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
                HaveInspectionOrderNumbers= orderNumberList,
                HaveInspectionSumCount = haveInspectionSumCount
            };
            return returnModle;
        }

        

        private double  GetFqcMasterHaveInspectionCountBy(string orderId)
        {
            var listdatas = GetFqcMasterInspectionAllOrderIdDatasBy(orderId);
            return (listdatas == null || listdatas.Count <=0)? 0: listdatas.Sum(e => e.InspectionCount);
        }
        private List<double> GetFqcMasterHaveInspectionOrderNumberBy(string orderId)
        {
            List<double> returNumber = new List<double>();
               var listdatas = GetFqcMasterInspectionAllOrderIdDatasBy(orderId);
            if (listdatas == null || listdatas.Count <= 0) return returNumber;

            listdatas.ForEach(e => {
                returNumber.Add(e.OrderNumber);
            });
            return returNumber;
        }




        private List<InspectionFqcMasterModel> GetFqcMasterModeListlBy(string MarterialId, string inspecitonItem)
        {
            return null;
        }



        /// <summary>
        /// 判断是否按提正常还
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        private string GetJudgeInspectionMode(string inspectionClass, string orderIdNumber, string inspecitonItem)
        {
            ///3，比较 对比
            ///4，返回一个 转换的状态
            ///1,通过料号 和 抽检验项目  得到当前的最后一次抽检的状态
            string retrunstirng = "正常";
            var DetailModeList = GetFqcMasterModeListlBy( orderIdNumber, inspecitonItem); 
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
                    else
                    { ///加严的数量
                        int getTheNumber = DetailModeList.Take(sampleNumberVauleMin).Count(e => e.InspectionResult == "NG");
                        retrunstirng = (getTheNumber >= AcceptNumberVauleMax) ? "加严" : currentStatus;
                    }
                    break;
                default:
                    break;
            }
            return retrunstirng;
        }
      
      
    }
}
