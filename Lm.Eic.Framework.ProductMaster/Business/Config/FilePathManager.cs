using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Framework.ProductMaster.DbAccess.Repository;
using Lm.Eic.Framework.ProductMaster.Model;
using Lm.Eic.Uti.Common.YleeOOMapper;


namespace Lm.Eic.Framework.ProductMaster.Business.Config
{
  /// <summary>
  /// 文件路径管理器
  /// </summary>
  public  class FilePathManager
    {
      private IConfigFilePathRepository irep = null;

      public FilePathManager()
      {
          this.irep = new ConfigFilePathRepository();
      }


      /// <summary>
      /// 根据程序集名称和模块键值获取文件路径信息
      /// </summary>
      /// <param name="assemblyName"></param>
      /// <param name="moduleKey"></param>
      /// <returns></returns>
      public ConfigFilePathModel GetFilePathInfo(string assemblyName, string moduleKey)
      {
          return this.irep.Entities.FirstOrDefault(e => e.AssemblyName == assemblyName && e.ModuleKey == moduleKey);
      }
    }

  /// <summary>
  /// 文件路径常量键值
  /// </summary>
  public class FilePathConstKey
  {
      public const string AsmHRM = "HRM";

      public const string ModlAreaCodeInfo = "AreaCodeInfo";
  }
}
