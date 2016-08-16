using Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel;
using  Lm.Eic.Uti.Common.YleeExtension.Conversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Lm.Eic.App.Erp.DbAccess.MocManageDb.OrderManageDb
{
    /// <summary>
    /// 工单详情Db
    /// </summary>
    public class OrderDetailsDb
    {
        /// <summary>
        /// Sql
        /// </summary>
        private string SqlFields
        {
            get { return "Select TA001,TA002,TA006,TA034,TA035,TA016,TA010,TA063 from MOCTA"; }
        }
        /// <summary>
        /// 获取工单详情
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public OrderModel GetOrderDetailsBy(string orderID)
        {
            var idm = ErpDbAccessHelper.DecomposeID(orderID);
            string sqlWhere = string.Format(" where TA001='{0}' and TA002='{1}'", idm.Category, idm.Code);
            var ListModels = ErpDbAccessHelper.FindDataBy<OrderModel>(SqlFields, sqlWhere, (dr, m) =>
            {
                m.Code = dr["TA002"].ToString();
                m.Category = dr["TA001"].ToString();
                m.ProductID = dr["TA006"].ToString();
                m.ProductName = dr["TA034"].ToString();
                m.ProductSpecify = dr["TA035"].ToString();
                m.Count = dr["TA016"].ToString().ToDouble();
                m.OrderFinishDate = DateTime.ParseExact(dr["TA010"].ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                m.InStockDate = DateTime.ParseExact(dr["TA063"].ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            });
            return ListModels.FirstOrDefault();
        }
    }

    /// <summary>
    /// 工单物料Db
    /// </summary>
    public class OrderMaterialDb
    {
        /// <summary>
        /// Sql
        /// </summary>
        private string SqlFields
        {
            get { return " select TB003 AS 材料品号,TB012 as 品名,TB013 as 规格,TB007 as 单位,TB004 as 需领用量 from MOCTB "; }
        }
        /// <summary>
        /// 获取工单物料列表
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<MaterialModel> GetOrderMaterialListBy(string orderId)
        {
            var idm = ErpDbAccessHelper.DecomposeID(orderId);
            string sqlWhere = string.Format(" where TB001='{0}' and TB002='{1}' ", idm.Category, idm.Code);
            return ErpDbAccessHelper.FindDataBy<MaterialModel>(SqlFields, sqlWhere, (dr, m) =>
            {
                m.MaterialId = dr["材料品号"].ToString();
                m.MaterialName = dr["品名"].ToString();
                m.MaterialSpecify = dr["规格"].ToString();
                m.Unit = dr["单位"].ToString();
                m.ReceiveCount = dr["需领用量"].ToString().ToDouble();
            });
        }
    }
}
