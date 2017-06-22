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
    /// 模块开发持久层
    /// </summary>
    public interface IItilEmailManageRepository : IRepository<ItilEmailManageModel> 
    {

    }
    public class ItilEmailManageRepository : LmProMasterRepositoryBase<ItilEmailManageModel>, IItilEmailManageRepository { }

}
