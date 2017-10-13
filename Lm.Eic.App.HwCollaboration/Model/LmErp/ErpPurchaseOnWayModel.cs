using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.HwCollaboration.Model.LmErp
{
    public class ErpPurchaseOnWayModel
    {
        /// <summary>
        /// 供应商工厂代码
        /// 非空
        /// </summary>
        public string VdFactoryCode { get; set; }
        /// <summary>
        /// 客户代码
        /// </summary>
        public string CustomerVendorCode { get; set; }
        /// <summary>
        /// 采购物料编码
        /// 非空
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// 采购模式
        /// 非空，限制为NORMAL, VMI, DUN, JIT, OTHER
        /// </summary>
        public string BusinessMode { get; set; }
        /// <summary>
        /// 采购物料Open PO号
        /// 非空
        /// </summary>
        public string PoNumber { get; set; }
        /// <summary>
        /// PO下达时间
        /// 非空，yyyy-MM-dd格式
        /// </summary>
        public string PoPublishDateStr { get; set; }
        /// <summary>
        /// 要求到料时间
        /// 可以空，yyyy-MM-dd格式
        /// </summary>
        public string DemandArrivalDateStr { get; set; }
        /// <summary>
        /// Open PO数量
        /// 非空, 正整数
        /// </summary>
        public double OpenPoQuantity { get; set; }
        /// <summary>
        /// Open PO 承诺交货时间
        /// 非空，yyyy-MM-dd格式
        /// </summary>
        public string PromisedDeliveryDateStr { get; set; }
        /// <summary>
        /// 华为指定供应商Code
        /// </summary>
        public string ComponentVendorCode { get; set; }
        /// <summary>
        /// 下级供应商名称
        /// </summary>
        public string ComponentVendorName { get; set; }
    }
}
