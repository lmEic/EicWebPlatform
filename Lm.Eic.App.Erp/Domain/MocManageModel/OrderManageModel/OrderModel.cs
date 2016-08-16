using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel
{
    /// <summary>
    /// 订单详情
    /// </summary>
    public class OrderModel  :OrderBase 
    {

        //ToDo:产品品号，产品品名,产品规格，总批量，出货日期，完工日期，等.....       \

         /// <summary>
         ///   TA001  +TA002
         /// </summary>
        public string OrderId { get { return ID; } }
        /// <summary>
        /// 品号 TA006
        /// </summary>
        public string ProductID { get; set; }

        /// <summary>
        /// 品名  TA034
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 规格 
        /// </summary>
        public string ProductSpecify { get; set; }
         /// <summary>
        /// 总批量
         /// </summary>
        public double  ProductCount { set; get; }
        /// <summary>
        ///     出货日期
        /// </summary>
        public DateTime ProductInStockDate { set; get; }
        /// <summary>
        /// 订单完工日期
        /// </summary>
        public DateTime OrderIdFinishDate { set; get; }
    }
}
