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
    [Serializable]
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

    /// <summary>
    /// ERP物料发料明细模型
    /// </summary>
    [Serializable]
    public class ErpMaterialShipmentModel
    {
        /// <summary>
        /// 供应商工厂代码
        /// </summary>
        public string VendorFactoryCode { get; set; }
        /// <summary>
        /// 客户代码
        /// </summary>
        public string CustomerVendorCode { get; set; }
        /// <summary>
        /// 生产订单号  对应ERP的工单号码
        /// </summary>
        public string OrderNumber { get; set; }
        /// <summary>
        /// 生产待发物料编码
        /// 供应商生产订单的待发物料编码
        /// 对应ERP中的物料料号
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// 生产应发数量
        /// 供应商生产订单需求的物料编码的应发数量
        /// 对应ERP中的需领用量
        /// </summary>
        public double ShouldShipQuantity { get; set; }
        /// <summary>
        /// 生产已发数量
        /// 供应商生产订单需求的物料编码的已发数量
        /// 对应ERP中的已领用量
        /// </summary>
        public double ShippedQuantity { get; set; }
        /// <summary>
        /// BOM用量
        /// 供应商生产订单需求的物料编码的BOM用量
        /// </summary>
        public double BomUsage { get; set; }
        /// <summary>
        /// 替代组
        /// 替代料组代码
        /// </summary>
        public string SubstituteGroup { get; set; }
    }

    /// <summary>
    /// Erp关键物料Bom模型
    /// </summary>
    [Serializable]
    public class ErpMaterialKeyBomModel
    {
        /// <summary>
        /// 父阶物料编码
        /// 非空，最大长度125，父阶物料编码和子阶物料编码不能相同
        /// 对应ERP中的主件品号
        /// </summary>
        public string VendorItemCode { get; set; }
        /// <summary>
        /// 子阶物料编码
        /// 非空，最大长度125，父阶物料编码和子阶物料编码不能相同
        /// 对应ERP中的元件品号
        /// </summary>
        public string SubItemCode { get; set; }

        /// <summary>
        /// 父阶基础用量
        /// 非空，必须大于0且为整数
        /// 对应ERP中的标准批量
        /// </summary>
        public int BaseUsedQuantity { get; set; }
        /// <summary>
        /// 子阶用量
        /// 非空，必须大于0且为整数
        /// 对应于ERP中的组成用量
        /// </summary>
        public int StandardQuantity { get; set; }
    }
}
