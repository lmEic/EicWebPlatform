using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.HwCollaboration.Model
{
    /// <summary>
    /// 人力信息工厂单据
    /// </summary>
    [Serializable]
    public class SccManpowerMain
    {
        /// <summary>
        /// 工厂代码
        /// 非空
        /// </summary>
        public string vendorFactoryCode { get; set; }
        /// <summary>
        /// 工厂人员离职率(%)
        /// 数字，非空，范围:0-100
        /// </summary>
        public double hrLeavePercent { get; set; }
        /// <summary>
        /// 工厂总人数（人）
        /// 数字，非空
        /// </summary>
        public double manpowerTotalQuantity { get; set; }
        /// <summary>
        /// 工厂人员总缺口（人）
        /// 数字，非空
        /// </summary>
        public double manpowerGapQuantity { get; set; }
        /// <summary>
        /// 工厂人员补充计划(人)
        /// 数字，非空
        /// </summary>
        public double manpowerAddQuantity { get; set; }
        /// <summary>
        /// 关键部门单据列表
        /// 非空
        /// </summary>
        public List<SccManpowerLine> keyDeptDataList { get; set; }
    }
    /// <summary>
    /// 人力信息关键部门单据
    /// </summary>
    [Serializable]
    public class SccManpowerLine
    {
        /// <summary>
        /// 关键工序/部门名称_相对华为需求
        /// 非空
        /// </summary>
        public string keyDeptName { get; set; }
        /// <summary>
        /// 关键工序/部门人员需求_相对华为需求
        /// 数字，非空
        /// </summary>
        public double hrQequestQuantity { get; set; }
        /// <summary>
        /// 关键工序/部门人员缺口_相对华为需求
        /// 数字，非空
        /// </summary>
        public double hrGapQuantity { get; set; }
        /// <summary>
        /// 工序/部门人员补充计划_相对华为需求
        /// 数字，非空
        /// </summary>
        public double hrAddQuantity { get; set; }
        /// <summary>
        /// 关键工序/部门人员离职率_相对华为需求
        /// 数字，非空，范围:0-100
        /// </summary>
        public double hrLeavePercent { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string description { get; set; }
    }
}
