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
    public interface IProductFlowRepositoryRepository : IRepository<ProductFlowModel> { }
    /// <summary>
    ///工序仓储
    /// </summary>
    public class ProductFlowRepositoryRepository : BpmRepositoryBase<ProductFlowModel>, IProductFlowRepositoryRepository
    { }


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
    public interface IDailyReportTemplateRepositoryRepository : IRepository<DailyReportTemplateModel> { }
    /// <summary>
    ///日报模板仓储
    /// </summary>
    public class DailyReportTemplateRepositoryRepository : BpmRepositoryBase<DailyReportTemplateModel>, IDailyReportTemplateRepositoryRepository
    { }

}
