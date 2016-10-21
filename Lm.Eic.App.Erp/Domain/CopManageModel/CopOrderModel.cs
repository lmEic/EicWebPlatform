using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Erp.Domain.CopManageModel
{
    /// <summary>
    /// 业务订单
    /// </summary>
    public class CopOrderModel
    {
        public string OrderId { get; set; }
        /// <summary>
        /// TD003 AS 序号
        /// </summary>
        public string OrderDesc
        { set; get; }
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
