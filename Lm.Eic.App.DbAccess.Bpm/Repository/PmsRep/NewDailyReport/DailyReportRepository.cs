using Lm.Eic.App.DbAccess.Bpm.Mapping;
using Lm.Eic.App.DomainModel.Bpm.Pms.NewDailyReport;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Repository.PmsRep.NewDailyReport
{
    /// <summary>
    ///标准生产工艺流程
    /// </summary>
    public interface IStandardProductionFlowRepository : IRepository<StandardProductionFlowModel>
    {
        /// <summary>
        /// 获取产品总概述前30行接口  =》部门是必须的
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        List<ProductFlowSummaryVm> GetProductFlowSummaryDatasBy(string department, string productName);
        /// <summary>
        /// 获取产品总概述
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        ProductFlowSummaryVm GetProductFlowSummaryDataBy(string department, string productName);
    }
    /// <summary>
    ///标准生产工艺流程
    /// </summary>
    public class StandardProductionFlowRepository : BpmRepositoryBase<StandardProductionFlowModel>, IStandardProductionFlowRepository
    {
        public ProductFlowSummaryVm GetProductFlowSummaryDataBy(string department, string productName)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT ProductName, COUNT(ProductName)AS ProductFlowCount, ");
                sb.Append("CAST(SUM(CASE StandardProductionTimeType WHEN 'UPS' THEN StandardProductionTime* ProductCoefficient  WHEN 'UPH' THEN 3600* ProductCoefficient / StandardProductionTime ");
                sb.Append("ELSE StandardProductionTime END) AS decimal(10, 2)) AS StandardHoursCount ");
                sb.Append("FROM  Pms_StandardProductionFlow ");
                sb.Append("WHERE (Department = '" + department + "') And       (ProductName = '" + productName + "')  AND (IsValid = 1) AND(IsSum = 1)");
                sb.Append("GROUP BY ProductName ");
                string sqltext = sb.ToString();
                return DbHelper.Bpm.LoadEntities<ProductFlowSummaryVm>(sqltext).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 获取产品总概述前30行
        /// </summary>
        /// <param name="department"></param>
        /// <param name="containsProductName">包函产品名称</param>
        /// <returns></returns>
        public List<ProductFlowSummaryVm> GetProductFlowSummaryDatasBy(string department, string productName)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT ProductName, COUNT(ProductName)AS ProductFlowCount, ");
                sb.Append("CAST(SUM(CASE StandardProductionTimeType WHEN 'UPS' THEN StandardProductionTime / 60 WHEN 'UPH' THEN 60 / StandardProductionTime ");
                sb.Append("ELSE StandardProductionTime END) AS decimal(10, 2)) AS StandardHoursCount ");
                sb.Append("FROM  Pms_StandardProductionFlow ");
                sb.Append("WHERE(Department = '" + department + "') ");
                if (productName != null && productName != string.Empty)
                {
                    sb.Append("and (ProductName like '%" + productName + "%') ");
                }
                sb.Append("GROUP BY ProductName ");
                string sqltext = sb.ToString();
                var productFlowSummaryDatas = DbHelper.Bpm.LoadEntities<ProductFlowSummaryVm>(sqltext).ToList();
                if (productFlowSummaryDatas.Count >= 30)
                    productFlowSummaryDatas = productFlowSummaryDatas.Take(30).ToList();
                return productFlowSummaryDatas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

        }
    }


    /// <summary>
    ///生产日报订单分配
    /// </summary>
    public interface IProductOrderDispatchRepository : IRepository<ProductOrderDispatchModel> { }
    /// <summary>
    ///生产日报订单分配
    /// </summary>
    public class ProductOrderDispatchRepository : BpmRepositoryBase<ProductOrderDispatchModel>, IProductOrderDispatchRepository
    {

    }





    /// <summary>
    ///每天生产日报表 UnProductionReportModel
    /// </summary>
    public interface IDailyProductionReportRepository : IRepository<DailyProductionReportModel> { }
    /// <summary>
    ///每天生产日报表
    /// </summary>
    public class DailyProductionReportRepository : BpmRepositoryBase<DailyProductionReportModel>, IDailyProductionReportRepository
    { }




    /// <summary>
    /// 生产代码配置
    /// </summary>
    public interface IProductionCodeConfigRepository : IRepository<ProductionCodeConfigModel> { }
    /// <summary>
    ///生产代码配置
    /// </summary>
    public class ProductionCodeConfigRepository : BpmRepositoryBase<ProductionCodeConfigModel>, IProductionCodeConfigRepository
    { }


    /// <summary>
    /// 不良处理程序
    /// </summary>
    public interface IProductionDefectiveTreatmentRepository : IRepository<DailyProductionDefectiveTreatmentModel> { }
    /// <summary>
    ///不良处理程序
    /// </summary>
    public class ProductionDefectiveTreatmentRepository : BpmRepositoryBase<DailyProductionDefectiveTreatmentModel>, IProductionDefectiveTreatmentRepository
    { }


    /// <summary>
    /// 不良处理程序
    /// </summary>
    public interface IReportsMachineRepository : IRepository<ReportsMachineModel> { }
    /// <summary>
    ///不良处理程序
    /// </summary>
    public class ReportsMachineRepository : BpmRepositoryBase<ReportsMachineModel>, IReportsMachineRepository
    { }

}
