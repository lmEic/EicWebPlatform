using Lm.Eic.App.DbAccess.Bpm.Mapping;
using Lm.Eic.App.DomainModel.Bpm.Pms.LeaveAsk;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Repository.PmsRep.LeaveAsk
{
    public interface ILeaveAskManagerRepository : IRepository<LeaveAskManagerModels>
    {


    }
  public  class LeaveAskRepository:BpmRepositoryBase<LeaveAskManagerModels>, ILeaveAskManagerRepository
    {


    }
}
