using Lm.Eic.App.DbAccess.Bpm.Mapping.HrmMapping;
using Lm.Eic.App.DomainModel.Bpm.Hrm.WorkOverHours;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.WorkOverHours
{
    /// <summary>
    /// 加班管理持久层
    /// </summary>
    public interface IWorkOverHoursManageModelsRepository : IRepository<WorkOverHoursMangeModels> { }
    public class WorkOverHoursManageModelsRepository : HrmRepositoryBase<WorkOverHoursMangeModels>, IWorkOverHoursManageModelsRepository
    { }



}
