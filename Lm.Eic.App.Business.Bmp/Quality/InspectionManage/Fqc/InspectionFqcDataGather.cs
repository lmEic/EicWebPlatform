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
        #region  对抽检项目 及 需要录入数据的数量 以后编改的接口 
        /// <summary>
        /// 需要录入的项目
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderIdNumber"></param>
        /// <param name="materialId"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public int FqcNeedInputDataCountBy(string orderId, int orderIdNumber, string materialId, string InspectionDataGatherType, int defaultValue )
        {
            if (InspectionDataGatherType == "D") return 1;
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
                if (InspectionItemDatas==null)
                    InspectionItemDatas = InspectionManagerCrudFactory.FqcItemConfigCrud.FindFqcInspectionItemConfigDatasBy(materialId);
                if (InspectionItemDatas == null || InspectionItemDatas.Count <= 0)
                    return new List<InspectionFqcItemConfigModel>();
                return InspectionItemDatas;

            }
            catch (Exception ex)
            {
                return null;
                throw new Exception(ex.InnerException .Message ) ;
            }
           
        }
        private List<InspectionFqcItemConfigModel> InspectionItemDatas = null;
        #endregion

        #region  对外控件  Public
        /// <summary>
        ///  得到FQC工单信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public InspectionFqcOrderIdModel GetFqcInspectionFqcOrderIdInfoBy(string orderId)
        {
            try
            {
                var orderMaterialInfoList =this. GetPuroductSupplierInfo(orderId);
                if (orderMaterialInfoList == null || orderMaterialInfoList.Count <= 0)
                    return new InspectionFqcOrderIdModel();
                /// 一个制令单 对应一个物料 
                var orderMaterialInfo = orderMaterialInfoList[0];
                /// 统计已经检验的总数量
               double haveInspectionSumCount = GetFqcMasterHaveInspectionCountBy(orderId);
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
                    HaveInspectionSumCount = haveInspectionSumCount
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
        ///  得到Mater列表数据
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<InspectionFqcMasterModel> GetFqcMasterOrderIdDatasBy(string orderId)
        {
            try
            {
              return InspectionManagerCrudFactory.FqcMasterCrud.GetFqcInspectionMasterModelListBy(orderId);
            }
            catch (Exception ex)
            {return null; throw new Exception(ex.InnerException.Message);} 
        }
        /// <summary>
        /// 生成FQC检验项目所有的信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<InspectionItemDataSummaryLabelModel> BuildingFqcInspectionSummaryDataBy(string orderId, double sampleCount)
        {
            try
            {
                List<InspectionItemDataSummaryLabelModel> returnList =null;
                ///一个工单 对应一个料号，有工单就是料号
                var orderMaterialInfoList = GetPuroductSupplierInfo(orderId);
                if (orderMaterialInfoList == null || orderMaterialInfoList.Count <= 0) return new List<InspectionItemDataSummaryLabelModel>();
                var orderMaterialInfo = orderMaterialInfoList[0];
                ///得到需要检验的项目
                var fqcNeedInspectionsItemdatas = GetFqcNeedInspectionItemDatas(orderMaterialInfo.ProductID);

                if (fqcNeedInspectionsItemdatas == null || fqcNeedInspectionsItemdatas.Count <= 0) return new List<InspectionItemDataSummaryLabelModel>();

                /// Master表中得到序号+1
                int orderIdNumber = 0;
                var FqcHaveInspectionAllOrderiDDatas = GetFqcMasterOrderIdDatasBy(orderId);
                if (FqcHaveInspectionAllOrderiDDatas == null || FqcHaveInspectionAllOrderiDDatas.Count <= 0) orderIdNumber = 1;
                else orderIdNumber = FqcHaveInspectionAllOrderiDDatas.Max(e => e.OrderIdNumber)+1;
                ///处理数据
                returnList = HandleBuildingSummaryDataLabelModel(sampleCount, orderIdNumber, orderMaterialInfo, fqcNeedInspectionsItemdatas);
                ///
                if (returnList == null||returnList.Count <=0) return returnList;
                //存诸 表
                returnList.ForEach(m => {StoreFqcDataGather(m); });
                return returnList;
            }
            catch (Exception ex)
            {
                return new List<InspectionItemDataSummaryLabelModel>();
                throw new Exception(ex.InnerException.Message);
            }

        }
        /// <summary>
        /// 得到FQC检验项目所有的信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderIdNumber"></param>
        /// <returns></returns>
        public List<InspectionItemDataSummaryLabelModel> FindFqcInspectionSummaryDataBy(string orderId, int orderIdNumber)
        {
            try
            {
                var orderMaterialInfoList = GetPuroductSupplierInfo(orderId);
                if (orderMaterialInfoList == null || orderMaterialInfoList.Count <= 0)
                    return new List<InspectionItemDataSummaryLabelModel>();
                var orderMaterialInfo = orderMaterialInfoList[0];
                ///得到FQC已经检验详表
                var fqcHaveInspectionDatas = GetFqcInspectionDetailModeListlBy(orderId, orderIdNumber);
                if (fqcHaveInspectionDatas == null || fqcHaveInspectionDatas.Count <= 0)
                    return new List<InspectionItemDataSummaryLabelModel>(); 
                ///得到需要检验的项目
                var fqcInspectionsItemdatas = GetFqcNeedInspectionItemDatas(orderMaterialInfo.ProductID);
                if (fqcInspectionsItemdatas == null || fqcInspectionsItemdatas.Count <= 0)
                    return new List<InspectionItemDataSummaryLabelModel>();
                return HandleFindSummaryDataLabelModel(orderMaterialInfo, fqcHaveInspectionDatas, fqcInspectionsItemdatas);
            }
            catch (Exception ex)
            {
                return new List<InspectionItemDataSummaryLabelModel>();
                throw new Exception(ex.InnerException.Message);
               
            }
        }
        /// <summary>
        /// 存储收集数据
        /// </summary>
        /// <param name="sumModel"></param>
        /// <returns></returns>
        public OpResult StoreFqcDataGather(InspectionItemDataSummaryLabelModel sumModel, string rootPath="")
        {
            var returnOpResult = new OpResult("数据为空，保存失败", false);
            if (sumModel == null) return returnOpResult;
            InspectionFqcMasterModel masterModel = null;
            InspectionFqcDetailModel detailModel = null;
            ///先排除总表不能为空
            SumDataToConvterData(sumModel, rootPath, out masterModel, out detailModel);
            if (detailModel == null || masterModel == null) return new OpResult("表单数据为空，保存失败", false);
            // 先保存详细表  再更新主表信息
            returnOpResult = storeInspectionDetial(detailModel);
            if (!returnOpResult.Result ) return returnOpResult;
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
        /// 新建FQC总表
        /// </summary>
        /// <param name="sampleCount"></param>
        /// <param name="orderIdNumber"></param>
        /// <param name="orderMaterialInfo"></param>
        /// <param name="fqcNeedInspectionsItemdatas"></param>
        /// <returns></returns>
        private List<InspectionItemDataSummaryLabelModel> HandleBuildingSummaryDataLabelModel(double sampleCount, int orderIdNumber, MaterialModel orderMaterialInfo, List<InspectionFqcItemConfigModel> fqcNeedInspectionsItemdatas)
        {
            try
            {
                List<InspectionItemDataSummaryLabelModel> returnList = new List<InspectionItemDataSummaryLabelModel>();
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
                    var model = new InspectionItemDataSummaryLabelModel()
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
                        Department=m.ProductDepartment,
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
                        ClassType="白班",
                        InStorageOrderId="入库单",
                        InspectionItemSumCount =i,
                        InspectionNGCount=0,
                        InspectionDataTimeRegion="录入时间段",
                        Memo=string.Empty,
                        OpPerson= "StartSetValue",
                        NeedFinishDataNumber = 0,
                        HaveFinishDataNumber = 0,
                        InspectionItemResult = string.Empty
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
                return new List<InspectionItemDataSummaryLabelModel>();
                throw new Exception (ex.InnerException.Message);
            }
        }
        /// <summary>
        /// 处理查找FQC的详细数据
        /// </summary>
        /// <param name="orderMaterialInfo"></param>
        /// <param name="fqcHaveInspectionDatas"></param>
        /// <param name="fqcInspectionsItemdatas"></param>
        /// <returns></returns>
        private List<InspectionItemDataSummaryLabelModel> HandleFindSummaryDataLabelModel(MaterialModel orderMaterialInfo, List<InspectionFqcDetailModel> fqcHaveInspectionDatas, List<InspectionFqcItemConfigModel> fqcInspectionsItemdatas)
        {
            try
            {
                List<InspectionItemDataSummaryLabelModel> returnList = new List<InspectionItemDataSummaryLabelModel>();
                ///如果没有此测试项目直接返回
                if (fqcInspectionsItemdatas == null || fqcInspectionsItemdatas.Count <= 0) return returnList;
                string inspectionDataTimeRegion = DateTime.Now.Date.ToShortDateString();
                int inspectionItemSumCount = fqcInspectionsItemdatas.Count;
                /// 对每一个已经检验的项目 进行加工处理
                fqcHaveInspectionDatas.ForEach(m =>
                {
                    ///初始化 综合模块
                    var model = new InspectionItemDataSummaryLabelModel()
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
                        MaterialCount =m.MaterialCount ,
                        InspectionItem = m.InspectionItem,
                        EquipmentId = m.EquipmentId,
                        Memo = m.Memo,
                        InspectionItemStatus = m.InspectionItemStatus,
                        ///检验方法
                        InspectionMethod = m.InspectionMethod,
                        InspectionItemDatas = m.InspectionItemDatas,
                        ///需要完成数量 得于 检验数
                        NeedFinishDataNumber =m.NeedPutInDataCount,
                        InspectionItemResult = m.InspectionItemResult,
                        Department = m.Department,
                        InspectionNGCount = m.InspectionNGCount,
                        ClassType = m.ClassType,
                        InsptecitonItemIsFinished = false,
                        /// 分析已完成的数据的数量
                        HaveFinishDataNumber = this.DoHaveFinishDataNumber(m.InspectionItemResult, m.InspectionItemDatas,(int) m.InspectionCount),

                       
                        InStorageOrderId=m.InStorageOrderId,
                        InspectionItemSumCount= inspectionItemSumCount,

                        InspectionDataTimeRegion = inspectionDataTimeRegion,

                        InspectionMode = string.Empty,
                        InspectionLevel = string.Empty,
                        InspectionAQL = string.Empty,
                        InspectionCount = 0,
                        AcceptCount = 0,
                        RefuseCount = 0,

                        InspectionDataGatherType = string.Empty,
                        SizeLSL =0,
                        SizeUSL=0,
                        SizeMemo=string.Empty ,
                        Id_Key=m.Id_Key 
                    };
                    /// 依据检验项目得到相应的数值
                    var InspectionsItemdata = fqcInspectionsItemdatas.FirstOrDefault(e => e.InspectionItem == m.InspectionItem);
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
                    returnList.Add(model);
                });
                return returnList;

            }
            catch (Exception ex)
            {
                return new List<InspectionItemDataSummaryLabelModel>();
                throw new Exception(ex.InnerException.Message);

            }


        }
        /// <summary>
        /// 总表分解为分表
        /// </summary>
        /// <param name="sumModel"></param>
        /// <param name="masterModel"></param>
        /// <param name="detailModel"></param>
        private void SumDataToConvterData(InspectionItemDataSummaryLabelModel sumModel, string rootPath, out InspectionFqcMasterModel masterModel, out InspectionFqcDetailModel detailModel)
        {
            try
            {
                if (sumModel == null) { masterModel = null; detailModel = null; }
                ///   DocumentPath
                
                masterModel = new InspectionFqcMasterModel()
                {
                    OrderId = sumModel.OrderId,
                    OrderIdNumber = sumModel.OrderIdNumber,
                    MaterialId = sumModel.MaterialId,
                    MaterialName = sumModel.MaterialName,
                    MaterialSpec = sumModel.MaterialSpec,
                    MaterialDrawId = sumModel.MaterialDrawId,
                    MaterialSupplier = sumModel.MaterialSupplier,
                    InspectionItemCount = sumModel.InspectionItemSumCount,
                    ///订单数量
                    MaterialCount = sumModel.MaterialInCount,
                    MaterialInDate = sumModel.MaterialInDate,
                    InspectionMode = sumModel.InspectionMode,
                    InspectionItems = sumModel.InspectionItem,
                    FinishDate = DateTime.Now.Date,
                    InspectionStatus = "待审核",
                    InspectionResult = "未完工",
                    ///检验批次数量
                    InspectionCount = sumModel.MaterialCount ,
                    Department = sumModel.Department,
                    OpPerson = sumModel.OpPerson,
                    OpSign = "add"
                };

                detailModel = new InspectionFqcDetailModel()
                {
                    OrderId = sumModel.OrderId,
                    OrderIdNumber = sumModel.OrderIdNumber,
                    EquipmentId = sumModel.EquipmentId,
                    OrderIdCount = sumModel.MaterialInCount,
                    InspectionMethod=sumModel.InspectionMethod,
                    ///物料批次数量
                    MaterialCount=sumModel.MaterialCount,
                    InspectionItem = sumModel.InspectionItem,
                    InspectionAcceptCount = sumModel.AcceptCount,
                    InspectionCount = sumModel.InspectionCount,
                    InspectionRefuseCount = sumModel.RefuseCount,
                    InspectionDate = DateTime.Now,
                    InspectionItemDatas = sumModel.InspectionItemDatas,
                    InspectionItemResult = sumModel.InspectionItemResult,
                    InspectionItemStatus = sumModel.InspectionItemStatus,
                    MaterialId = sumModel.MaterialId,
                    MaterialInDate = sumModel.MaterialInDate,
                    InspectionMode = sumModel.InspectionMode,
                    NeedPutInDataCount=sumModel.NeedFinishDataNumber ,
                    ///
                    DocumentPath=sumModel .DocumentPath ,
                    FileName=sumModel.FileName,
                    Memo = sumModel.Memo,
                    ClassType = sumModel.ClassType,
                    Department = sumModel.Department,
                    InspectionDataTimeRegion = sumModel.InspectionDataTimeRegion,
                    InStorageOrderId = sumModel.InStorageOrderId,
                    InspectionNGCount = sumModel.InspectionNGCount,
                    OpSign = "add",
                    OpPerson = sumModel.OpPerson
                };

            }
            catch (Exception ex)
            {
                masterModel = null; detailModel = null;
                throw new  Exception(ex.InnerException.Message) ;
            }
          
        }
        /// <summary>
        /// 存储主表信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private  OpResult storeInspectionMasterial(InspectionFqcMasterModel model)
        {
            var existmodel = InspectionManagerCrudFactory.FqcMasterCrud.ExistModel( model.OrderId , model.OrderIdNumber , model.MaterialId);
            if (existmodel != null&& existmodel.Id_Key!=0&& model.OpPerson != "StartSetValue")
            {
                if (existmodel.InspectionItems.Contains(model.InspectionItems))
                    model.InspectionItems = existmodel.InspectionItems;
                else model.InspectionItems = existmodel.InspectionItems + "," + model.InspectionItems;
                if (model.InspectionItemCount== this.GetHaveFinishDataNumber(model.InspectionItems))
                {
                    model.InspectionResult = "已完成";
                }
                model.Id_Key = existmodel.Id_Key;
                model.OpSign = "edit";
                return InspectionManagerCrudFactory.FqcMasterCrud.Store(model);
            }
            if (existmodel == null && model.OpPerson == "StartSetValue")
                return InspectionManagerCrudFactory.FqcMasterCrud.Store(model);
            return new OpResult("不必添加了");
           

          
        }


        /// <summary>
        ///  存储副表信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult storeInspectionDetial(InspectionFqcDetailModel model)
        {
            var isExistmodel = InspectionManagerCrudFactory.FqcDetailCrud.GetFqcDetailModelBy(model.OrderId, model.OrderIdNumber, model.InspectionItem);
            if (isExistmodel!=null || model.Id_Key!=0)
            {
                model.Id_Key = isExistmodel.Id_Key;
                string path = isExistmodel.DocumentPath;
                
                model.OpSign = "edit";
            }
            return InspectionManagerCrudFactory.FqcDetailCrud.Store(model);
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
                if(ddd.Keys.Contains(e.OrderIdNumber))
                ddd.Add(e.OrderIdNumber, e.InspectionStatus);
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
