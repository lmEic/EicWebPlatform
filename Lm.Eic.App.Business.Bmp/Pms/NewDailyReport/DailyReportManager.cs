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

namespace Lm.Eic.App.Business.Bmp.Pms.NewDailyReport
{
    public class DailyProductionReportManager
    {
        /// <summary>
        /// 产品生产工艺设置
        /// </summary>
        public ProductionFlowManager ProductionFlowSet
        {
            get { return OBulider.BuildInstance<ProductionFlowManager>(); }
        }
        /// <summary>
        /// 生产日报订单分配管理
        /// </summary>
        public ProductOrderDispatchManager ProductOrderDispatch
        {
            get { return OBulider.BuildInstance<ProductOrderDispatchManager>(); }
        }
        public DailyReportManager DailyReport
        {
            get { return OBulider.BuildInstance<DailyReportManager>(); }
        }

    }

    public class ProductionFlowManager
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
            //先依据部门和品名进行数据库清除 然后批量添加进数据库
            if (modelList.Count > 0)
            {
                ///先删除后添加
                /// DailyReportCrudFactory.ProductionFlowCrud.DeleteProductFlowModelBy(modelList[0].Department, modelList[0].ProductName);
                return DailyReportCrudFactory.ProductionFlowCrud.AddProductFlowModelList(modelList);
            }
            else
            {
                return OpResult.SetErrorResult("列表不能为空！");
            }
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
            DateTime nowDate = DateTime.Now.Date;
            //从ERP中获得部门 在制所有工单信息
            var productionOrderIdInfo = DailyProductionReportService.ProductionConfigManager.ProductOrderDispatch.GetHaveDispatchOrderBy(department);
            List<ProductFlowSummaryVm> flowSummaryVms = new List<ProductFlowSummaryVm>();
            List<string> productNamelist = new List<string>();
            ProductFlowSummaryVm flowSummaryVm = null;
            if (productionOrderIdInfo.Count > 0)
            {
                productionOrderIdInfo.ForEach(m =>
                {
                    flowSummaryVm = DailyReportCrudFactory.ProductionFlowCrud.GetProductionFlowSummaryDateBy(m.ProductName);
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


        public List<ProductFlowSummaryVm> GetFlowShowSummaryInfosBy(string department, string productName)
        {
            return DailyReportCrudFactory.ProductionFlowCrud.GetProductionFlowSummaryDatesBy(department, productName);
        }
    }

    public class ProductOrderDispatchManager
    {
        /// <summary>
        /// 获得当前已经分配的订单
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public List<ProductOrderDispatchModel> GetHaveDispatchOrderBy(string department, DateTime nowDate)
        {
            return DailyReportCrudFactory.ProductOrderDispatch.GetHaveDispatchOrderBy(department, nowDate);
        }
        public List<ProductOrderDispatchModel> GetHaveDispatchOrderBy(string department)
        {
            return DailyReportCrudFactory.ProductOrderDispatch.GetHaveDispatchOrderBy(department);
        }
        public List<ProductOrderDispatchModel> GetNeedDispatchOrderBy(string department, DateTime nowDate)
        {
            List<ProductOrderDispatchModel> returndatas = new List<ProductOrderDispatchModel>();
            ProductOrderDispatchModel model = null;
            ///ERP在制生产订单
            var erpInProductiondatas = QualityDBManager.OrderIdInpectionDb.GetProductionOrderIdInfoBy(department, "在制");
            ///今日生产已确认分配的订单
            var todayHaveDispatchProductionOrderDatas = DailyProductionReportService.ProductionConfigManager.ProductOrderDispatch.GetHaveDispatchOrderBy(department, nowDate);
            if (erpInProductiondatas == null || erpInProductiondatas.Count == 0) return returndatas;
            erpInProductiondatas.ForEach(e =>
            {
                model = new ProductOrderDispatchModel();
                OOMaper.Mapper<ProductionOrderIdInfo, ProductOrderDispatchModel>(e, model);
                var haveDispatchOrder = todayHaveDispatchProductionOrderDatas.Find(m => m.OrderId == e.OrderId);
                if (haveDispatchOrder == null)
                {
                    model.ProductionDate = e.PlanStartProductionDate;
                    model.ValidDate = e.PlanEndProductionDate;
                    model.IsValid = "False";
                }
                else
                {
                    model.IsValid = "True";
                    model.ProductionDate = haveDispatchOrder.ProductionDate;
                    model.ValidDate = haveDispatchOrder.ValidDate;
                }

                if (!returndatas.Contains(model))
                    returndatas.Add(model);
            });
            TreatmentNoProductOrderId(todayHaveDispatchProductionOrderDatas, erpInProductiondatas);
            return returndatas.OrderByDescending(e => e.OrderId).ToList();
        }
        public void TreatmentNoProductOrderId(List<ProductOrderDispatchModel> todayHaveDispatchProductionOrderDatas, List<ProductionOrderIdInfo> erpInProductiondatas)
        {
            if (todayHaveDispatchProductionOrderDatas == null || todayHaveDispatchProductionOrderDatas.Count == 0) return;
            todayHaveDispatchProductionOrderDatas.ForEach(e =>
            {
                if (e.OrderId != "511-1111111")
                {
                    var dates = erpInProductiondatas.FindAll(f => f.OrderId == e.OrderId);
                    if (dates == null || dates.Count == 0)
                    {
                        e.IsValid = "False";
                        e.OpSign = OpMode.Edit;
                        StoreOrderDispatchData(e);
                    }
                }
            });

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
    }

    public class DailyReportManager
    {
        public OpResult StoreDailyReport(DailyProductionReportModel model)
        {
            return DailyReportCrudFactory.DailyProductionReport.Store(model, true);
        }
        public OpResult StoreDailyReport(List<DailyProductionReportModel> model)
        {
            return null;
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
                double sumNoProductionTime = groupUserInfos.Sum(e => e.WorkerNoProductionTime);
                double sumWorkerProductionTime = groupUserInfos.Sum(e => e.WorkerProductionTime);
                if (sumWorkerProductionTime == 0)
                {
                    storeListDatas = null;
                    return OpResult.SetErrorResult("人员统计的工时为空，操作失败!");
                }
                groupUserInfos.ForEach(m =>
                {
                    newModel = new DailyProductionReportModel();
                    OOMaper.Mapper<DailyProductionReportModel, DailyProductionReportModel>(model, newModel);
                    OOMaper.Mapper<UserInfoVm, DailyProductionReportModel>(m, newModel);
                    ///日期格式 简化
                    newModel.InPutDate = model.InPutDate.ToDate();
                    newModel.TodayProductionCount = Math.Round((sumProductCount / sumWorkerProductionTime) * m.WorkerProductionTime, 2, MidpointRounding.AwayFromZero);
                    if (sumNoProductionTime != 0)
                        newModel.TodayBadProductCount = Math.Round((sumProductBadCount / sumNoProductionTime) * m.WorkerNoProductionTime, 2, MidpointRounding.AwayFromZero);
                    if (!DailyReportList.Contains(newModel))
                        DailyReportList.Add(newModel);
                });
            }
            OpResult resultDatas = DailyReportCrudFactory.DailyProductionReport.SavaDailyReportList(DailyReportList);
            storeListDatas = DailyReportList;
            return resultDatas;
        }

        /// <summary>
        /// 处理日报表数据反回到界中去
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="disposeSign"></param>
        /// <returns></returns>
        public List<DailyProductionReportModel> DisposeMultitermDailyReportdDatas(List<DailyProductionReportModel> datas, string disposeSign)
        {
            List<DailyProductionReportModel> DailyReportList = new List<DailyProductionReportModel>();
            DailyProductionReportModel newModel = null;
            if (datas != null)
            {
                double workerLookMachineSumTime = datas.FirstOrDefault().WorkerProductionTime;
                double sumProductCount = datas.FirstOrDefault().MachineProductionCount;
                double sumProductBadCount = datas.FirstOrDefault().MachineUnproductiveTime;
                double machineRunSumTime = datas.Sum(e => e.MachineProductionTime);
                double sumNoProductionTime = datas.Sum(e => e.WorkerNoProductionTime);
                double sumWorkerProductionTime = datas.Sum(e => e.WorkerProductionTime);
                if (sumWorkerProductionTime == 0 || machineRunSumTime == 0)
                    return DailyReportList;
                // 人员投入工时 分摊到每个工单上  依据 出勤时数=（人员工时/ 机台生产总时间）* 当前机台生产时间
                // WorkerProductionTime= （WorkerProductionTime/ MachineProductionTimeSum） *MachineProductionTime
                datas.ForEach(e =>
                {
                    newModel = new DailyProductionReportModel();
                    OOMaper.Mapper<DailyProductionReportModel, DailyProductionReportModel>(e, newModel);
                    newModel.InPutDate = e.InPutDate.ToDate();
                    newModel.TodayProductionCount = e.MachineProductionCount;
                    newModel.TodayBadProductCount = e.MachineProductionBadCount;
                    newModel.GetProductionTime = Math.Round(e.MachineProductionCount * e.StandardProductionTime / 3600, 2);
                    if (disposeSign == "机台")
                    {
                        /// 机台录入分摊人工工时
                        newModel.WorkerProductionTime = Math.Round((workerLookMachineSumTime / machineRunSumTime) * e.MachineProductionTime, 2);
                        newModel.MachineUnproductiveTime = e.MachineSetProductionTime - e.MachineProductionTime;
                    }
                    else
                    {
                        /// 团队录入分摊数量
                        newModel.TodayProductionCount = Math.Round((sumProductCount / sumWorkerProductionTime) * e.WorkerProductionTime, 2, MidpointRounding.AwayFromZero);
                        if (sumNoProductionTime != 0)
                            newModel.TodayBadProductCount = Math.Round((sumProductBadCount / sumNoProductionTime) * e.WorkerNoProductionTime, 2, MidpointRounding.AwayFromZero);
                    }
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
    }
}
