using Lm.Eic.Framework.Authenticate.Model;
using Lm.Eic.Uti.Common.YleeDbHandler;
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
