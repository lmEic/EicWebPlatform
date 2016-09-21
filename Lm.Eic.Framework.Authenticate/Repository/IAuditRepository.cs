using Lm.Eic.Framework.Authenticate.Model;
using Lm.Eic.Framework.Authenticate.Repository.Mapping;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.Authenticate.Repository
{
    /// <summary>
    ///
    /// </summary>
    public interface IAuditModelMappingRepository : IRepository<AuditModel> { }
    /// <summary>
    ///
    /// </summary>
    public class AuditModelMappingRepository : AuthenRepositoryBase<AuditModel>, IAuditModelMappingRepository
    { }

}
