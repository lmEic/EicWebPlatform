using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.App.Erp.Domain.InvManageModel;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel;
using Lm.Eic.App.Erp.DbAccess.MocManageDb.BomManageBb;
using System.Collections.Generic;
using System.Data;
using System.Text;
namespace Lm.Eic.App.Erp.DbAccess.InvManageDb
{
    /// <summary>
    /// 
    /// </summary>
  internal class InvOrderCrudFactory
    {
        /// <summary>
        /// 订单Crud
        /// </summary>
        public static InvOrderManage InvManageDb
        {
            get { return OBulider.BuildInstance<InvOrderManage>(); }
        }


    }
  public  class InvOrderManage
    {
      private MarterialBaseInfo MarterialBaasInfo(string productID )
      {
         return  BomCrudFactory.BomManageDb.GetBomFormERP_INVMB_By(productID);
      }
      private string  SqlFields
      {
          get { return " SELECT   MC001 AS 品号, MC002 AS 仓位, MC003 AS 备注, MC007 AS 人库数量, MC012 AS 人库日期  FROM INVMC  "; }
      }
      public List<FinishedProductStoreModel> GetProductStroeInfoBy(string productId)
      {
          string sqlWhere = string.Format(" where (MC001 = '{0}') ", productId);
          var marterialBaasInfo = MarterialBaasInfo(productId);
          return ErpDbAccessHelper.FindDataBy<FinishedProductStoreModel>(SqlFields, sqlWhere, (dr, m) =>
          {
              m.ProductID = dr["品号"].ToString().Trim();
              m.ProductName = marterialBaasInfo.MaterialName;
              m.ProductSpecify = marterialBaasInfo.MaterialSpecify;
              m.PutInStoreDate = dr["人库日期"].ToString().ToDate();
              m.StroeId = dr["仓位"].ToString().Trim();
              m.InStroeNumber = dr["人库数量"].ToString().ToDouble();
              m.More = dr["备注"].ToString().Trim();
          });
      }
    }
}
