using Lm.Eic.App.DomainModel.Bpm.Pms.NewDailyReport;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System.Collections.Generic;
using Lm.Eic.App.Erp.Bussiness.QuantityManage;
using Lm.Eic.App.Erp.Domain.QuantityModel;
using System;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using System.Linq;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.App.Business.Bmp.Pms.NewDailyReport.DailyReportConfig;

namespace Lm.Eic.App.Business.Bmp.Pms.NewDailyReport
{
   /// <summary>
   /// 日报表管理工厂
   /// </summary>
    public class DailyProductionReportManager
    {
        /// <summary>
        /// 产品标准工时工艺设置
        /// </summary>
        public StandardProductionFlowManager ProductionFlowSet
        {
            get { return OBulider.BuildInstance<StandardProductionFlowManager>(); }
        }
        /// <summary>
        /// 日报表生产编码管理
        /// </summary>
        public DailyProductionCodeConfigManager DailyProductionCodeConfig
        {
            get { return OBulider.BuildInstance<DailyProductionCodeConfigManager>(); }
        }
        /// <summary>
        /// 生产日报订单分配管理
        /// </summary>
        public ProductOrderDispatchManager ProductOrderDispatch
        {
            get { return OBulider.BuildInstance<ProductOrderDispatchManager>(); }
        }
     
        
        /// <summary>
        /// 日报表表管理
        /// </summary>
        public DailyReportManager DailyReport
        {
            get { return OBulider.BuildInstance<DailyReportManager>(); }
        }
        /// <summary>
        /// 不良制程处理
        /// </summary>
        public DailyProductionDefectiveTreatmentManger ProductionDefectiveTreatment
        {
            get { return OBulider.BuildInstance<DailyProductionDefectiveTreatmentManger>(); }
        }
        /// <summary>
        /// 机台管理
        /// </summary>
       public MachineInfoManager MachineInfo
        {
            get { return OBulider.BuildInstance<MachineInfoManager>(); }
        }

    }

    /// <summary>
    ///  产品标准工时工艺设置管理
    /// </summary>
    public class StandardProductionFlowManager
    {
        /// <summary>
        /// 获取产品工艺
        /// </summary>
        /// <param name="dto">数据传输对象 品名和部门是必须的</param>
        /// <returns></returns>
        public List<StandardProductionFlowModel> GetProductFlowInfoBy(QueryDailyProductReportDto dto)
        {
            return DailyReportCrudFactory.ProductionFlowCrud.FindBy(dto);
        }

        /// <summary>
        /// 导入工序列表
        /// </summary>
        /// <param name="documentPatch">Excel文档路径</param>
        /// <returns></returns>
        public List<StandardProductionFlowModel> ImportProductFlowListBy(string documentPatch)
        {
            return documentPatch.GetEntitiesFromExcel<StandardProductionFlowModel>();
        }
        /// <summary>
        /// 获取工序Excel模板
        /// </summary>
        /// <param name="documentPath"></param>
        /// <returns></returns>
        public DownLoadFileModel GetProductFlowTemplate(string siteRootPath, string documentPath, string fileName)
        {
            DownLoadFileModel dlfm = new DownLoadFileModel();
            return dlfm.CreateInstance
                 (siteRootPath.GetDownLoadFilePath(documentPath),
                 fileName.GetDownLoadContentType(),
                  fileName);
        }
        /// <summary>
        /// 存储数据表
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult StoreModelList(List<StandardProductionFlowModel> modelList)
        {
            return  (modelList.Count > 0)?
                DailyReportCrudFactory.ProductionFlowCrud.AddProductFlowModelList(modelList):
                OpResult.SetErrorResult("列表不能为空！");
        }
        public OpResult StoreProductFlow(StandardProductionFlowModel model)
        {
            return DailyReportCrudFactory.ProductionFlowCrud.Store(model, true);
        }
        public OpResult DeleteSingleProcessesFlow(string productName, string processesName)
        {
            return DailyReportCrudFactory.ProductionFlowCrud.DeleteSingleProductFlow(productName, processesName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <param name="productName"></param>
        /// <returns></returns>
        public List<ProductFlowSummaryVm> GetFlowShowSummaryInfosBy(string department)
        {
            ///从得到部门已分配的工单  并且有效 
            var productionOrderIdInfo = DailyProductionReportService.ProductionConfigManager.ProductOrderDispatch.GetHaveDispatchOrderBy(department, "已分配", "True");
            List<ProductFlowSummaryVm> flowSummaryVms = new List<ProductFlowSummaryVm>();
            List<string> productNamelist = new List<string>();
            ProductFlowSummaryVm flowSummaryVm = null;
            if (productionOrderIdInfo.Count > 0)
            {
                productionOrderIdInfo.ForEach(m =>
                {
                    flowSummaryVm = DailyReportCrudFactory.ProductionFlowCrud.GetProductionFlowSummaryDateBy(department, m.ProductName);
                    if (flowSummaryVm == null) flowSummaryVm = new ProductFlowSummaryVm();
                    flowSummaryVm.ProductName = m.ProductName;
                    flowSummaryVm.ProductId = m.ProductId;
                    if (!productNamelist.Contains(flowSummaryVm.ProductName))
                    {
                        flowSummaryVms.Add(flowSummaryVm);
                        productNamelist.Add(m.ProductName);
                    }
                });
            }
            return flowSummaryVms;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <param name="productName"></param>
        /// <returns></returns>

        public List<ProductFlowSummaryVm> GetFlowShowSummaryInfosBy(string department, string productName)
        {
            return DailyReportCrudFactory.ProductionFlowCrud.GetProductionFlowSummaryDatesBy(department, productName);
        }

    }

    /// <summary>
    /// 工单分配管理
    /// </summary>
    public class ProductOrderDispatchManager
    {
        /// <summary>
        /// 获得当前已经分配的订单
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public List<ProductOrderDispatchModel> GetHaveDispatchOrderBy(string department, string dicpatchStatus, string isValid)
        {
            return DailyReportCrudFactory.ProductOrderDispatch.GetHaveDispatchOrderBy(department, dicpatchStatus, isValid );
        }
        /// <summary>
        /// 得到部门工单
        /// </summary>
        /// <param name="department"></param>
        /// <param name="isVirtualOrderId">是否为虚以的工单</param>
        /// <returns></returns>
        public List<ProductOrderDispatchModel> GetDispatchOrderDataBy(string department, int isVirtualOrderId)
        {
            return DailyReportCrudFactory.ProductOrderDispatch.GetDepartmentDispatchOrderDataBy(department,  isVirtualOrderId);
        }

        /// <summary>
        /// 得到部门需要分配的工单 并更新处理分配工单列表数据
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public List<ProductOrderDispatchModel> GetNeedDispatchOrderBy(string department)
        {
            DateTime getDate =DateTime.Now.Date.ToDate();
            List<ProductOrderDispatchModel> returndatas = new List<ProductOrderDispatchModel>();
            ProductOrderDispatchModel model = null;
            ///ERP在制生产订单
            var erpInProductiondatas = QualityDBManager.OrderIdInpectionDb.GetProductionOrderIdInfoBy(department, "在制");
            ///今日生产已确认分配的订单

            if (erpInProductiondatas == null || erpInProductiondatas.Count == 0) return returndatas;
            ///以ERP中在制工单为主线  
            erpInProductiondatas.ForEach(e =>
            {
                model = new ProductOrderDispatchModel();
                OOMaper.Mapper<ProductionOrderIdInfo, ProductOrderDispatchModel>(e, model);
                ////如果订单不在分配的数据库中表中，添加新的，
                var haveDispatchOrder = DailyReportCrudFactory.ProductOrderDispatch.GetOrderInfoBy(e.OrderId);
                if (haveDispatchOrder == null)
                {
                    model.ProductionDate = e.PlanStartProductionDate;
                    model.ValidDate = e.PlanEndProductionDate;
                    model.DicpatchStatus = "未分配";
                    model.IsValid = "false";
                }
                ///如果存在 更新其完工状态   判断其有效时间
                else
                {
                    model = haveDispatchOrder;
                    if (model.ValidDate <= getDate)
                    {
                        model.DicpatchStatus = "已失效";
                    }
                    model.ProductStatus = e.ProductStatus;
                }
                if (!returndatas.Contains(model))
                    returndatas.Add(model);
            });
            /// 更新 ERP中  （不再是“在制”）已完工工单
            TreatmentNoProductOrderId(department, returndatas);
            return returndatas.OrderByDescending(e => e.OrderId).ToList();
        }
        /// <summary>
        /// 依据工单号模糊查询 工单信息
        /// </summary>
        /// <param name="orderId">工单号</param>
        /// <returns></returns>
        public List<ProductOrderDispatchModel> GetLikeQueryOrderDataBy(string orderId)
        {
        
            List<ProductOrderDispatchModel> returndatas = new List<ProductOrderDispatchModel>();
            ProductOrderDispatchModel model = null;
            ///ERP在制生产订单
            var erpInProductiondatas = QualityDBManager.OrderIdInpectionDb.GetLikeQueryProductionOrderInfoBy(orderId);
            ///今日生产已确认分配的订单
            if (erpInProductiondatas == null || erpInProductiondatas.Count == 0) return returndatas;
            erpInProductiondatas.ForEach(e =>
            {
                model = new ProductOrderDispatchModel();
                OOMaper.Mapper<ProductionOrderIdInfo, ProductOrderDispatchModel>(e, model);
                ///如果订单在数据库中不存在
                var haveDispatchOrder = DailyReportCrudFactory.ProductOrderDispatch.GetOrderInfoBy(e.OrderId);
                if (haveDispatchOrder == null)
                {
                    model.ProductionDate = e.PlanStartProductionDate;
                    model.ValidDate = e.PlanEndProductionDate;
                    model.DicpatchStatus = "未分配";
                    model.IsValid = "false";
                }
                else model = haveDispatchOrder;
                
                if (!returndatas.Contains(model))
                    returndatas.Add(model);
            });
            return returndatas.OrderByDescending(e => e.OrderId).ToList();
        }
        /// <summary>
        /// 保存订单数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreOrderDispatchData(ProductOrderDispatchModel model)
        {
            return DailyReportCrudFactory.ProductOrderDispatch.Store(model, true);
        }
        /// <summary>
        /// 处理已经分配的  但是（不再是“在制”）已完工的订单
        /// </summary>
        /// <param name="todayHaveDispatchProductionOrderDatas">ERP在制工单</param>
        /// <param name="erpInProductiondatas"></param>
        void TreatmentNoProductOrderId(string department, List<ProductOrderDispatchModel> erpInProductiondatas)
        {
            ///查询出部门已分配工单，未失效的工单
            var todayHaveDispatchProductionOrderDatas = DailyReportCrudFactory.ProductOrderDispatch.GetHaveDispatchOrderBy(department, "已分配", "True");
            if (todayHaveDispatchProductionOrderDatas == null || todayHaveDispatchProductionOrderDatas.Count == 0) return;
            ///除掉虚以的工单
            var datas = todayHaveDispatchProductionOrderDatas.Where(e => e.IsVirtualOrderId == 0).ToList();
            if (datas == null || datas.Count == 0) return;
            datas.ForEach(e =>
            {
                ///如果工单不在ERP中的在制工单中 那么此分配的工单失效 状态变为已经完工 如果是虚拟工单不用改变
                var dates = erpInProductiondatas.FirstOrDefault(f => f.OrderId == e.OrderId && e.IsVirtualOrderId == 0);
                if (dates == null)
                {
                    e.IsValid = "false";
                    e.ProductStatus = "已完工";
                    e.OpSign = OpMode.Edit;
                    StoreOrderDispatchData(e);
                }
            });
        }
    }

    /// <summary>
    /// 日报表管理
    /// </summary>
    public class DailyReportManager
    {
        public OpResult StoreDailyReport(DailyProductionReportModel model)
        {
            return DailyReportCrudFactory.DailyProductionReport.Store(model, true);
        }
        public OpResult StoreDailyReport(List<DailyProductionReportModel> DailyReportList)
        {
            return DailyReportCrudFactory.DailyProductionReport.SavaDailyReportList(DailyReportList);
        }
        public OpResult StoreDailyReport(DailyProductionReportModel model, List<UserInfoVm> groupUserInfos, out List<DailyProductionReportModel> storeListDatas)
        {
            List<DailyProductionReportModel> DailyReportList = new List<DailyProductionReportModel>();
            if (model != null)
            {
                DailyProductionReportModel newModel = null;
                double sumProductCount = model.TodayProductionCount;
                double sumProductBadCount = model.TodayBadProductCount;
                if (groupUserInfos == null || groupUserInfos.Count == 0)
                {
                    storeListDatas = null;
                    return OpResult.SetErrorResult("人员数据为空，操作失败!");
                }
                double sumWorkerProductionTime = groupUserInfos.Sum(e => e.WorkerProductionTime);
                ///总工时不能为空
                if (sumWorkerProductionTime == 0)
                {
                    storeListDatas = null;
                    return OpResult.SetErrorResult("人员统计的工时为空，操作失败!");
                }
                ///对多人信息进行分摊
                groupUserInfos.ForEach(m =>
                {
                    newModel = new DailyProductionReportModel();
                    OOMaper.Mapper<DailyProductionReportModel, DailyProductionReportModel>(model, newModel);
                    OOMaper.Mapper<UserInfoVm, DailyProductionReportModel>(m, newModel);
                    newModel.TodayProductionCount = Math.Round((sumProductCount / sumWorkerProductionTime) * m.WorkerProductionTime, 2, MidpointRounding.AwayFromZero);
                    //不良数也按工时分分摊
                    newModel.TodayBadProductCount = Math.Round((sumProductBadCount / sumWorkerProductionTime) * m.WorkerProductionTime, 2, MidpointRounding.AwayFromZero);
                    if (!DailyReportList.Contains(newModel))
                        DailyReportList.Add(newModel);
                });
            }
            OpResult resultDatas = DailyReportCrudFactory.DailyProductionReport.SavaDailyReportList(DailyReportList);
            storeListDatas = DailyReportList;
            return resultDatas;
        }

        /// <summary>
        /// 处理机台输入分摊日报表数据返回
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="disposeSign"></param>
        /// <returns></returns>
        public List<DailyProductionReportModel> DisposeDailyReportdMachineDatas(List<DailyProductionReportModel> datas, double workerLookMachineSumTime,double workerNoProductionSumTime)
        {
            List<DailyProductionReportModel> DailyReportList = new List<DailyProductionReportModel>();
            DailyProductionReportModel newModel = null;
            if (datas != null)
            {
                double machineRunSumTime = datas.Sum(e => e.MachineProductionTime);
                
                // 人员投入工时 分摊到每个工单上  依据 出勤时数=（人员工时/ 机台生产总时间）* 当前机台生产时间
                // WorkerProductionTime= （WorkerProductionTime/ MachineProductionTimeSum） *MachineProductionTime
                datas.ForEach(e =>
                {
                    newModel = new DailyProductionReportModel();
                    OOMaper.Mapper<DailyProductionReportModel, DailyProductionReportModel>(e, newModel);
                    newModel.InPutDate = e.InPutDate.ToDate();
                    ///人工生产产量 ==机台生产产量
                    newModel.TodayProductionCount = e.MachineProductionCount;
                    /// 人工不良产量= 机台不良产量
                    newModel.TodayBadProductCount = e.MachineProductionBadCount;
                    ///计算人员的标准工时
                   if(e.MachineProductionCount!=0&& e.StandardProductionTime!=0)
                        newModel.GetProductionTime = Math.Round(e.MachineProductionCount * e.StandardProductionTime / 3600, 2);
                    /// 机台录入分摊人工工时
                    if (machineRunSumTime != 0)
                    {
                        newModel.WorkerProductionTime = Math.Round((workerLookMachineSumTime / machineRunSumTime) * e.MachineProductionTime, 2);
                        newModel.WorkerNoProductionTime = Math.Round((workerNoProductionSumTime / machineRunSumTime) * e.MachineProductionTime, 2);
                    }
                    newModel.MachineUnproductiveTime = e.MachineSetProductionTime - e.MachineProductionTime;    
                    if (!DailyReportList.Contains(newModel))
                        DailyReportList.Add(newModel);
                });
            }
            return DailyReportList;
        }
        public List<ProductFlowCountDatasVm> GetProductionFlowCountDatas(string department, string orderId, string productName)
        {
            List<ProductFlowCountDatasVm> retrundatas = new List<ProductFlowCountDatasVm>();
            ProductFlowCountDatasVm modelVm = null;
            var datas = DailyProductionReportService.ProductionConfigManager.ProductionFlowSet.GetProductFlowInfoBy(new QueryDailyProductReportDto()
            { Department = department, ProductName = productName, SearchMode = 2 });
            if (datas == null || datas.Count == 0) return retrundatas;
            datas.ForEach(e =>
            {
                double orderHavePutInNumber = DailyReportCrudFactory.DailyProductionReport.GetDailyProductionCountBy(orderId, e.ProcessesName);
                modelVm = new ProductFlowCountDatasVm()
                { OrderId = orderId, OrderHavePutInNumber = orderHavePutInNumber };
                OOMaper.Mapper<StandardProductionFlowModel, ProductFlowCountDatasVm>(e, modelVm);
                var orderInfo = DailyReportCrudFactory.ProductOrderDispatch.GetOrderInfoBy(orderId);
                if (orderInfo != null)
                {
                    modelVm.ProductName = orderInfo.ProductName;
                    modelVm.OrderProductNumber = Math.Round(orderInfo.ProduceNumber, 2);
                    modelVm.OrderNeedPutInNumber = orderInfo.ProduceNumber * e.ProductCoefficient;
                }
                if (!retrundatas.Contains(modelVm))
                    retrundatas.Add(modelVm);
            });
            return retrundatas;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="orderId"></param>
        /// <param name="processesName"></param>
        /// <returns></returns>
        public List<DailyProductionReportModel> GetDailyDataBy(DateTime date, string orderId, string processesName)
        {
            return DailyReportCrudFactory.DailyProductionReport.GetDailyDatasBy(date, orderId, processesName);
        }
        public DailyProductionReportModel GetWorkerDailyDatasBy(string workerId)
        {
            var datas = DailyReportCrudFactory.DailyProductionReport.GetWorkerDailyDatasBy(workerId);
            if (datas == null) return null;
            return datas.FirstOrDefault();
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="department"></param>
       /// <returns></returns>
        public List<DailyPTProductVm> getPt1ReportData(string department)
        {
            List<DailyPTProductVm> datas = new List<DailyPTProductVm>();
            DailyPTProductVm newModel = null;
            var machineInfos = DailyReportCrudFactory.DailyReportsMachine.GetMachineDatas(department);
            if (machineInfos == null) return datas;
            machineInfos.ForEach(e => {

                newModel = new DailyPTProductVm();
                OOMaper.Mapper<ReportsMachineModel, DailyPTProductVm>(e, newModel);
                newModel.ClassType = "白班";
                datas.Add(newModel);
            });
            return datas;
        }
    }
}
