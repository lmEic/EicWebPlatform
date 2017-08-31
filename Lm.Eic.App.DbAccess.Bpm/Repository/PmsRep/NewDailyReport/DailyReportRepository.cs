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
    public interface IStandardProductionFlowRepository : IRepository<StandardProductionFlowModel> { }
    /// <summary>
    ///标准生产工艺流程
    /// </summary>
    public class StandardProductionFlowRepository : BpmRepositoryBase<StandardProductionFlowModel>, IStandardProductionFlowRepository
    { }
    /// <summary>
    ///每天生产日报表
    /// </summary>
    public interface IDailyProductionReportRepository : IRepository<DailyProductionReportModel> { }
    /// <summary>
    ///每天生产日报表
    /// </summary>
    public class DailyProductionReportRepository : BpmRepositoryBase<DailyProductionReportModel>, IDailyProductionReportRepository
    { }
}
