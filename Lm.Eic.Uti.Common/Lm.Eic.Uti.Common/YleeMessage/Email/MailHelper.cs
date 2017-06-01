using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;


namespace Lm.Eic.Uti.Common.YleeMessage.Email
{


    /// <summary>
    /// 邮件助手
    /// </summary>
    public class MailHelper
    {
        public SmtpClient SmtpClient { get; private set; }
        public SmtpClient SetSmtpClient(string host, int port, bool enableSsl, string userName, string password)
        {
            SmtpClient = new SmtpClient();
            SmtpClient.Host = host;
            SmtpClient.Port = port;
            SmtpClient.EnableSsl = enableSsl;
            SmtpClient.UseDefaultCredentials = false;
            SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpClient.Credentials = new NetworkCredential(userName, password);
            return SmtpClient;
        }

        public void SendMail(MailMsg mailMsg)
        {
            MailMessage msg = new MailMessage();
            msg.From = mailMsg.AddressFrom;

            mailMsg.AddressTo.ToList().ForEach(to =>
            {


            });
            this.SmtpClient.Send(msg);
        }
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
        /// 设置邮件正文。
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 设置邮件正文是否为 Html 格式的值。
        /// </summary>
        public bool IsBodyHtml { get; set; }

        private MailMsg()
        { }
        /// <summary>
        /// 构造器，设定发件人地址，和目标地址
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public MailMsg(MailAddress from, MailAddressCollection to)
        {
            this.AddressFrom = from;
            this.AddressTo = to;
        }
    }


}
