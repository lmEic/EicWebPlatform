using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using System;
using System.IO;
using System.Text;
using Lm.Eic.Uti.Common.YleeMessage.Log;
using System.Collections.Generic;

namespace Lm.Eic.Uti.Common.YleeOOMapper
{
    /// <summary>
    /// 操作结果
    /// </summary>
    public class OpResult
    {
        private string message;

        /// <summary>
        /// 操作结果信息
        /// </summary>
        public string Message
        {
            get
            {
                return message;
            }
        }

        private int recordCount = 0;

        /// <summary>
        /// 操作结果返回数量
        /// </summary>
        public int RecordCount
        {
            get
            {
                return recordCount;
            }
        }
        private bool result = false;
        /// <summary>
        /// 操作结果
        /// </summary>
        public bool Result
        {
            get { return result; }
        }

        /// <summary>
        /// 对象的键值
        /// </summary>
        public decimal Id_Key { get; set; }
        /// <summary>
        /// 编辑之后的实体对象
        /// </summary>
        public object Entity { get; set; }

        public OpResult(string successMessage, bool result)
        {
            this.result = result;
            this.message = successMessage;
            if (!result)
                this.message = "系统默认提示：您的此次操发生异常，操作失败！";
        }
        public OpResult(string errorMsg)
        {
            this.result = false;
            this.message = errorMsg;
        }
        public OpResult(string successMessage, bool result, decimal idKey)
        {
            this.result = result;
            this.message = successMessage;
            this.Id_Key = idKey;
            if (!result)
                this.message = "系统默认提示：您的此次操发生异常，操作失败！";
        }

        public OpResult(string successMessage, string falseMessage, int record)
        {
            this.result = record > 0;
            this.message = successMessage;
            this.recordCount = record;
            if (!result)
                this.message = falseMessage;
        }

        public OpResult(string successMessage, string falseMessage, bool result)
        {
            this.result = result;
            this.message = successMessage;
            if (!result)
                this.message = falseMessage;
        }
        public OpResult(string successMessage, string falseMessage, int record, decimal idKey)
        {
            this.result = record > 0;
            this.message = successMessage;
            this.Id_Key = idKey;
            if (!result)
                this.message = falseMessage;
        }

        /// <summary>
        /// 附加对象
        /// </summary>
        public object Attach { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception Exception { get; private set; }
        /// <summary>
        /// 异常信息Id
        /// </summary>
        public string ExceptionId { get; private set; }
        /// <summary>
        /// 记录错误文件
        /// </summary>
        /// <param name="ex"></param>
        private void LogErrorMsgToFile(Exception ex)
        {
            ExceptionId = Guid.NewGuid().ToString("N");
            string errorLogPath = @"C:\EicSystem\WebPlatform\ErrorMsgTrace\";
            string fileName = Path.Combine(errorLogPath, ExceptionId + ".txt");
            StringBuilder sbMsg = new StringBuilder();
            sbMsg.AppendFormat("错误Id：{0}", ExceptionId).AppendLine();
            sbMsg.AppendFormat("错误信息：{0}", ex.Message).AppendLine();
            sbMsg.AppendFormat("错误描述：{0}", ex.StackTrace).AppendLine();
            sbMsg.AppendFormat("错误源：{0}", ex.Source).AppendLine();
            sbMsg.AppendFormat("发生时间：{0}", DateTime.Now).AppendLine();
            fileName.AppendFile(sbMsg.ToString());
        }
        /// <summary>
        /// 设置操作错误结果
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static OpResult SetErrorResult(Exception ex)
        {
            var opResult = new OpResult(ex.Message);
            opResult.LogErrorMsgToFile(ex);
            return opResult;
        }
       
        /// <summary>
        /// 设定操作结果
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static OpResult SetErrorResult(string errorMsg)
        {
            return new OpResult(errorMsg);
        }
        /// <summary>
        /// 设定操作结果
        /// </summary>
        /// <param name="successMessage">成功的信息</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static OpResult SetSuccessResult(string successMessage, bool result = true)
        {
            return new OpResult(successMessage, result);
        }
        public static OpResult SetSuccessResult(string successMessage, bool result, decimal idKey)
        {
            return new OpResult(successMessage, result, idKey);
        }

        public static OpResult SetResult(string successMessage, string falseMessage, int record, decimal idKey)
        {
            return new OpResult(successMessage, falseMessage, record, idKey);
        }

        public static OpResult SetResult(string successMessage, string falseMessage, int record)
        {
            return new OpResult(successMessage, falseMessage, record);
        }
        public static OpResult SetResult(string successMessage, string falseMessage, bool result)
        {
            return new OpResult(successMessage, falseMessage, result);
        }
        public static OpResult SetResult(string message, bool result)
        {
            return new OpResult(message, result);
        }
    }

    /// <summary>
    /// 数据操作类型
    /// </summary>
    public class OpMode
    {
        public const string Add = "add";
        public const string Edit = "edit";
        public const string Delete = "delete";
        public const string UpDate = "update";
        public const string UploadFile = "uploadFile";
        public const string DeleteFile = "deleteFile";
    }
    public class DataList<T> : List<T>
    {
        public string ExceptionId { get; set; }
    }
    /// <summary>
    /// OpResult的扩展方法集合
    /// </summary>
    public static class OpResultExtension
    {
        /// <summary>
        /// 扩展方法，返回异常结果
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static OpResult ExOpResult(this Exception ex)
        {
            return OpResult.SetErrorResult(ex);
        }
        /// <summary>
        /// 将异常信息记录到文件中去
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="fnName"></param>
        public static DataList<T> ExDataList<T>(this Exception ex)
        {
            OpResult result = ex.ExOpResult();
            var datas= new DataList<T>();
            datas.ExceptionId = result.ExceptionId;
            return datas;
        }
    }
}