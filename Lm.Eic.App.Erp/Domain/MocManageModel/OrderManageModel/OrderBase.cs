using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel
{
    /// <summary>
    /// OrderModel型基类
    /// </summary>

    public class OrderBase
    {
        /// <summary>
        /// 单别
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        public string Code { get; set; }

        protected string OrderID { get { return string.Format("{0}-{1}", Category.Trim(), Code.Trim()); } }

    }
}
