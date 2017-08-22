using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.HwCollaboration.Model
{
    /// <summary>
    /// 库存信息明细模型
    /// </summary>
    [Serializable]
    public class SccFactoryInventory
    {
        /// <summary>
        /// 供应商工厂代码
        /// 非空
        /// </summary>
        public string vendorFactoryCode { get; set; }
        /// <summary>
        /// 供应商物料编码
        /// 非空
        /// </summary>
        public string vendorItemCode { get; set; }
        /// <summary>
        /// 物料编码版本
        /// </summary>
        public string HWItemRevision { get; set; }
        /// <summary>
        /// 客户编码
        /// </summary>
        public string CustomerCode { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        public float GoodINVQty { get; set; }
        /// <summary>
        /// 待检库存
        /// </summary>
        public float InspectQty { get; set; }
        /// <summary>
        /// 供应商子库
        /// </summary>
        public string VendorStock { get; set; }
        /// <summary>
        /// 供应商货位
        /// </summary>
        public string VendorLocation { get; set; }
        /// <summary>
        /// 隔离品数量
        /// </summary>
        public float Fault_Qty { get; set; }
        /// <summary>
        /// 协议类型(9月版新增）
        /// </summary>
        public string TYPE { get; set; }
    }
}
