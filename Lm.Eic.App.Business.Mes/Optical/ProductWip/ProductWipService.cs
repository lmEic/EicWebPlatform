using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
namespace Lm.Eic.App.Business.Mes.Optical.ProductWip
{
  /// <summary>
  /// WIP服务窗口
  /// </summary>
  public static class ProductWipService
    {
      /// <summary>
      /// WIP数据管理器
      /// </summary>
      public static ProductWipDataManager WipDataManager
      {
          get
          {
              return OBulider.BuildInstance<ProductWipDataManager>();
          }
      }
    }
}
