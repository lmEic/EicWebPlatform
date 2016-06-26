using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Lm.Eic.Uti.LogMessageNet.LeeLog4Net
{
    /// <summary>
    /// 通用文件日志记录器
    /// 文件存放位置：应用程序输出文件夹
    /// \bin\CommonFileLog\InfoFileLogMessage\
    /// 日志文件记录格式：yyyyMMdd.html
    /// </summary>
    public class CommonInfoLogger
    {
        private ILog infoLogger;
        public CommonInfoLogger()
        {
            this.infoLogger = LogManager.GetLogger("commonInfoLogger");
        }
        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="message"></param>
        public void LogInfo(string message)
        {
            if (infoLogger.IsInfoEnabled)
            {
                infoLogger.Info(message);
            }
        }
    }

    /// <summary>
    /// 通用文件日志记录器
    /// 文件存放位置：应用程序输出文件夹
    /// \bin\CommonFileLog\ErrorFileLogMessage\
    /// 日志文件记录格式：yyyyMMdd.html
    /// </summary>
    public class CommonErrorLogger
    {
        private ILog errorLogger;

        public CommonErrorLogger()
        {
            this.errorLogger = LogManager.GetLogger("commonErrorLogger");
        }
        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="operationName">操作名称</param>
        /// <param name="ex">错误信息</param>
        public void LogError(string operationName, Exception ex)
        {
            if (errorLogger.IsErrorEnabled)
            {
                errorLogger.Error(operationName, ex);
            }
        }
        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="ex">异常</param>
        public void LogError(Exception ex)
        {
            if (errorLogger.IsErrorEnabled)
            {
                errorLogger.Error(ex.ToString());
            }
        }
    }
}
