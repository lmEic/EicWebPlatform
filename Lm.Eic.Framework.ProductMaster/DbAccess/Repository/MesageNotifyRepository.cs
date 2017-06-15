using Lm.Eic.Framework.ProductMaster.DbAccess.Mapping;
using Lm.Eic.Framework.ProductMaster.Model.MessageNotify;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.DbAccess.Repository
{
    /// <summary>
    ///配置消息通知地址
    /// </summary>
    public interface IConfigNotifyAddressRepository : IRepository<ConfigNotifyAddressModel> { }
    /// <summary>
    ///配置消息通知地址
    /// </summary>
    public class ConfigNotifyAddressRepository : LmProMasterRepositoryBase<ConfigNotifyAddressModel>, IConfigNotifyAddressRepository
    { }
}