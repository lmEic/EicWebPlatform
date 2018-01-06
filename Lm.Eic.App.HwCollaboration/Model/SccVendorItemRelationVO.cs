using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.HwCollaboration.Model
{
    /// <summary>
    /// 物料基础信息dto
    /// </summary>
    [Serializable]
    public class VendorItemRelationDto
    {
        public List<SccVendorItemRelationVO> vendorItemList { get; set; }
    }

    /// <summary>
    /// 物料基础信息
    /// </summary>
    [Serializable]
    public class SccVendorItemRelationVO
    {
        /// <summary>
        /// 供应商物料编码
        /// 非空，最大长度125
        /// </summary>
        public string vendorItemCode { get; set; }

        /// <summary>
        /// 供应商产品型号
        /// 可以空，最大长度125
        /// </summary>
        public string vendorProductModel { get; set; }

        /// <summary>
        /// 供应商物料描述
        /// 非空，最大长度500
        /// </summary>
        public string vendorItemDesc { get; set; }

        /// <summary>
        /// 物料小类
        /// 非空，最大长度125
        /// </summary>
        public string itemCategory { get; set; }

        /// <summary>
        /// 客户代码
        /// 非空，默认157
        /// </summary>
        public string customerVendorCode { get; set; }

        /// <summary>
        /// 客户物料编码
        /// 可以空，若为空，则系统默认设置为'NA'；最大长度125
        /// </summary>
        public string customerItemCode { get; set; }

        /// <summary>
        /// 客户产品型号
        /// 可以空，最大长度125
        /// </summary>
        public string customerProductModel { get; set; }

        /// <summary>
        /// 单位
        /// 非空
        /// </summary>
        public string unitOfMeasure { get; set; }

        /// <summary>
        /// 供应商Item类别
        /// 非空，限制为FG、SEMI-FG、RM
        ///对照关系：FG(成品)、SEMI-FG(半成品)、RM(原材料)
        /// </summary>
        public string inventoryType { get; set; }

        /// <summary>
        /// 良率
        /// 可以空，若为空，则系统默认设置为100；限制为0-100(包含)的数字，小数位不能超过2位
        /// </summary>
        public double goodPercent { get; set; }

        /// <summary>
        /// 货期(天）
        /// 非空，小数位不能超过1位
        /// </summary>
        public double leadTime { get; set; }

        /// <summary>
        /// 生命周期状态
        /// 非空，限制为NPI、MP、EOL
        ///对照关系：NPI(量产前)、MP(量产)、EOL(停产)
        /// </summary>
        public string lifeCycleStatus { get; set; }

    }



    /// <summary>
    /// 关键物料BOM   dto
    /// </summary>
    [Serializable]
    public class KeyMaterialDto
    {
        public List<SccKeyMaterialVO> keyMaterialList { get; set; }
    }

    /// <summary>
    /// 关键物料BOM表信息
    /// </summary>
    [Serializable]
    public class SccKeyMaterialVO
    {
        /// <summary>
        /// 父阶物料编码
        /// 非空，最大长度125，父阶物料编码和子阶物料编码不能相同
        /// </summary>
        public string vendorItemCode { get; set; }
        /// <summary>
        /// 子阶物料编码
        /// 非空，最大长度125，父阶物料编码和子阶物料编码不能相同
        /// </summary>
        public string subItemCode { get; set; }

        /// <summary>
        /// 替代组
        /// 可以空，最大长度125
        /// </summary>
        public string substituteGroup { get; set; }

        /// <summary>
        /// 父阶基础用量
        /// 非空，必须大于0且为整数
        /// </summary>
        public int baseUsedQuantity { get; set; }

        /// <summary>
        /// 子阶用量
        /// 非空，必须大于0且为整数
        /// </summary>
        public int standardQuantity { get; set; }
    }
}
