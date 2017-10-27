using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using Lm.Eic.Uti.Common.YleeMessage.Log;

namespace Lm.Eic.Uti.Common.YleeMessage.Email
{
    /// <summary>
    /// 邮件助手
    /// </summary>
    public class MailHelper
    {
        #region constructure
        private MailHelper()
        { }
        /// <summary>
        /// 邮箱助手构造器，必须配置SMTP服务器
        /// </summary>
        /// <param name="config"></param>
        public MailHelper(SmtpConfig config)
        {
            SmtpClient = new SmtpClient();
            SmtpClient.Host = config.Host;
            SmtpClient.Port = config.Port;
            SmtpClient.EnableSsl = config.EnableSsl;
            SmtpClient.UseDefaultCredentials = false;
            SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpClient.Credentials = new NetworkCredential(config.UserName, config.Password);
        }
        #endregion
        public SmtpClient SmtpClient { get; private set; }
        #region method
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailMsg"></param>
        public void SendMail(MailMsg mailMsg)
        {
            MailMessage msg = CreateMailMessage(mailMsg);
            if (msg == null) return;
            try
            {
                this.SmtpClient.Send(msg);
            }
            catch (SmtpException ex)
            {
                MessageLogger.LogErrorMsgToFile("SendMail", ex);
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailMsg"></param>
        public void SendMailAsync(MailMsg mailMsg)
        {
            MailMessage msg = CreateMailMessage(mailMsg);
            if (msg == null) return;
            try
            {
                //this.SmtpClient.SendAsync(;
            }
            catch (SmtpException ex)
            {
                MessageLogger.LogErrorMsgToFile("SendMail", ex);
            }
        }

        private void ValidateAddress(MailAddress addr, MailAddressCollection addrCollection)
        {
            if (!MailValiator.IsEmail(addr.Address))
            {
                MessageLogger.LogMsgToFile("CreateMailMessage", string.Format("{0}邮箱输入错误", addr.Address));
            }
            else
            {
                addrCollection.Add(addr);
            }
        }

        private MailMessage CreateMailMessage(MailMsg mailMsg)
        {
            MailMessage msg = null;
            if (!MailValiator.IsEmail(mailMsg.AddressFrom.Address))
            {
                MessageLogger.LogMsgToFile("CreateMailMessage", string.Format("{0}邮箱输入错误", mailMsg.AddressFrom.Address));
                return msg;
            }
            msg = new MailMessage();
            MailAddress ma = null;
            msg.From = mailMsg.AddressFrom;
            mailMsg.AddressTo.ToList().ForEach(to =>
            {
                ValidateAddress(to, msg.To);
            });
            if (mailMsg.BccAddress != null)
                mailMsg.BccAddress.ForEach(bcc =>
                {
                    ma = new MailAddress(bcc);
                    ValidateAddress(ma, msg.Bcc);
                });
            if (mailMsg.CC != null)
                mailMsg.CC.ForEach(c =>
                {
                    ma = new MailAddress(c);
                    ValidateAddress(ma, msg.CC);
                });

            if (mailMsg.Priority == 0)
                msg.Priority = MailPriority.Normal;
            else if (mailMsg.Priority == 1)
                msg.Priority = MailPriority.High;
            else
                msg.Priority = MailPriority.Low;

            msg.Subject = mailMsg.Subject;
            msg.Body = mailMsg.Body;
            msg.IsBodyHtml = mailMsg.IsBodyHtml;

            msg.BodyEncoding = mailMsg.BodyEncoding;
            msg.SubjectEncoding = mailMsg.SubjectEncoding;
            return msg;
        }
        #endregion
    }
    /// <summary>
    /// 邮件发送体
    /// </summary>
    public class MailMsg
    {
        /// <summary>
        /// 发送邮件目标地址
        /// </summary>
        public MailAddressCollection AddressTo { get; private set; }
        /// <summary>
        /// 发件人邮件地址
        /// </summary>
        public MailAddress AddressFrom { get; private set; }
        /// <summary>
        /// 设置此电子邮件的主题。
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 邮件主题编码格式
        /// </summary>
        public Encoding SubjectEncoding { get; set; }
        /// <summary>
        /// 设置邮件正文。
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 邮件内容编码格式
        /// </summary>
        public Encoding BodyEncoding { get; set; }
        /// <summary>
        /// 设置邮件正文是否为 Html 格式的值。
        /// </summary>
        public bool IsBodyHtml { get; set; }
        /// <summary>
        /// 密件抄送收件人列表
        /// </summary>
        public List<string> BccAddress { get; set; }
        /// <summary>
        /// 抄送收件人列表
        /// </summary>
        public List<string> CC { get; set; }
        /// <summary>
        /// 邮件优先级
        /// 0：正常，1：高，-1：低
        /// </summary>
        public int Priority { get; set; }
        private MailMsg()
        { }
        /// <summary>
        /// 构造器，设定发件人地址，和目标地址
        /// </summary>
        /// <param name="addressFrom"></param>
        /// <param name="addressTo"></param>
        public MailMsg(string addressFrom, string addressTo)
        {
            this.AddressFrom = new MailAddress(addressFrom);
            this.AddressTo = new MailAddressCollection();
            this.AddressTo.Add(new MailAddress(addressTo));
            this.InitDefault();
        }
        public MailMsg(string addressFrom, List<string> addressTo)
        {
            this.AddressFrom = new MailAddress(addressFrom);
            this.AddressTo = new MailAddressCollection();
            if (AddressTo != null)
            {
                MailAddress ma = null;
                addressTo.ForEach(a =>
                {
                    ma = new MailAddress(a);
                    this.AddressTo.Add(ma);
                });
            }
            this.InitDefault();
        }

        public MailMsg(MailAddress addressFrom, MailAddressCollection addressToList)
        {
            this.AddressFrom = addressFrom;
            this.AddressTo = addressToList;
            this.InitDefault();
        }

        private void InitDefault()
        {
            this.IsBodyHtml = true;
            this.Subject = "MAIL";
            this.Body = "MAIL";
            this.Priority = 0;
            this.BodyEncoding = System.Text.Encoding.Default;
            this.SubjectEncoding = System.Text.Encoding.UTF8;
        }
    }
    /// <summary>
    /// Smtp参数配置
    /// </summary>
    public class SmtpConfig
    {
        public string Host { get; private set; }
        public int Port { get; private set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool EnableSsl { get; set; }
        private SmtpConfig()
        { }

        public SmtpConfig(string host, int port, string userName, string password)
        {
            Host = host;
            Port = port;
            UserName = userName;
            Password = password;
            EnableSsl = false;
        }

    }

    internal class MailValiator
    {
        /// <summary>
        /// 数据验证类使用的正则表述式选项
        /// </summary>
        private const RegexOptions Options = RegexOptions.IgnoreCase | RegexOptions.Compiled;
        /// <summary>
        /// 检测字符串是否为有效的邮件地址捕获正则
        /// </summary>
        private static readonly Regex EmailRegex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", Options);
        /// <summary>
        /// 检测字符串是否为有效的邮件地址
        /// </summary>
        /// <param name="input">需要检查的字符串</param>
        /// <returns>如果字符串为有效的邮件地址，则为 true；否则为 false。</returns>
        internal static bool IsEmail(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            else
            {
                bool result = EmailRegex.IsMatch(input);
                if (!result)
                    MessageLogger.LogMsgToFile("IsEmail", $"{input}为非法邮箱名");
                return result;
            }
        }
    }
}
