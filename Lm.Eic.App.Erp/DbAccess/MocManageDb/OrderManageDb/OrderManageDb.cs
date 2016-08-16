using Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel;
using  Lm.Eic.Uti.Common.YleeExtension.Conversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Lm.Eic.App.Erp.DbAccess.MocManageDb.OrderManageDb
{
    public class OrderDetailsDb
    {

        private string OrderSqlString
        {
            get { return "Select TA001,TA002,TA006,TA034,TA035,TA016,TA010,TA063 from MOCTA"; };
        }

        private void ConvertToModel(DataRow dr, OrderModel m)
        {
            m.Code = dr["TA002"].ToString();
            m.Category = dr["TA001"].ToString();
            m.ProductID = dr["TA006"].ToString();
            m.ProductName = dr["TA034"].ToString();
            m.ProductSpecify = dr["TA035"].ToString();
            m.ProductCount = dr["TA016"].ToString().ToDouble();
            m.OrderIdFinishDate = DateTime.ParseExact(dr["TA010"].ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            m.ProductInStockDate = DateTime.ParseExact(dr["TA063"].ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
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
            var ListModels=  ErpDbAccessHelper.FindDataBy<OrderModel>(OrderSqlString, sqlWhere, (dr, m) =>
            {
                this.ConvertToModel(dr, m);
            });


            return ListModels.FirstOrDefault();
           
        }
    }

    public class OrderMaterialDb
    {


        private string OrderMaterialSqlString
        {
            get { return " select TB003 AS 材料品号,TB012 as 品名,TB013 as 规格,TB007 as 单位,TB004 as 需领用量 from MOCTB "; }
        }

        private void ConvertToModel(DataRow dr, MaterialModel m)
        {
            m.MaterialId = dr["材料品号"].ToString();
            m.MaterialName = dr["品名"].ToString();
            m.MaterialSpecify = dr["规格"].ToString();
            m.MaterialUnit = dr["单位"].ToString();
            m.MaterialReceiveCount = dr["需领用量"].ToString().ToDouble ();
        }
        /// <summary>
        /// 获取工单物料列表
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<MaterialModel> GetOrderMaterialListBy(string orderId)
        {

            var idm = ErpDbAccessHelper.DecomposeID(orderId);
            ///除掉    “  PE袋  隔板    纸箱    ”
            string sqlWhere = string.Format(" where TB001='{0}' and TB002='{1}' and TB012<>'PE袋' and TB012 not like '隔板%' and TB012 not like '纸箱%'", idm.Category, idm.Code);
            return ErpDbAccessHelper.FindDataBy<MaterialModel>(OrderMaterialSqlString, sqlWhere, (dr, m) =>
            {
                this.ConvertToModel(dr, m);
            });


           
           


            
        }

    }
}
