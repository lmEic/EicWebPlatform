using Lm.Eic.Framework.Authenticate.Model;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.Framework.Authenticate.Repository.Mapping;

namespace Lm.Eic.Framework.Authenticate.Repository
{

    public interface IAssemblyRepository : IRepository<AssemblyModel> { }

    public class AssemblyRepository : AuthenRepositoryBase<AssemblyModel>, IAssemblyRepository
    { }

    public interface IModuleNavigationRepository : IRepository<ModuleNavigationModel>
    {
       
    }

    public class ModuleNavigationRepository : AuthenRepositoryBase<ModuleNavigationModel>, IModuleNavigationRepository
    {
    
    }
}
