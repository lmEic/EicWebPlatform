using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.App.DbAccess.Bpm.Mapping.LightMaterMapping;

namespace Lm.Eic.App.DbAccess.Bpm.Repository.PmsRep.DailyReport
{
    public interface ILmProDailyReportRepository : IRepository<WipProductCompleteInputDataModel>
    {
    }
    public class LmProDailyReportRepository : LightMasterMesRepositoryBase<WipProductCompleteInputDataModel>, ILmProDailyReportRepository
    {

    }
}
