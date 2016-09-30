using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel
{
    /// <summary>
    /// 订单详情
    /// </summary>
    public class OrderModel : OrderBase
    {
        /// <summary>
        ///   TA001 +TA002
        /// </summary>
        public string OrderId { get { return OrderID; } }
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
        public double Count { set; get; }
        /// <summary>
        /// 入库日期
        /// </summary>
        public DateTime InStockDate { set; get; }
        /// <summary>
        /// 订单完工日期
        /// </summary>
        public DateTime OrderFinishDate { set; get; }
    }

    /// <summary>
    /// 业务订单
    /// </summary>
    public class CopOrderModel:OrderBase
    {
        public string OrderId { get { return OrderID; } }
        /// <summary>
        /// TD003 AS 序号
        /// </summary>
        public string OrderDesc
        {set; get; }
        /// <summary>
        /// TD004 AS 品号
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        ///TD005 AS 品名
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// TD006 AS 规格
        /// </summary>
        public string ProductSpecify { get; set; }
        /// <summary>
        /// TD007 AS 仓位号
        /// </summary>
        public string WarehouseID { set; get; }
        /// <summary>
        ///  TD008 AS   订单数量
        /// </summary>
        public double ProductNumber
        { set; get; }
        /// <summary>
        /// TD009 AS 已交量
        /// </summary>
        public double FinishNumber
        { set; get; }

    }
}
