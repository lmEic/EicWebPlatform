using Lm.Eic.Framework.ProductMaster.DbAccess.Mapping;
using Lm.Eic.Framework.ProductMaster.Model.EmailConfigInfo;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.DbAccess.Repository
{
    /// <summary>
    ///邮件配置仓储
    /// </summary>
    public interface IConfig_MailInfoRepository : IRepository<Config_MailInfoModel> { }
    /// <summary>
    ///邮件配置仓储
    /// </summary>
    public class Config_MailInfoRepository : LmProMasterRepositoryBase<Config_MailInfoModel>, IConfig_MailInfoRepository
    { }

}
