using Lm.Eic.Framework.ProductMaster.DbAccess.Mapping;
using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.DbAccess.Repository
{
    /// <summary>
    ///模块开发管控持久层
    /// </summary>
    public interface IItilDevelopModuleManageRepository : IRepository<ItilDevelopModuleManageModel> { }
    /// <summary>
    ///模块开发管控持久层
    /// </summary>
    public class ItilDevelopModuleManageRepository : LmProMasterRepositoryBase<ItilDevelopModuleManageModel>, IItilDevelopModuleManageRepository
    { }

}
