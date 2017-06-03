using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using System.IO;
using Lm.Eic.Uti.Common.YleeMessage.Email;

namespace Lm.Eic.Uti.Common.YleeMessage.Log
{
    /// <summary>
    /// 消息日志记录器
    /// </summary>
    public static class MessageLogger
    {
        /// <summary>
        /// 邮件通知器
        /// </summary>
        private static MailHelper EmailNotifier
        {
            get
            {
                MailHelper mailHelper = new MailHelper(new SmtpConfig("smtp.exmail.qq.com", 25, "softwareadmin@ezconn.cn", "EIc2017"));
                return mailHelper;
            }
        }

        #region property
        /// <summary>
        /// 错误发生计数容器
        /// </summary>
        private static Dictionary<string, int> errorOccurCountDocker = new Dictionary<string, int>();
        /// <summary>
        /// 错误日志文件夹路径
        /// </summary>
        private static string errorLogFilePath = @"C:\EicSystem\WebPlatform\UtiCommon\";
        #endregion
        /// <summary>
        /// 将错误消息记录到文件中
        /// </summary>
        /// <param name="fnName">函数名称</param>
        /// <param name="ex"></param>
        public static void LogErrorMsgToFile(string fnName, Exception ex)
        {

            string fileName = Path.Combine(errorLogFilePath, DateTime.Now.ToString("yyyyMMdd") + ".txt");
            StringBuilder sbMsg = CreateExceptionMessage(ex, fnName);
            if (CheckErrorOccurTime(fnName)) return;

            fileName.AppendFile(sbMsg.ToString());
        }

        private static StringBuilder CreateExceptionMessage(Exception ex, string fnName = null)
        {
            StringBuilder sbMsg = new StringBuilder();
            if (!string.IsNullOrEmpty(fnName))
                sbMsg.AppendFormat("函数名称：{0}", fnName).AppendLine();
            sbMsg.AppendFormat("错误信息：{0}", ex.Message).AppendLine();
            sbMsg.AppendFormat("错误描述：{0}", ex.StackTrace).AppendLine();
            sbMsg.AppendFormat("错误源：{0}", ex.Source).AppendLine();
            sbMsg.AppendFormat("发生时间：{0}", DateTime.Now).AppendLine();
            return sbMsg;
        }

        /// <summary>
        /// 将错误消息记录到文件中
        /// </summary>
        /// <param name="fnName">函数名称</param>
        /// <param name="msg"></param>
        public static void LogMsgToFile(string fnName, string msg)
        {
            string fileName = Path.Combine(errorLogFilePath, DateTime.Now.ToString("yyyyMMdd") + ".txt");
            StringBuilder sbMsg = new StringBuilder();
            sbMsg.AppendFormat("函数名称：{0}", fnName).AppendLine();
            sbMsg.AppendFormat("信息：{0}", msg).AppendLine();

            if (CheckErrorOccurTime(fnName)) return;

            fileName.AppendFile(sbMsg.ToString());
        }

        /// <summary>
        /// 将信息通过邮件发送给相关人
        /// </summary>
        /// <param name="cell"></param>
        public static void NotifyMessageTo(MailCell cell)
        {
            StringBuilder sbMessage = new StringBuilder();
            List<string> receivers = cell.Recivers;
            string subject = cell.Subject;
            string messageBody = cell.MessageBody;
            if (CheckErrorOccurTime(subject)) return;
            try
            {
                sbMessage.Append(messageBody).AppendLine()
               .AppendLine("特别说明：此邮件为系统自动发送邮件，请勿回复！！!");
                MailMsg mailMsg = new MailMsg("softwareadmin@ezconn.cn", receivers);
                mailMsg.Subject = subject;
                mailMsg.Body = sbMessage.ToString();
                EmailNotifier.SendMail(mailMsg);
            }
            catch (System.Exception ex)
            {
                LogErrorMsgToFile("NotifyMessageTo", ex);
            }
        }
        /// <summary>
        /// 通知给软件开发管理员
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="messageBody"></param>
        public static void NotifyMessageToSoftAdmin(string subject, string messageBody)
        {
            if (CheckErrorOccurTime(subject)) return;
            MailCell mc = new MailCell()
            {
                MessageBody = messageBody,
                Recivers = new List<string>() { "ylei@ezconn.cn", "wxq520@ezconn.cn" },
                Subject = subject
            };
            NotifyMessageTo(mc);
        }
        /// <summary>
        /// 将异常信息发送给软件开发人员
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="ex"></param>
        public static void NotifyMessageToSoftAdmin(string subject, Exception ex)
        {
            if (CheckErrorOccurTime(subject)) return;
            MailCell mc = new MailCell()
            {
                MessageBody = CreateExceptionMessage(ex).ToString(),
                Recivers = new List<string>() { "ylei@ezconn.cn", "wxq520@ezconn.cn" },
                Subject = subject
            };
            NotifyMessageTo(mc);
        }

        private static bool CheckErrorOccurTime(string key)
        {
            if (errorOccurCountDocker.ContainsKey(key))
            {
                if (errorOccurCountDocker[key] >= 3)
                {
                    return true;
                }
                else
                {
                    errorOccurCountDocker[key]++;
                }
            }
            else
            {
                errorOccurCountDocker.Add(key, 1);
            }
            return false;
        }
    }

    public class MailCell
    {
        /// <summary>
        /// 邮件接收人列表
        /// </summary>
        public List<string> Recivers { get; set; }
        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string MessageBody { get; set; }
    }
}
