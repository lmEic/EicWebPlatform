using Lm.Eic.App.DbAccess.Bpm.Mapping.HrmMapping;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.Archives
{
    public interface ICalendarsRepository : IRepository<CalendarModel>
    { }
    public class CalendarsRepository : HrmRepositoryBase<CalendarModel>, ICalendarsRepository
    {
    }
}
