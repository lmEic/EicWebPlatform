using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.HwCollaboration.Model
{
    /// <summary>
    ///供应商任务令发料明细模型
    /// </summary>
    [Serializable]
    public class SccMaterialShipmentVO
    {
        /// <summary>
        /// 供应商工厂代码
        /// </summary>
        public string vendorFactoryCode { get; set; }
        /// <summary>
        /// 客户代码
        /// </summary>
        public string customerVendorCode { get; set; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        public string orderNumber { get; set; }
        /// <summary>
        /// 生产待发物料编码
        /// 供应商生产订单的待发物料编码
        /// </summary>
        public string itemCode { get; set; }
        /// <summary>
        /// 生产应发数量
        /// 供应商生产订单需求的物料编码的应发数量
        /// </summary>
        public double shouldShipQuantity { get; set; }
        /// <summary>
        /// 生产已发数量
        /// 供应商生产订单需求的物料编码的已发数量
        /// </summary>
        public double shippedQuantity { get; set; }
        /// <summary>
        /// BOM用量
        /// 供应商生产订单需求的物料编码的BOM用量
        /// </summary>
        public double bomUsage { get; set; }
        /// <summary>
        /// 替代组
        /// 替代料组代码
        /// </summary>
        public string substituteGroup { get; set; }
    }
}
