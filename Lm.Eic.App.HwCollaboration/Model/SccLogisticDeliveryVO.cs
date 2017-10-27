using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.HwCollaboration.Model
{
    /// <summary>
    /// 物料基础信息模型
    /// </summary>
    [Serializable]
    public class SccLogisticDeliveryVO
    {
        /// <summary>
        /// 运输方式
        /// 非空，枚举值：LAND(陆运),SHIPPING(海运),AIR(空运),OTHER(其他)
        /// </summary>
        public string deliveryType { get; set; }
        /// <summary>
        /// 运单号
        /// 非空
        /// </summary>
        public string deliveryNumber { get; set; }
        /// <summary>
        /// ASN号
        /// 非空，真实的ASN号，或'NA'，如果有多个，用英文逗号分隔
        /// </summary>
        public string asnShipmentNumber { get; set; }
        /// <summary>
        ///始发地
        ///非空
        /// </summary>
        public string departureAddress { get; set; }
        /// <summary>
        /// 物流状态
        /// 非空，OPEN, CLOSED
        /// </summary>
        public string deliveryStatus { get; set; }
        /// <summary>
        /// 物流当前位置列表
        /// </summary>
        public List<SccLogisticLineVO> logisticNodeList { get; set; }
    }
    /// <summary>
    /// 物流当前位置单据
    /// </summary>
    [Serializable]
    public class SccLogisticLineVO
    {
        /// <summary>
        /// 到达节点时间
        /// 非空，yyyy-MM-dd HH:mm:ss格式
        /// </summary>
        public string arrivePointTime { get; set; }
        /// <summary>
        /// 物流当前位置
        /// 非空
        /// </summary>
        public string deliveryCurrentLocaion { get; set; }
    }
}
