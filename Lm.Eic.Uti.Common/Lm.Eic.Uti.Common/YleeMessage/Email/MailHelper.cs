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


    }



}
