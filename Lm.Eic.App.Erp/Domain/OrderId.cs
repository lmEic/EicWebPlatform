using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Erp.Domain
{
    /// <summary>
    /// 单号模型
    /// </summary>
    public class OrderId
    {
        /// <summary>
        /// 单号
        /// </summary>
        public string OId { get; set; }
        /// <summary>
        /// 单别
        /// </summary>
        public string OType { get; set; }
    }
}
