using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.HwCollaboration.Model
{
    /// <summary>
    /// 物料基础信息模型
    /// </summary>
    [Serializable]
    public class VendorItemRelationInfoModel
    {
        /// <summary>
        /// 供应商代码
        /// </summary>
        public string VendorCode { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string VendorName { get; set; }
        /// <summary>
        /// 供应商物料编码
        /// </summary>
        public string VendorItemCode { get; set; }
        /// <summary>
        /// 供应商产品型号
        /// </summary>
        public string VendorItemType { get; set; }
        /// <summary>
        /// 供应商物料描述
        /// </summary>
        public string VendorItemDesc { get; set; }
        /// <summary>
        /// 物料小类
        /// </summary>
        public string ItemCategory { get; set; }
        /// <summary>
        /// 客户代码
        /// </summary>
        public string CustomerCodeHW { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 客户物料编码
        /// </summary>
        public string CustomerItemCode { get; set; }
        /// <summary>
        /// 客户产品型号
        /// </summary>
        public string CustomerItemType { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 供应商Item类别
        /// </summary>
        public string VendorInventoryType { get; set; }
        /// <summary>
        /// 良率%
        /// </summary>
        public float YieldRate { get; set; }
        /// <summary>
        /// 货期(天）
        /// </summary>
        public int LeadTime { get; set; }
        /// <summary>
        /// 生命周期状态
        /// </summary>
        public string Item_cycle_Status { get; set; }
    }
}
