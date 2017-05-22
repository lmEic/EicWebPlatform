using Lm.Eic.Framework.ProductMaster.DbAccess.Mapping;
using Lm.Eic.Framework.ProductMaster.Model.Tools;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.DbAccess.Repository
{
    /// <summary>
    ///合作联系人库持久化
    /// </summary>
    public interface ICollaborateContactLibRepository : IRepository<CollaborateContactLibModel> { }
    /// <summary>
    ///合作联系人库持久化
    /// </summary>
    public class CollaborateContactLibRepository : LmProMasterRepositoryBase<CollaborateContactLibModel>, ICollaborateContactLibRepository
    { }

}
