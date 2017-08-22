using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.HwCollaboration.Model
{
    /// <summary>
    /// 工厂基础信息模型
    /// </summary>
    [Serializable]
    public class VendorFactoryBaseModel
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
        /// 国家
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 身份
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 工厂地址
        /// </summary>
        public string VendFactoryAddress { get; set; }
        /// <summary>
        /// 行业地位
        /// </summary>
        public string TradePosition { get; set; }
        /// <summary>
        /// 主要客户
        /// </summary>
        public string MajorCustomer { get; set; }
        /// <summary>
        /// 工厂状态
        /// </summary>
        public string VendFactoryStatus { get; set; }
    }
}
