using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.HwCollaboration.Model
{
    /// <summary>
    /// 供应商关键物料BOM信息模型
    /// </summary>
    [Serializable]
    public class VendorItemKeyBomModel
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
        /// 父阶物料编码
        /// </summary>
        public string VendorItemCode { get; set; }

        /// <summary>
        /// 子阶物料编码
        /// </summary>
        public string SubItemCode { get; set; }
        /// <summary>
        /// 父阶基础用量
        /// </summary>
        public float FatherStandardQty { get; set; }
        /// <summary>
        /// 子阶基础用量
        /// </summary>
        public float StandardQty { get; set; }

        /// <summary>
        /// 替代组
        /// </summary>
        public float ReplaceGroup { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public float CreationBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// 最后更新人
        /// </summary>
        public string LastUpdateBy { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateDate { get; set; }
    }
}
