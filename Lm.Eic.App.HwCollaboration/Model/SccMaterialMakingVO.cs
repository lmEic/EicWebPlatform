using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.HwCollaboration.Model
{
    /// <summary>
    /// 供应商任务令(在制)明细模型
    /// </summary>
    [Serializable]
    public class SccMaterialMakingVO
    {
        /// <summary>
        /// 工厂代码
        /// </summary>
        public string vendorFactoryCode { get; set; }
        /// <summary>
        /// 客户代码
        /// </summary>
        public string customerVendorCode { get; set; }
        /// <summary>
        /// 组件代码
        /// </summary>
        public string componentCode { get; set; }
        /// <summary>
        /// 组件描述
        /// </summary>
        public string componentDescription { get; set; }
        /// <summary>
        /// 生产订单下达时间
        /// </summary>
        public string orderPublishDateStr { get; set; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        public string orderNumber { get; set; }
        /// <summary>
        /// 供应商生产订单的数量
        /// </summary>
        public float orderQuantity { get; set; }
        /// <summary>
        /// 供应商生产订单的数量
        /// </summary>
        public float orderCompletedQuantity { get; set; }
        /// <summary>
        /// 供应商生产订单的状态
        /// </summary>
        public string orderStatus { get; set; }
    }
}
