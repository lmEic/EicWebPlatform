using Lm.Eic.App.DomainModel.Bpm.Pms.NewDailyReport;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.NewDailyReport.DailyReportConfig
{
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
            DateTime nowDate = DateTime.Now.Date.ToDate();
            //从ERP中获得部门 在制所有工单信息
            var productionOrderIdInfo = DailyProductionReportService.ProductionConfigManager.ProductOrderDispatch.GetHaveDispatchOrderBy(department,"已分配");
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
}
