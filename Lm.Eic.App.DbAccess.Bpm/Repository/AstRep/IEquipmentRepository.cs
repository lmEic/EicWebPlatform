using Lm.Eic.App.DbAccess.Bpm.Mapping;
using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.DbAccess.Bpm.Repository.AstRep
{
    public interface IEquipmentRepository : IRepository<EquipmentModel>
    {

    }

    public class EquipmentRepository : EquipmentRepositoryBase<EquipmentModel>, IEquipmentRepository
    {

    }
}
