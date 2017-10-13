using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.HwCollaboration.Model
{
    /// <summary>
    /// 采购在途Dto
    /// </summary>
    [Serializable]
    public class PurchaseOnWayDto
    {
        public List<SccOpenPOVO> sccOpenPOList { get; set; }
    }
    /// <summary>
    /// 采购在途PO信息信息模型
    /// </summary>
    [Serializable]
    public class SccOpenPOVO
    {
        /// <summary>
        /// 供应商工厂代码
        /// 非空
        /// </summary>
        public string vdFactoryCode { get; set; }
        /// <summary>
        /// 客户代码
        /// </summary>
        public string customerVendorCode { get; set; }
        /// <summary>
        /// 采购物料编码
        /// 非空
        /// </summary>
        public string itemCode { get; set; }
        /// <summary>
        /// 采购模式
        /// 非空，限制为NORMAL, VMI, DUN, JIT, OTHER
        /// </summary>
        public string businessMode { get; set; }
        /// <summary>
        /// 采购物料Open PO号
        /// 非空
        /// </summary>
        public string poNumber { get; set; }
        /// <summary>
        /// PO下达时间
        /// 非空，yyyy-MM-dd格式
        /// </summary>
        public string poPublishDateStr { get; set; }
        /// <summary>
        /// 要求到料时间
        /// 可以空，yyyy-MM-dd格式
        /// </summary>
        public string demandArrivalDateStr { get; set; }
        /// <summary>
        /// Open PO数量
        /// 非空, 正整数
        /// </summary>
        public double openPoQuantity { get; set; }
        /// <summary>
        /// Open PO 承诺交货时间
        /// 非空，yyyy-MM-dd格式
        /// </summary>
        public string promisedDeliveryDateStr { get; set; }
        /// <summary>
        /// 华为指定供应商Code
        /// </summary>
        public string componentVendorCode { get; set; }
        /// <summary>
        /// 下级供应商名称
        /// </summary>
        public string componentVendorName { get; set; }
    }
}
