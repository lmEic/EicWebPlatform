using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.Erp.Domain.QuantityModel;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
    public class InspectionFqcDataGather : InspectionDateGatherManageBase
    {

        private static Dictionary<string, List<InspectionFqcItemConfigModel>> BufferInspectionItemDatas = new Dictionary<string, List<InspectionFqcItemConfigModel>>();
        #region  对抽检项目 及 需要录入数据的数量 以后编改的接口 
        /// <summary>
        /// 需要录入的项目
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderIdNumber"></param>
        /// <param name="materialId"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public int FqcNeedInputDataCountBy(string orderId, int orderIdNumber, string materialId, string InspectionDataGatherType, int defaultValue)
        {

            return defaultValue;
        }
        /// <summary>
        ///  依条件   加载所有的测试项目
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<InspectionFqcItemConfigModel> GetFqcNeedInspectionItemDatas(string materialId)
        {
            try
            {
                if (!BufferInspectionItemDatas.Keys.Contains(materialId))
                    BufferInspectionItemDatas.Add(materialId, InspectionManagerCrudFactory.FqcItemConfigCrud.FindFqcInspectionItemConfigDatasBy(materialId));
                return BufferInspectionItemDatas[materialId];

            }
            catch (Exception ex)
            {
                return null;
                throw new Exception(ex.InnerException.Message);
            }

        }

        #endregion
        #region LoadClass 
        public FqcDetailDatasGather DetailDatasGather
        {
            get { return OBulider.BuildInstance<FqcDetailDatasGather>(); }
        }

        public FqcMasterDatasGather MasterDatasGather
        {
            get { return OBulider.BuildInstance<FqcMasterDatasGather>(); }
        }

        #endregion


        #region  Method
        /// <summary>
        ///  得到FQC工单信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public InspectionFqcOrderIdModel GetFqcInspectionFqcOrderIdInfoBy(string orderId)
        {
            try
            {
                var orderMaterialInfoList = this.GetPuroductSupplierInfo(orderId);
                if (orderMaterialInfoList == null || orderMaterialInfoList.Count <= 0)
                    return new InspectionFqcOrderIdModel();
                /// 一个制令单 对应一个物料 
                var orderMaterialInfo = orderMaterialInfoList[0];
                ///是否加载有检验配置项目
                bool isHaveItemConfig = InspectionManagerCrudFactory.FqcItemConfigCrud.IsExistFqcConfigmaterailId(orderMaterialInfo.ProductID);
                /// 统计已经检验的总数量
                double haveInspectionSumCount = MasterDatasGather.GetFqcMasterHaveInspectionCountBy(orderId);
                InspectionFqcOrderIdModel returnModle = new InspectionFqcOrderIdModel()
                {
                    OrderId = orderId,
                    MaterialId = orderMaterialInfo.ProductID,
                    MaterialName = orderMaterialInfo.ProductName,
                    MaterialSpec = orderMaterialInfo.ProductStandard,
                    MaterialDrawId = orderMaterialInfo.ProductDrawID,
                    MaterialInCount = orderMaterialInfo.ProduceNumber,
                    MaterialSupplier = orderMaterialInfo.ProductSupplier,
                    MaterialInDate = orderMaterialInfo.ProduceInDate,
                    HaveInspectionSumCount = haveInspectionSumCount,
                    IsHaveItemConfig = isHaveItemConfig,
                };
                return returnModle;

            }
            catch (Exception ex)
            {
                return new InspectionFqcOrderIdModel();
                throw new Exception(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// 生成FQC检验项目所有的信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<InspectionItemDataSummaryVM> BuildingFqcInspectionSummaryDatasBy(string orderId, double sampleCount)
        {
            try
            {
                List<InspectionItemDataSummaryVM> returnDatas = null;
                if (sampleCount <= 0) return returnDatas;
                ///一个工单 对应一个料号，有工单就是料号
                var orderMaterialInfoList = this.GetPuroductSupplierInfo(orderId);
                if (orderMaterialInfoList == null || orderMaterialInfoList.Count <= 0) return returnDatas;
                var orderMaterialInfo = orderMaterialInfoList[0];
                ///得到需要检验的项目
                var fqcNeedInspectionsItemdatas = GetFqcNeedInspectionItemDatas(orderMaterialInfo.ProductID);

                if (fqcNeedInspectionsItemdatas == null || fqcNeedInspectionsItemdatas.Count <= 0) return returnDatas;

                /// Master表中得到序号+1
                int orderIdNumber = 0;
                var FqcHaveInspectionAllOrderiDDatas = MasterDatasGather.GetFqcMasterOrderIdDatasBy(orderId);
                if (FqcHaveInspectionAllOrderiDDatas == null || FqcHaveInspectionAllOrderiDDatas.Count <= 0) orderIdNumber = 1;
                else orderIdNumber = FqcHaveInspectionAllOrderiDDatas.Max(e => e.OrderIdNumber) + 1;
                ///处理数据
                returnDatas = HandleBuildingSummaryDataLabelModel(sampleCount, orderIdNumber, orderMaterialInfo, fqcNeedInspectionsItemdatas);
                ///
                if (returnDatas == null || returnDatas.Count <= 0) return returnDatas;
                //存诸 表
                returnDatas.ForEach(m => { StoreFqcDataGather(m); });
                return returnDatas;
            }
            catch (Exception ex)
            {
                return new List<InspectionItemDataSummaryVM>();
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// 得到FQC检验项目所有的信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderIdNumber"></param>
        /// <returns></returns>
        public List<InspectionItemDataSummaryVM> FindFqcInspectionSummaryDataBy(string orderId, int orderIdNumber)
        {
            try
            {
                var orderMaterialInfoList = this.GetPuroductSupplierInfo(orderId);
                if (orderMaterialInfoList == null || orderMaterialInfoList.Count <= 0)
                    return new List<InspectionItemDataSummaryVM>();
                var orderMaterialInfo = orderMaterialInfoList[0];
                ///得到FQC已经检验详表
                var fqcHaveInspectionDatas = DetailDatasGather.GetFqcInspectionDetailDatasBy(orderId, orderIdNumber);
                if (fqcHaveInspectionDatas == null || fqcHaveInspectionDatas.Count == 0)
                    return new List<InspectionItemDataSummaryVM>();
                ///得到需要检验的项目
                var fqcInspectionsItemdatas = GetFqcNeedInspectionItemDatas(orderMaterialInfo.ProductID);
                if (fqcInspectionsItemdatas == null || fqcInspectionsItemdatas.Count <= 0)
                    return new List<InspectionItemDataSummaryVM>();
                return HandleFindSummaryDataLabelModel(orderMaterialInfo, fqcHaveInspectionDatas, fqcInspectionsItemdatas);
            }
            catch (Exception ex)
            {
                return new List<InspectionItemDataSummaryVM>();
                throw new Exception(ex.InnerException.Message);

            }
        }
        /// <summary>
        /// 存储收集数据 （二次调用，一次是新建存，一次是编辑）
        /// </summary>
        /// <param name="sumModel"></param>
        /// <returns></returns>
        public OpResult StoreFqcDataGather(InspectionItemDataSummaryVM sumModel)
        {
            var returnOpResult = new OpResult("采集数据模型不能为NULL", false);
            InspectionFqcMasterModel masterModel = null;
            InspectionFqcDetailModel detailModel = null;
            ///先排除总表不能为空
            GetMasterAndDetailModelFrom(sumModel, out masterModel, out detailModel);
            if (detailModel == null || masterModel == null) return new OpResult("表单数据为空，保存失败", false);
            /// 先保存副表  再更新主表信息
            returnOpResult = DetailDatasGather.storeInspectionDetial(detailModel, sumModel.SiteRootPath);
            if (!returnOpResult.Result) return returnOpResult;
            ///如果只是上传文档 不用更新  Masterial
            if (sumModel.OpSign != OpMode.UploadFile)
                returnOpResult = MasterDatasGather.storeInspectionMasterial(masterModel);
            return returnOpResult;
        }
        #endregion





        #region  对内处理 Private

        /// <summary>
        /// 新建FQC总表
        /// </summary>
        /// <param name="sampleCount"></param>
        /// <param name="orderIdNumber"></param>
        /// <param name="orderMaterialInfo"></param>
        /// <param name="fqcNeedInspectionsItemdatas"></param>
        /// <returns></returns>
        private List<InspectionItemDataSummaryVM> HandleBuildingSummaryDataLabelModel(double sampleCount, int orderIdNumber, MaterialModel orderMaterialInfo, List<InspectionFqcItemConfigModel> fqcNeedInspectionsItemdatas)
        {
            try
            {
                List<InspectionItemDataSummaryVM> returnList = new List<InspectionItemDataSummaryVM>();
                if (orderMaterialInfo == null) return returnList;
                int i = 0;
                fqcNeedInspectionsItemdatas.ForEach(m =>
                {
                    i++;
                    ///得到检验方式 “正常” “放宽” “加严”
                    var inspectionMode = GetJudgeInspectionMode("FQC", m.MaterialId);
                    ///得到检验方案
                    var inspectionModeConfigModelData = this.GetInspectionModeConfigDataBy(m.InspectionLevel, m.InspectionAQL, sampleCount, inspectionMode);

                    ///初始化 综合模块
                    var model = new InspectionItemDataSummaryVM()
                    {
                        OrderId = orderMaterialInfo.OrderID,
                        OrderIdNumber = orderIdNumber,
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
                        ProductDepartment = m.ProductDepartment,
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
                        ClassType = string.Empty,
                        InStorageOrderId = "入库单",
                        InspectionItemSumCount = i,
                        InspectionNGCount = 0,
                        InspectionDataTimeRegion = "录入时间段",
                        Memo = string.Empty,
                        OpPerson = "StartSetValue",
                        NeedFinishDataNumber = 0,
                        HaveFinishDataNumber = 0,
                        OpSign = OpMode.Add,
                        InspectionItemResult = "未完工"
                    };
                    ///如果没有得到抽检验方案 侧为空
                    if (inspectionModeConfigModelData != null)
                    {
                        model.InspectionMode = inspectionModeConfigModelData.InspectionMode;
                        model.InspectionLevel = inspectionModeConfigModelData.InspectionLevel;
                        model.InspectionAQL = inspectionModeConfigModelData.InspectionAQL;
                        model.InspectionCount = inspectionModeConfigModelData.InspectionCount;
                        model.AcceptCount = inspectionModeConfigModelData.AcceptCount;
                        model.RefuseCount = inspectionModeConfigModelData.RefuseCount;
                        //需要录入的数据个数 暂时为抽样的数量
                        model.NeedFinishDataNumber = FqcNeedInputDataCountBy(model.OrderId, model.OrderIdNumber, model.MaterialId, model.InspectionDataGatherType, inspectionModeConfigModelData.InspectionCount);
                    }
                    returnList.Add(model);
                });
                return returnList;
            }
            catch (Exception ex)
            {
                return new List<InspectionItemDataSummaryVM>();
                throw new Exception(ex.InnerException.Message);
            }
        }
        /// <summary>
        /// 处理查找FQC的详细数据
        /// </summary>
        /// <param name="orderMaterialInfo"></param>
        /// <param name="fqcHaveInspectionDatas"></param>
        /// <param name="fqcInspectionsItemdatas"></param>
        /// <returns></returns>
        private List<InspectionItemDataSummaryVM> HandleFindSummaryDataLabelModel(MaterialModel orderMaterialInfo, List<InspectionFqcDetailModel> fqcHaveInspectionDatas, List<InspectionFqcItemConfigModel> fqcInspectionsItemdatas)
        {
            try
            {
                List<InspectionItemDataSummaryVM> returnList = new List<InspectionItemDataSummaryVM>();
                ///如果没有此测试项目直接返回
                if (fqcInspectionsItemdatas == null || fqcInspectionsItemdatas.Count <= 0) return returnList;
                string inspectionDataTimeRegion = DateTime.Now.Date.ToShortDateString();
                int inspectionItemSumCount = fqcInspectionsItemdatas.Count;
                /// 对每一个已经检验的项目 进行加工处理
                fqcHaveInspectionDatas.ForEach(m =>
                {
                    ///初始化 综合模块
                    var model = new InspectionItemDataSummaryVM()
                    {
                        OrderId = orderMaterialInfo.OrderID,
                        OrderIdNumber = m.OrderIdNumber,
                        MaterialId = orderMaterialInfo.ProductID,
                        MaterialName = orderMaterialInfo.ProductName,
                        MaterialSpec = orderMaterialInfo.ProductStandard,
                        MaterialSupplier = orderMaterialInfo.ProductSupplier,
                        MaterialDrawId = orderMaterialInfo.ProductDrawID,
                        MaterialInDate = orderMaterialInfo.ProduceInDate,
                        MaterialInCount = orderMaterialInfo.ProduceNumber,
                        MaterialCount = m.MaterialCount,
                        InspectionItem = m.InspectionItem,
                        EquipmentId = m.EquipmentId,
                        DocumentPath = m.DocumentPath,
                        FileName = m.FileName,
                        Memo = m.Memo,
                        InspectionItemStatus = m.InspectionItemStatus,
                        ///检验方法
                        InspectionMethod = m.InspectionMethod,
                        InspectionItemDatas = m.InspectionItemDatas,
                        ///需要完成数量 得于 检验数
                        NeedFinishDataNumber = m.NeedPutInDataCount,
                        InspectionItemResult = m.InspectionItemResult,
                        ProductDepartment = m.ProductDepartment,
                        InspectionNGCount = m.InspectionNGCount,
                        ClassType = m.ClassType,
                        InsptecitonItemIsFinished = false,
                        /// 分析已完成的数据的数量
                        HaveFinishDataNumber = this.DoHaveFinishDataNumber(m.InspectionItemResult, m.InspectionItemDatas, (int)m.InspectionCount),
                        InStorageOrderId = m.InStorageOrderId,
                        InspectionItemSumCount = inspectionItemSumCount,
                        InspectionDataTimeRegion = inspectionDataTimeRegion,
                        InspectionMode = string.Empty,
                        InspectionLevel = string.Empty,
                        InspectionAQL = string.Empty,
                        InspectionCount = 0,
                        AcceptCount = 0,
                        RefuseCount = 0,
                        InspectionDataGatherType = string.Empty,
                        SizeLSL = 0,
                        SizeUSL = 0,
                        SizeMemo = string.Empty,
                        Id_Key = m.Id_Key
                    };
                    /// 依据检验项目得到相应的数值
                    var InspectionsItemdata = fqcInspectionsItemdatas.FirstOrDefault(e => e.InspectionItem == m.InspectionItem);
                    if (InspectionsItemdata != null)
                    {
                        model.InspectionDataGatherType = InspectionsItemdata.InspectionDataGatherType;
                        model.SizeLSL = InspectionsItemdata.SizeLSL;
                        model.SizeUSL = InspectionsItemdata.SizeUSL;
                        model.SizeMemo = InspectionsItemdata.SizeMemo;


                        ///得到检验方案
                        var inspectionModeConfigModelData = this.GetInspectionModeConfigDataBy(InspectionsItemdata.InspectionLevel, InspectionsItemdata.InspectionAQL, m.MaterialCount, m.InspectionMode);
                        if (inspectionModeConfigModelData != null)
                        {
                            ///如果检验方法 不为空 侧不需要赋值
                            if (model.InspectionMode == string.Empty)
                                model.InspectionMode = inspectionModeConfigModelData.InspectionMode;
                            model.InspectionLevel = inspectionModeConfigModelData.InspectionLevel;
                            model.InspectionAQL = inspectionModeConfigModelData.InspectionAQL;
                            model.InspectionCount = inspectionModeConfigModelData.InspectionCount;
                            model.AcceptCount = inspectionModeConfigModelData.AcceptCount;
                            model.RefuseCount = inspectionModeConfigModelData.RefuseCount;
                            //需要录入的数据个数 暂时为抽样的数量
                            //model.NeedFinishDataNumber = inspectionModeConfigModelData.InspectionCount;
                        }
                    }
                    returnList.Add(model);
                });
                return returnList;

            }
            catch (Exception ex)
            {
                return new List<InspectionItemDataSummaryVM>();
                throw new Exception(ex.InnerException.Message);

            }


        }
        /// <summary>
        /// 总表分解为分表
        /// </summary>
        /// <param name="sumModel"></param>
        /// <param name="masterModel"></param>
        /// <param name="detailModel"></param>
        private void GetMasterAndDetailModelFrom(InspectionItemDataSummaryVM sumModel, out InspectionFqcMasterModel masterModel, out InspectionFqcDetailModel detailModel)
        {
            try
            {
                if (sumModel == null)
                { masterModel = null; detailModel = null; }
                masterModel = new InspectionFqcMasterModel();
                OOMaper.Mapper<InspectionItemDataSummaryVM, InspectionFqcMasterModel>(sumModel, masterModel);
                masterModel.InspectionItemCount = sumModel.InspectionItemSumCount;
                masterModel.InspectionItems = sumModel.InspectionItem;
                masterModel.FinishDate = DateTime.Now.Date;
                masterModel.InspectionStatus = "未完工";
                masterModel.InspectionResult = "未完工";
                masterModel.InspectionCount = sumModel.MaterialCount;
                detailModel = new InspectionFqcDetailModel();
                OOMaper.Mapper<InspectionItemDataSummaryVM, InspectionFqcDetailModel>(sumModel, detailModel);
                detailModel.OrderIdCount = sumModel.MaterialInCount;
                detailModel.InspectionAcceptCount = sumModel.AcceptCount;
                detailModel.InspectionRefuseCount = sumModel.RefuseCount;
                detailModel.InspectionDate = DateTime.Now.ToDate();
                detailModel.NeedPutInDataCount = (int)sumModel.NeedFinishDataNumber;
            }
            catch (Exception ex)
            {
                masterModel = null; detailModel = null;
                throw new Exception(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// 判断是否按提正常还
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        private string GetJudgeInspectionMode(string inspectionClass, string materialId)
        {
            ///3，比较 对比
            ///4，返回一个 转换的状态
            ///1,通过料号 和 抽检验项目  得到当前的最后一次抽检的状态
            string retrunstirng = "正常";
            var DetailModeList = MasterDatasGather.GetFqcMasterModeListlBy(materialId);
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
