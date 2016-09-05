using Lm.Eic.App.Erp.DbAccess.PurchaseManageDb;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
namespace Lm.Eic.App.Erp.Bussiness.PurchaseManage
{
    public static class PurchaseDbManager
    {
      /// <summary>
      /// 请购数据访问接口
      /// </summary>
      public static RequisitionDb RequisitionDb
      {
          get 
          {
              return OBulider.BuildInstance<RequisitionDb>();
          }
      }
      /// <summary>
      /// 采购数据访问接口
      /// </summary>
      public static PurchaseDb PurchaseDb
      {
          get
          {
              return OBulider.BuildInstance<PurchaseDb>();
          }
      }
      /// <summary>
      /// 进货数据访问接口
      /// </summary>
      public static StockDb StockDb
      {
          get
          { 
              return OBulider.BuildInstance<StockDb>(); 
          }
      }

    }
}
