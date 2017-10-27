using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;

namespace Lm.Eic.App.HwCollaboration.Model
{
    /// <summary>
    /// 华为API调用结果
    /// </summary>
    public class HwAccessApiResult
    {
        /// <summary>
        /// 是否访问成功
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string errorMessage { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string errorCode { get; set; }
    }
    /// <summary>
    /// 华为数据传输日志
    /// </summary>
    public class HwDataTransferLog
    {
        /// <summary>
        /// 操作人
        /// </summary>
        public string OpPerson { get; set; }
        /// <summary>
        /// 操作标志
        /// </summary>
        public string OpSign { get; set; }
        /// <summary>
        /// 操作模块
        /// </summary>
        public string OpModule { get; set; }
        /// <summary>
        /// 数据状态：0和1  0表示已废弃，1表示使用中
        /// </summary>
        public int DataStatus { get; set; }
    }
}
