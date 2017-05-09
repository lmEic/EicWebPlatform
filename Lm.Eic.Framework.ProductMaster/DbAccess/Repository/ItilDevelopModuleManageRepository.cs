using Lm.Eic.Framework.ProductMaster.DbAccess.Mapping;
using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using Lm.Eic.Uti.Common.YleeDbHandler;

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

    /// <summary>
    ///模块开发管控操作履历持久层
    /// </summary>
    public interface IItilDevelopModuleManageChangeRecordRepository : IRepository<ItilDevelopModuleManageChangeRecordModel> { }
    /// <summary>
    ///模块开发管控操作履历持久层
    /// </summary>
    public class ItilDevelopModuleManageChangeRecordRepository : LmProMasterRepositoryBase<ItilDevelopModuleManageChangeRecordModel>, IItilDevelopModuleManageChangeRecordRepository
    { }

}
