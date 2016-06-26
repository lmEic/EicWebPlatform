using Lm.Eic.Framework.ProductMaster.DbAccess.Mapping;
using Lm.Eic.Framework.ProductMaster.Model;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.DbAccess.Repository
{
    public interface IConfigDataDictionaryRepository : IRepository<ConfigDataDictionaryModel>
    {
    }
    public class ConfigDataDictionaryRepository : LmProMasterRepositoryBase<ConfigDataDictionaryModel>, IConfigDataDictionaryRepository
    { }

    /// <summary>
    ///文件路径配置持久化
    /// </summary>
    public interface IConfigFilePathRepository : IRepository<ConfigFilePathModel> { }
    /// <summary>
    ///文件路径配置持久化
    /// </summary>
    public class ConfigFilePathRepository : LmProMasterRepositoryBase<ConfigFilePathModel>, IConfigFilePathRepository
    { }
}
