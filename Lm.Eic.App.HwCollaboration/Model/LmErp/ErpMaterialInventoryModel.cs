using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.HwCollaboration.Model.LmErp
{
    /// <summary>
    /// 物料库存明细模型
    /// </summary>
    [Serializable]
    public class ErpMaterialInventoryModel
    {
        /// <summary>
        /// 物料料号----对应华为供应商物料编码
        /// </summary>
        public string MaterialId { get; set; }
        /// <summary>
        /// 入库时间
        /// </summary>
        public string StockTime { get; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        public double GoodQuantity { get; set; }
        /// <summary>
        /// 仓库子库
        /// </summary>
        public string VendorStock { get; set; }
        /// <summary>
        /// 库位
        /// </summary>
        public string VendorLocation { get; set; }
    }
    /// <summary>
    /// 物料在制明细模型
    /// </summary>
    public class ErpMaterialMakingModel
    {
        /// <summary>
        /// 组件代码 对应ERP中的产品品号
        /// </summary>
        public string ComponentCode { get; set; }
        /// <summary>
        /// 生产订单下达时间 对应工单开工日期
        /// </summary>
        public string OrderPublishDateStr { get; set; }
        /// <summary>
        /// 生产订单号 对应工单单号
        /// </summary>
        public string OrderNumber { get; set; }
        /// <summary>
        /// 供应商生产订单的数量  对应工单中的预计产量
        /// </summary>
        public float OrderQuantity { get; set; }
        /// <summary>
        /// 供应商生产订单的数量  对应工单中的已生产量
        /// </summary>
        public float OrderCompletedQuantity { get; set; }
        /// <summary>
        /// 供应商生产订单的状态 对应ERP中的工单状态
        /// </summary>
        public string OrderStatus { get; set; }
    }
}
