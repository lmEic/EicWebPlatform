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
    public class VendorTaskStatusModel
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
        /// 生产订单下达时间ORDER_TIME
        /// </summary>
        public string OrderTime { get; set; }
        /// <summary>
        /// 供应商组件编码VENDOR_ITEM_CODE
        /// 供应商生产订单的物料编码
        /// </summary>
        public string VendorItemCode { get; set; }

        /// <summary>
        /// 描述（品名）DESCRIPTION
        /// 供应商生产订单的物料编码品名描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 生产订单数量ORDER_QUANTITY
        /// 供应商生产订单的数量
        /// </summary>
        public float OrderQuantity { get; set; }
        /// <summary>
        /// 生产订单完工数量COMPLETE_ORDER_QUANTITY
        /// 供应商生产订单的数量
        /// </summary>
        public float CompleteOrderQuantity { get; set; }
        /// <summary>
        /// 生产订单状态ORDER_STATUS
        /// 供应商生产订单的状态
        /// </summary>
        public string OrderStatus { get; set; }
    }
}
