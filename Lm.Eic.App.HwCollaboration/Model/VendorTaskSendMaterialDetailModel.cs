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
    public class VendorTaskSendMaterialDetailModel
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
        /// 工厂代码
        /// </summary>
        public string VendorFactoryCode { get; set; }

        /// <summary>
        /// 生产订单号
        /// ORDER_NUMBER
        /// </summary>
        public string OrderNumber { get; set; }
        /// <summary>
        /// 客户代码
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// 生产待发物料编码
        /// PRO_DUE_OUT_MATERIAL_CODE
        /// 供应商生产订单的待发物料编码
        /// </summary>
        public string ProDueOutMaterialCode { get; set; }
        /// <summary>
        /// 生产应发数量
        /// PRO_SHOULD_SEND_QUANTITY
        /// 供应商生产订单需求的物料编码的应发数量
        /// </summary>
        public int ProShouldSendQuantity { get; set; }
        /// <summary>
        /// 生产已发数量
        /// PRO_HAS_SENT_QUANTITY
        /// 供应商生产订单需求的物料编码的已发数量
        /// </summary>
        public int ProHasSentQuantity { get; set; }
        /// <summary>
        /// BOM用量
        /// BOM_AMOUNT_USED
        /// 供应商生产订单需求的物料编码的BOM用量
        /// </summary>
        public int BomAmountUsed { get; set; }
        /// <summary>
        /// 替代组
        /// SUBSTITUTION_GROUP
        /// 替代料组代码
        /// </summary>
        public string SubstitutionGroup { get; set; }
    }
}
