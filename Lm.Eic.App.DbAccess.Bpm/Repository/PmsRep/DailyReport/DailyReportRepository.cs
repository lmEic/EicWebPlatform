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
        /// 获取产品总概述前30行接口  =》部门是必须的
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        List<ProductFlowOverviewModel> GetProductFlowOverviewListBy(string department);
        /// <summary>
        /// 获取产品总概述前30行接口  =》部门是必须的
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        List<ProductFlowOverviewModel> GetProductFlowOverviewListBy(string department,  string containsProductName);
        /// <summary>
        /// 获取产品工艺总览 =》品名和部门是必须的
        /// </summary>
        /// <param name="dto">数据传输对象 请设置部门和品名</param>
        /// <returns></returns>
        ProductFlowOverviewModel GetProductFlowOverviewBy(QueryDailyReportDto dto);
    }
    /// <summary>
    ///工序仓储
    /// </summary>
    public class ProductFlowRepositoryRepository : BpmRepositoryBase<ProductFlowModel>, IProductFlowRepositoryRepository
    {
        /// <summary>
        /// 获取产品工艺总览 =》品名和部门是必须的
        /// </summary>
        /// <param name="dto">数据传输对象 请设置部门和品名</param>
        /// <returns></returns>
        public ProductFlowOverviewModel GetProductFlowOverviewBy(QueryDailyReportDto dto)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT   ProductName, COUNT(ProductName) AS ProductFlowCount, CAST(SUM(CASE StandardHoursType WHEN '1' THEN StandardHours / 60 WHEN '3' THEN 60 / StandardHours ELSE StandardHours END) AS decimal(10, 2)) AS StandardHoursCount ")
                  .Append("FROM   Pms_DReportsProductFlow ")
                  .Append("WHERE   (Department = '" + dto.Department + "')  AND (ProductName = '" + dto.ProductName + "')")
                  .Append("GROUP BY ProductName ");
                string sqltext = sb.ToString();
                return DbHelper.Bpm.LoadEntities<ProductFlowOverviewModel>(sqltext).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// 获取产品总概述前30行
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public List<ProductFlowOverviewModel> GetProductFlowOverviewListBy(string department)
        {
            try
            {
                department = "成型课";

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT   ProductName, COUNT(ProductName) AS ProductFlowCount, CAST(SUM(CASE StandardHoursType WHEN '1' THEN StandardHours / 60 WHEN '3' THEN 60 / StandardHours ELSE StandardHours END) AS decimal(10, 2)) AS StandardHoursCount ")
                  .Append("FROM   Pms_DReportsProductFlow ")
                  .Append("WHERE   (Department = '" + department + "')")
                  .Append("GROUP BY ProductName");
                string sqltext = sb.ToString();
                var productFlowList = DbHelper.Bpm.LoadEntities<ProductFlowOverviewModel>(sqltext).ToList();
                if (productFlowList.Count >= 30)
                    productFlowList = productFlowList.Take(30).ToList();
                return productFlowList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public List<ProductFlowOverviewModel> GetProductFlowOverviewListBy(string department, string containsProductName)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT   ProductName, COUNT(ProductName) AS ProductFlowCount, CAST(SUM(CASE StandardHoursType WHEN '1' THEN StandardHours / 60 WHEN '3' THEN 60 / StandardHours ELSE StandardHours END) AS decimal(10, 2)) AS StandardHoursCount ")
                  .Append("FROM   Pms_DReportsProductFlow ")
                  .Append("WHERE   (Department = '" + department + "')")
                   .Append("AND  (ProductName Like '%" + containsProductName + "%')")
                  .Append("GROUP BY ProductName");
                string sqltext = sb.ToString();
                var productFlowList = DbHelper.Bpm.LoadEntities<ProductFlowOverviewModel>(sqltext).ToList();
                return productFlowList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

        }
    }


    /// <summary>
    ///
    /// </summary>
    public interface IDailyReportRepository : IRepository<DailyReportModel> { }
    /// <summary>
    /// 日报录入仓储
    /// </summary>
    public class DailyReportRepository : BpmRepositoryBase<DailyReportModel>, IDailyReportRepository
    { }


    /// <summary>
    ///
    /// </summary>
    public interface IDailyReportTempRepository : IRepository<DailyReportTempModel> 
    {
        int ChangeCheckSign(string department, DateTime dailyReportDate, string checkSign);

    }
    /// <summary>
    /// 日报录入仓储
    /// </summary>
    public class DailyReportTepmRepository : BpmRepositoryBase<DailyReportTempModel>, IDailyReportTempRepository
    {
        public int ChangeCheckSign(string department, DateTime dailyReportDate, string checkSign)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Update   Pms_DailyReportsTemp ")
                  .Append("Set CheckSign ='" + checkSign + "'")
                  .Append("WHERE   (Department = '" + department + "')  AND (DailyReportDate = '" + dailyReportDate.ToShortDateString() + "')");
             
                string sqltext = sb.ToString();
              return  DbHelper.Bpm.ExecuteNonQuery(sqltext);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
    }


    /// <summary>
    ///机台仓储
    /// </summary>
    public interface IMachineRepositoryRepository : IRepository<MachineModel> { }
    /// <summary>
    ///机台仓储
    /// </summary>
    public class MachineRepositoryRepository : BpmRepositoryBase<MachineModel>, IMachineRepositoryRepository
    { }


    /// <summary>
    /// 非生产原因仓储
    /// </summary>
    public interface INonProductionReasonModelRepository : IRepository<NonProductionReasonModel> { }
    /// <summary>
    ///  非生产原因仓储
    /// </summary>
    public class NonProductionReasonModelRepository : BpmRepositoryBase<NonProductionReasonModel>, INonProductionReasonModelRepository
    { }



    /// <summary>
    /// 工单信息仓储
    /// </summary>
    public interface IDReportsOrderModelRepository : IRepository<DReportsOrderModel> { }
    /// <summary>
    /// 工单信息仓储
    /// </summary>
    public class DReportsOrderModelRepository : BpmRepositoryBase<DReportsOrderModel>, IDReportsOrderModelRepository
    { }


}
