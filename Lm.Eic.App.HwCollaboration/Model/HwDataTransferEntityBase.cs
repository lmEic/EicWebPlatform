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
    [Serializable]
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
    /// 华为访问操作结果
    /// </summary>
    [Serializable]
    public class HwAccessOpResult
    {
        /// <summary>
        /// 操作名称
        /// </summary>
        public string OperationName { get; set; }
        public string OpResultMessage { get; set; }
        /// <summary>
        /// 操作结果变量
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// 操作数据列表
        /// </summary>
        public List<string> OpDataList { get; set; }

        private HwAccessOpResult()
        { }
        /// <summary>
        /// 设置结果
        /// </summary>
        /// <param name="operateName"></param>
        /// <param name="opResultMessage"></param>
        /// <param name="success"></param>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public static HwAccessOpResult SetResult(string operateName, string opResultMessage, bool success = true, List<string> dataList = null)
        {
            return new HwAccessOpResult()
            {
                OpDataList = dataList,
                OperationName = operateName,
                OpResultMessage = opResultMessage,
                Result = success
            };
        }
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
