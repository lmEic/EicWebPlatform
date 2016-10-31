using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel
{
    /// <summary>
    /// 订单详情
    /// </summary>
    public class OrderModel
    {
        /// <summary>
        /// 工单单号
        /// </summary>
        public string OrderId { get; set; }
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
        ///订单总数TA015
        /// </summary>
        public double Count { set; get; }
        /// <summary>
        ///订单已入库数TA017
        /// </summary>
        public double InStoreCount { set; get; }
        /// <summary>
        /// 订单完工状态
        /// </summary>
        public string OrderFinishStatus { set;get ; }
        /// <summary>
        /// 入库日期
        /// </summary>
        public DateTime InStockDate { set; get; }
        /// <summary>
        /// 订单完工日期
        /// </summary>
        public DateTime OrderFinishDate { set; get; }

    }
}
