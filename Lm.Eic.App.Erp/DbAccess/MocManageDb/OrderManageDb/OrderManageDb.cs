using Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel;
using Lm.Eic.App.Erp.Domain.ProductTypeMonitorModel;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Lm.Eic.Uti.Common.YleeObjectBuilder;

namespace Lm.Eic.App.Erp.DbAccess.MocManageDb.OrderManageDb
{

    /// <summary>
    /// 工单管理Crud工厂
    /// </summary>
    internal class OrderCrudFactory
    {
        /// <summary>
        /// 工单详情Crud
        /// </summary>
        public static OrderDetailsDb OrderDetailsDb
        {
            get { return OBulider.BuildInstance<OrderDetailsDb>(); }
        }

        /// <summary>
        /// 工单物料Crud
        /// </summary>
        public static OrderMaterialDb OrderMaterialDb
        {
            get { return OBulider.BuildInstance<OrderMaterialDb>(); }
        }
    }

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
                m.Code = dr["TA002"].ToString().Trim ();
                m.Category = dr["TA001"].ToString().Trim ();
                m.ProductID = dr["TA006"].ToString().Trim ();
                m.ProductName = dr["TA034"].ToString().Trim ();
                m.ProductSpecify = dr["TA035"].ToString().Trim ();
                m.Count = dr["TA016"].ToString().Trim ().ToDouble();
                m.OrderFinishDate = DateTime.ParseExact(dr["TA010"].ToString().Trim (), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                m.InStockDate = DateTime.ParseExact(dr["TA063"].ToString().Trim (), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            });
            return ListModels.FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContProductName"></param>
        /// <returns></returns>
        public List<OrderModel> GetOrderListBy(string ContProductName)
        {
            string sqlWhere = string.Format(" where TA001='{0}' and TA002='{1}'", ContProductName);
            return ErpDbAccessHelper.FindDataBy<OrderModel>(SqlFields, sqlWhere, (dr, m) =>
            {
                m.Code = dr["TA002"].ToString().Trim();
                m.Category = dr["TA001"].ToString().Trim();
                m.ProductID = dr["TA006"].ToString().Trim();
                m.ProductName = dr["TA034"].ToString().Trim();
                m.ProductSpecify = dr["TA035"].ToString().Trim();
                m.Count = dr["TA016"].ToString().Trim().ToDouble();
                m.OrderFinishDate = DateTime.ParseExact(dr["TA010"].ToString().Trim(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                m.InStockDate = DateTime.ParseExact(dr["TA063"].ToString().Trim(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            });
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
        public List<OrderMaterialModel> GetOrderMaterialListBy(string orderId)
        {
            var idm = ErpDbAccessHelper.DecomposeID(orderId);
            string sqlWhere = string.Format(" where TB001='{0}' and TB002='{1}' ", idm.Category, idm.Code);
            return ErpDbAccessHelper.FindDataBy<OrderMaterialModel>(SqlFields, sqlWhere, (dr, m) =>
            {
                m.MaterialId = dr["材料品号"].ToString().Trim ();
                m.MaterialName = dr["品名"].ToString().Trim ();
                m.MaterialSpecify = dr["规格"].ToString().Trim();
                m.Unit = dr["单位"].ToString().Trim();
                m.ReceiveCount = dr["需领用量"].ToString().Trim ().ToDouble();
            });
        }
    }
}
