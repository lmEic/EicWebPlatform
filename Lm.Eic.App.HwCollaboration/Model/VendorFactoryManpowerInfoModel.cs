using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.HwCollaboration.Model
{
    /// <summary>
    /// 工厂人力信息模型
    /// </summary>
    [Serializable]
    public class VendorFactoryManpowerInfoModel
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
        /// 工厂名称
        /// </summary>
        public string VendorFactoryName { get; set; }
        /// <summary>
        /// 工厂人员离职率(%)
        /// </summary>
        public float FactoryHRLeaveRate { get; set; }
        /// <summary>
        /// 工厂总人数（人）
        /// </summary>
        public int FactoryManpowerTotal { get; set; }
        /// <summary>
        /// 工厂人员总缺口(人)
        /// </summary>
        public int FactoryManpowerGap { get; set; }
        /// <summary>
        /// 工厂人员补充计划(人)
        /// </summary>
        public int FactoryManpowerAdd { get; set; }
        /// <summary>
        /// 关键工序/部门名称_相对华为需求
        /// </summary>
        public string KeyDeptNameHW { get; set; }
        /// <summary>
        /// 关键工序/部门人员需求_相对华为需求
        /// </summary>
        public int KeyDeptHRRequestHW { get; set; }
        /// <summary>
        /// 关键工序/部门人员缺口_相对华为需求
        /// </summary>
        public int KeyDeptHRGapHW { get; set; }
        /// <summary>
        /// 关键工序/部门人员补充计划_相对华为需求
        /// </summary>
        public int KeyDeptHRAddHW { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
