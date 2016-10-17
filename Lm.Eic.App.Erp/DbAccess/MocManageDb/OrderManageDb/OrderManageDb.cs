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
            get { return "Select TA001,TA002,TA006,TA011,TA034,TA035,TA015,TA017,TA010,TA063 from MOCTA "; }
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
                m.OrderId = string.Format("{0}-{1}", dr["TA001"].ToString().Trim(), dr["TA002"].ToString().Trim()); ;
                m.ProductID = dr["TA006"].ToString().Trim ();
                m.ProductName = dr["TA034"].ToString().Trim ();
                m.ProductSpecify = dr["TA035"].ToString().Trim ();
                m.OrderFinishStatus = OrderFinishStatusConverter(dr["TA011"].ToString().Trim());
                m.Count = dr["TA015"].ToString().Trim ().ToDouble();
                m.InStoreCount = dr["TA017"].ToString().Trim().ToDouble();
                m.OrderFinishDate = DateTime.ParseExact(dr["TA010"].ToString().Trim (), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                m.InStockDate = DateTime.ParseExact(dr["TA063"].ToString().Trim (), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            });
            return ListModels.FirstOrDefault();
        }
       /// <summary>
       /// 未完工的工单
       /// </summary>
        /// <param name="ContainsProductName">产品型号或规格</param>
       /// <returns></returns>
        public List<OrderModel> GetUnfinishedOrderBy(string ContainsProductName)
        {
            string sqlWhere = string.Format(" WHERE (NOT (TA011 = 'Y' OR  TA011 = 'y')) AND (TA034 LIKE '%{0}%')", ContainsProductName);
            return ErpDbAccessHelper.FindDataBy<OrderModel>(SqlFields, sqlWhere, (dr, m) =>
            {
                m.OrderId = string.Format("{0}-{1}", dr["TA001"].ToString().Trim(), dr["TA002"].ToString().Trim()); ;
                m.ProductID = dr["TA006"].ToString().Trim();
                m.ProductName = dr["TA034"].ToString().Trim();
                m.ProductSpecify = dr["TA035"].ToString().Trim();
                m.OrderFinishStatus = OrderFinishStatusConverter(dr["TA011"].ToString().Trim());
                m.Count = dr["TA015"].ToString().Trim().ToDouble();
                m.InStoreCount = dr["TA017"].ToString().Trim().ToDouble();
                m.OrderFinishDate = DateTime.ParseExact(dr["TA010"].ToString().Trim(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                m.InStockDate = DateTime.ParseExact(dr["TA063"].ToString().Trim(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            });
        }
        /// <summary>
        /// 所有的工单
        /// </summary>
        /// <param name="ContainsProductName">产品型号或规格</param>
        /// <returns></returns>
        public List<OrderModel> GetAllOrderBy(string ContainsProductName)
        {
            string sqlWhere = string.Format(" WHERE (TA034 LIKE '%{0}%') AND (NOT (TA010 = '')) AND (NOT (TA063 = '')) OR(TA035 LIKE '%{0}%') AND (NOT (TA010 = '')) AND (NOT (TA063 = ''))", ContainsProductName);
            return ErpDbAccessHelper.FindDataBy<OrderModel>(SqlFields, sqlWhere, (dr, m) =>
            {
                m.OrderId = string.Format("{0}-{1}", dr["TA001"].ToString().Trim(), dr["TA002"].ToString().Trim());
                m.ProductID = dr["TA006"].ToString().Trim();
                m.ProductName = dr["TA034"].ToString().Trim();
                m.ProductSpecify = dr["TA035"].ToString().Trim();
                m.OrderFinishStatus = OrderFinishStatusConverter(dr["TA011"].ToString().Trim());
                m.Count = dr["TA015"].ToString().Trim().ToDouble();
                m.InStoreCount = dr["TA017"].ToString().Trim().ToDouble();
                m.OrderFinishDate = DateTime.ParseExact(dr["TA010"].ToString().Trim(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                m.InStockDate = DateTime.ParseExact(dr["TA063"].ToString().Trim(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

            });
        }

        private string OrderFinishStatusConverter(string orderFinishStatusId)
        {
            string returnstring = string.Empty;
            switch (orderFinishStatusId)
            {
                case "1":
                    returnstring= "未生产";
                    break;
                case "2":
                    returnstring="已发料";
                    break;
                case "3":
                    returnstring= "生产中";
                    break;
                case "Y":
                    returnstring= "已完工";
                    break;
                case "y":
                    returnstring= "指定完工";
                    break;
                default:
                    returnstring= string.Empty;
                    break;
            }
            return returnstring;
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
