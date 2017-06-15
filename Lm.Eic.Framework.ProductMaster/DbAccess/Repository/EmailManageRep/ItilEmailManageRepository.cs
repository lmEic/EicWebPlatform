using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Framework.ProductMaster.DbAccess.Mapping;



namespace Lm.Eic.Framework.ProductMaster.DbAccess.Repository
{
    public interface IItilEmailManageRepository :IRepository<ItilEmailManageModel> { }
    /// <summary>
    /// 邮箱开发模块持久层
    /// </summary>

    public class ItilEmailManageRepository: LmProMasterRepositoryBase<ItilEmailManageModel>, IItilEmailManageRepository
    { }
}
