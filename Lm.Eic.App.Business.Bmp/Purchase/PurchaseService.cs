using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.Uti.Common.YleeObjectBuilder;


namespace Lm.Eic.App.Business.Bmp.Purchase
{
  /// <summary>
  /// 采购管理服务接口
  /// </summary>
  public static class PurchaseService
    {
      /// <summary>
      /// 请购管理者
      /// </summary>
      public static RequisitionManager ReqManager
      {
          get { return OBulider.BuildInstance<RequisitionManager>(); }
      }

      /// <summary>
      /// 采购管理者
      /// </summary>
      public static PurchaseManager PurManager
      {
          get { return OBulider.BuildInstance<PurchaseManager>(); }
      }

      /// <summary>
      /// 进货管理者
      /// </summary>
      public static StockManager StoManager
      {
          get { return OBulider.BuildInstance<StockManager>(); }
      }
    }
}
