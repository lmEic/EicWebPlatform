using Lm.Eic.App.Erp.DbAccess.QuantitySampleDb;
using Lm.Eic.Uti.Common.YleeObjectBuilder;

namespace Lm.Eic.App.Erp.Bussiness.QuantityManage
{
    public static   class QuantityDBManager
    {
        /// <summary>
        /// 质量管理访问接口
        /// </summary>
      public static MaterialSampleDb QuantityPurchseDb
      {
          get { return OBulider.BuildInstance<MaterialSampleDb>(); }
      }

    }
}
