using Lm.Eic.App.DomainModel.Bpm.Pms.NewDailyReport;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System.Collections.Generic;
using Lm.Eic.App.Erp.Bussiness.QuantityManage;
using Lm.Eic.App.Erp.Domain.QuantityModel;
using System;

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
        /// 存储数据表
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult StoreModelList(List<StandardProductionFlowModel> modelList)
        {
            //先依据部门和品名进行数据库清除 然后批量添加进数据库
            if (modelList.Count > 0)
            {
                //先删除后添加
                DailyReportCrudFactory.ProductionFlowCrud.DeleteProductFlowModelBy(modelList[0].Department, modelList[0].ProductName);
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
            var productionOrderIdInfo = DailyProductionReportService.ProductionConfigManager.ProductOrderDispatch.GetHaveDispatchOrderBy(department, nowDate);
            List<ProductFlowSummaryVm> flowSummaryVms = new List<ProductFlowSummaryVm>();
            ProductFlowSummaryVm flowSummaryVm = null;
            if (productionOrderIdInfo.Count > 0)
            {
                productionOrderIdInfo.ForEach(m =>
                {
                    flowSummaryVm = DailyReportCrudFactory.ProductionFlowCrud.GetProductionFlowSummaryDateBy(m.ProductName);
                    if (flowSummaryVm == null) flowSummaryVm = new ProductFlowSummaryVm();
                    flowSummaryVm.ProductName = m.ProductName;
                    if (!flowSummaryVms.Contains(flowSummaryVm))
                        flowSummaryVms.Add(flowSummaryVm);
                });
            }
            return flowSummaryVms;
        }


        public List<ProductFlowSummaryVm> GetFlowShowSummaryInfosBy(string department, string productName)
        {
            var data = QualityDBManager.OrderIdInpectionDb.GetProductionOrderIdInfoBy("515-1708058");
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
    }
}
