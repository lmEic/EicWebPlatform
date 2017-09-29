using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.HwCollaboration.Model
{

    /// <summary>
    /// 物料组合Dto
    /// </summary>
    public class MaterialComposeDto
    {
        public FactoryInventoryDto InvertoryDto { get; set; }

        public MaterialMakingDto MakingDto { get; set; }

        public MaterialShipmentDto ShippmentDto { get; set; }
    }
    /// <summary>
    /// 库存明细数据传输Dto
    /// </summary>
    public class FactoryInventoryDto
    {
        public List<SccFactoryInventory> factoryInventoryList { get; set; }
    }

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
        /// 供应商物料编码 厂内料号
        /// 非空
        /// </summary>
        public string vendorItemCode { get; set; }
        /// <summary>
        /// 客户编码
        /// 非空
        ///如果您是华为供应商，则可以填写：0971(代表终端)157(代表华技)
        ///如果您是非华为直接供应商，则填写在您客户在华为SRM系统注册的供应商编码
        /// </summary>
        public string customerCode { get; set; }
        /// <summary>
        /// 供应商子库
        /// 可空，如为空，系统自动设置为NA
        /// </summary>
        public string vendorStock { get; set; }
        /// <summary>
        /// 供应商货位
        /// 可空，如为空，系统自动设置为NA
        /// </summary>
        public string vendorLocation { get; set; }
        /// <summary>
        /// 入库时间
        /// 非空, yyyy-MM-dd格式
        /// </summary>
        public string stockTime { get; set; }
        /// <summary>
        /// 供应商物料编码版本
        /// 可以为空
        /// </summary>
        public string vendorItemRevision { get; set; }
        /// <summary>
        /// 库存
        /// 非空, 正整数
        /// </summary>
        public double goodQuantity { get; set; }
        /// <summary>
        /// 待检库存
        /// 可为空, 若传入, 则必须为正整数
        /// </summary>
        public double inspectQty { get; set; }
        /// <summary>
        /// 隔离品数量
        /// 可为空, 若传入, 则必须为正整数
        /// </summary>
        public double faultQty { get; set; }
    }
}
