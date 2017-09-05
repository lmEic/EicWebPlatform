using Lm.Eic.App.DomainModel.Bpm.Pms.NewDailyReport;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public OpResult Store(List<StandardProductionFlowModel> modelList)
        {
            //先依据部门和品名进行数据库清除 然后批量添加进数据库
            if (modelList.Count > 0)
            {
                DailyReportCrudFactory.ProductionFlowCrud.DeleteProductFlowModelBy(modelList[0].Department, modelList[0].ProductName);
                return DailyReportCrudFactory.ProductionFlowCrud.AddProductFlowModelList(modelList);
            }
            else
            {
                return OpResult.SetErrorResult("列表不能为空！");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="department"></param>
        /// <param name="productName"></param>
        /// <returns></returns>
        public List<ProductFlowSummaryVm> GetFlowShowSummaryInfosBy(string department, string productName = null)
        {
            //从ERP中获得部门 在制所有工单信息
            var ddd = "";
            return DailyReportCrudFactory.ProductionFlowCrud.GetProductionFlowSummaryDatesBy(department, productName);
        }
    }
}
