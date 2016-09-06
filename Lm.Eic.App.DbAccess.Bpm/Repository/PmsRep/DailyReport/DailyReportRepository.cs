using Lm.Eic.App.DbAccess.Bpm.Mapping;
using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Repository.PmsRep.DailyReport
{
    /// <summary>
    ///
    /// </summary>
    public interface IProductFlowRepositoryRepository : IRepository<ProductFlowModel> 
    {
        /// <summary>
        /// 获取产品总概述前30行接口
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        List<ProductFlowOverviewModel> GetFlowOverviewListBy(string department);
    }
    /// <summary>
    ///工序仓储
    /// </summary>
    public class ProductFlowRepositoryRepository : BpmRepositoryBase<ProductFlowModel>, IProductFlowRepositoryRepository
    {
        /// <summary>
        /// 获取产品总概述前30行
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public List<ProductFlowOverviewModel> GetFlowOverviewListBy(string department)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DISTINCT ProductName AS ProductName, COUNT(ProductName) AS ProductFlowCount, SUM(StandardHours) AS StandardHoursCount ")
              .Append("FROM   Pms_ProductFlow ")
              .Append("WHERE   (Department = '" + department + "')")
              .Append("GROUP BY ProductName ");
            string sqltext = sb.ToString();
            return DbHelper.Bpm.LoadEntities<ProductFlowOverviewModel>(sqltext).Take(30).ToList();
        }
    }


    /// <summary>
    ///
    /// </summary>
    public interface IDailyReportRepositoryRepository : IRepository<DailyReportModel> { }
    /// <summary>
    /// 日报录入仓储
    /// </summary>
    public class DailyReportRepositoryRepository : BpmRepositoryBase<DailyReportModel>, IDailyReportRepositoryRepository
    { }

    /// <summary>
    ///
    /// </summary>
    public interface IDailyReportTemplateRepositoryRepository : IRepository<DailyReportTemplateModel> 
    {
      
    }


    /// <summary>
    ///日报模板仓储
    /// </summary>
    public class DailyReportTemplateRepositoryRepository : BpmRepositoryBase<DailyReportTemplateModel>, IDailyReportTemplateRepositoryRepository
    {
       
    
    }

}
