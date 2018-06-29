using Lm.Eic.App.DbAccess.Bpm.Mapping.LmMapping;
using Lm.Eic.App.DomainModel.Bpm.Warehouse;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Repository.WarehouseRep.ExpressRep
{
   
    public interface IExpressRepository : IRepository<ExpressModel>
    { }
    public class ExpressRepository: LmMesRepositoryBase<ExpressModel>, IExpressRepository
    {
    }
}
