using Lm.Eic.App.DbAccess.Bpm.Mapping;
using Lm.Eic.App.DomainModel.Bpm.Pms.LeaveAsk;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Repository.PmsRep.LeaveAsk
{
    public interface ILeaveAskManagerRepository : IRepository<LeaveAskModels>
    {


    }
  public  class LeaveAskRepository:BpmRepositoryBase<LeaveAskModels>, ILeaveAskManagerRepository
    {


    }
}
