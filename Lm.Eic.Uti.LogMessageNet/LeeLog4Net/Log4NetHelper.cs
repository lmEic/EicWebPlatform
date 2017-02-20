namespace Lm.Eic.Uti.LogMessageNet.LeeLog4Net
{
    /// <summary>
    /// log4Net 组件操作助手
    /// </summary>
    public static class Log4NetHelper
    {
        private static readonly CommonInfoLogger infoLogger = new CommonInfoLogger();

        /// <summary>
        /// 通用消息日志记录器
        /// 文件存放位置：应用程序输出文件夹
        /// \bin\CommonFileLog\HtmlFileLogMessage\
        /// 日志文件记录格式：yyyyMMdd.html
        /// </summary>
        public static CommonInfoLogger InfoLogger
        {
            get { return infoLogger; }
        }

        private static readonly CommonErrorLogger errorLogger = new CommonErrorLogger();

        /// <summary>
        /// 通用错误日志记录器
        /// 文件存放位置：应用程序输出文件夹
        /// \bin\CommonFileLog\HtmlFileLogMessage\
        /// 日志文件记录格式：yyyyMMdd.html
        /// </summary>
        public static CommonErrorLogger ErrorLogger
        {
            get { return errorLogger; }
        }
    }
}