using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Lm.Eic.Uti.Common.YleeMessage.Email
{
    /// <summary>
    /// Email发送
    /// </summary>
    public class EmailMessageHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailFrom">发送人地址</param>
        /// <param name="mailPwd">发送人登录密码</param>
        /// <param name="isbodyHtml">发送人正文格式</param>
        /// <param name="severHost">发送人服务器</param>
        public EmailMessageHelper(string mailFrom, string mailPwd, bool isbodyHtml, string severHost = null)
        {
            if (severHost != null)
            { Host = severHost; }
            //默认为普通优先级
            MailPriority = MailPriority.Normal;
            this.MailFrom = mailFrom;
            this.MailPwd = mailPwd;
            this.IsbodyHtml = isbodyHtml;
        }
        /// <summary>
        /// 发送者
        /// </summary>
        public string MailFrom { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>
        public string[] MailToArray { get; set; }
        /// <summary>
        /// 抄送
        /// </summary>
        public string[] MailCcArray { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string MailSubject { get; set; }
        /// <summary>
        /// 正文
        /// </summary>
        public string MailBody { get; set; }
        /// <summary>
        /// 发件人密码
        /// </summary>
        public string MailPwd { get; set; }
        /// <summary>
        /// SMTP邮件服务器
        /// </summary>
        public string _host = "smtp.exmail.qq.com";
        public string Host
        {
            set { _host = value; }
            get { return _host; }
        }
        /// <summary>
        /// 正文是否是html格式
        /// </summary>
        public bool IsbodyHtml { get; set; }
        /// <summary>
        /// 指定邮件的优先级
        /// </summary>
        public MailPriority MailPriority { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public string[] AttachmentsPath { get; set; }
        public bool Send()
        {
            //使用指定的邮件地址初始化MailAddress实例
            MailAddress maddr = new MailAddress(MailFrom);
            //初始化MailMessage实例
            MailMessage myMail = new MailMessage();
            //向收件人地址集合添加邮件地址
            if (MailToArray != null)
            {
                for (int i = 0; i < MailToArray.Length; i++)
                {
                    myMail.To.Add(MailToArray[i].ToString());
                }
            }

            //向抄送收件人地址集合添加邮件地址
            if (MailCcArray != null)
            {
                for (int i = 0; i < MailCcArray.Length; i++)
                {
                    myMail.CC.Add(MailCcArray[i].ToString());
                }
            }
            //发件人地址
            myMail.From = maddr;

            //电子邮件的标题
            myMail.Subject = MailSubject;

            //电子邮件的主题内容使用的编码
            myMail.SubjectEncoding = Encoding.UTF8;

            //电子邮件正文
            myMail.Body = MailBody;

            myMail.Priority = MailPriority.Normal;

            //电子邮件正文的编码
            myMail.BodyEncoding = Encoding.Default;

            //电子邮件优先级
            myMail.Priority = MailPriority;
            //以Html格式发送 
            myMail.IsBodyHtml = IsbodyHtml;


            //在有附件的情况下添加附件
            try
            {
                if (AttachmentsPath != null && AttachmentsPath.Length > 0)
                {
                    Attachment attachFile = null;
                    foreach (string path in AttachmentsPath)
                    {
                        attachFile = new Attachment(path);
                        myMail.Attachments.Add(attachFile);
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception("在添加附件时有错误:" + err);
            }

            SmtpClient smtp = new SmtpClient();
            //指定发件人的邮件地址和密码以验证发件人身份
            smtp.Credentials = new System.Net.NetworkCredential(MailFrom, MailPwd);
            //设置SMTP邮件服务器
            smtp.Host = Host;
            try
            {
                //将邮件发送到SMTP邮件服务器
                smtp.Send(myMail);
                return true;

            }
            catch (SmtpException ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }

    public class EmailSenHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailFrom">发送人地址</param>
        /// <param name="mailPwd">发送人登录密码</param>
        /// <param name="isbodyHtml">发送人正文格式</param>
        /// <param name="severHost">发送人服务器</param>
        public EmailSenHelper(SendersMailModel model)
        {
            this.SendersMail = model;
        }
        public SendersMailModel SendersMail
        { set;get; }
       public  SendMailConnectModel SendMailConnect
        {
            set;get;
        }
       public bool Send()
        {
            if (SendersMail == null) return false;
            if (SendersMail.SendersmailAddess == null || SendersMail.SendersmailAddess == string.Empty)
                return false;
            //使用指定的邮件地址初始化MailAddress实例
            MailAddress maddr = new MailAddress(SendersMail.SendersmailAddess);
            //初始化MailMessage实例
            MailMessage myMail = new MailMessage();

            //向收件人地址集合添加邮件地址
            if (SendMailConnect.RecipientMailAddress == null|| SendMailConnect.RecipientMailAddress.Count ==0) return false;
           
                for (int i = 0; i < SendMailConnect.RecipientMailAddress.Count; i++)
                {
                    myMail.To.Add(SendMailConnect.RecipientMailAddress[i].ToString());
                }

            //向抄送收件人地址集合添加邮件地址
            if (SendMailConnect.CopyRecipientMailAddress ==null || SendMailConnect.CopyRecipientMailAddress.Count == 0) return false;
            {
                for (int i = 0; i < SendMailConnect.CopyRecipientMailAddress.Count ; i++)
                {
                    myMail.CC.Add(SendMailConnect.CopyRecipientMailAddress[i].ToString());
                }
            }
            //发件人地址
            myMail.From = maddr;
            //电子邮件的标题
            myMail.Subject = SendMailConnect.MailTitle;
            //电子邮件的主题内容使用的编码
            myMail.SubjectEncoding = Encoding.UTF8;
            //电子邮件正文
            myMail.Body = SendMailConnect.MailBody;
            myMail.Priority = MailPriority.Normal;
            //电子邮件正文的编码
            myMail.BodyEncoding = Encoding.Default;
            //电子邮件优先级
            myMail.Priority = SendersMail.MailPriority;
            //以Html格式发送 
            myMail.IsBodyHtml = SendersMail.isbodyHtml ;
            //在有附件的情况下添加附件
            try
            {
                if (SendMailConnect.AttachmentsPath  != null && SendMailConnect.AttachmentsPath.Count > 0)
                {
                    Attachment attachFile = null;
                    foreach (string path in SendMailConnect.AttachmentsPath)
                    {
                        attachFile = new Attachment(path);
                        myMail.Attachments.Add(attachFile);
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception("在添加附件时有错误:" + err);
            }

            SmtpClient smtp = new SmtpClient();
            //指定发件人的邮件地址和密码以验证发件人身份
            smtp.Credentials = new System.Net.NetworkCredential(SendersMail.SendersmailAddess, SendersMail.SendersMailPwd);
            //设置SMTP邮件服务器
            smtp.Host = SendersMail.SendersSeverHost;
            try
            {
                //将邮件发送到SMTP邮件服务器
                smtp.Send(myMail);
                return true;

            }
            catch (SmtpException ex)
            {
                throw new Exception(ex.Message);
            }

        }
    
    }
    /// <summary>
    /// 发件人邮箱
    /// </summary>
    public class SendersMailModel
    {
        /// <summary>
        /// 发邮人地址
        /// </summary>
      public  string SendersmailAddess
        {
            set;get;
        }
        /// <summary>
        /// 发件密码
        /// </summary>
        public string SendersMailPwd
        {
            set;get;
        }
        /// <summary>
        /// 是否以HTml格式
        /// </summary>
      public  bool isbodyHtml
        { set; get; }
        /// <summary>
        /// 发件人服务器
        /// </summary>
      public   string SendersSeverHost
        {
            set;get;
        }
        /// <summary>
        /// 指定邮件的优先级
        /// </summary>
        public MailPriority MailPriority { get; set; }
    }

    public class SendMailConnectModel
    {
        /// <summary>
        /// 收件人
        /// </summary>
        public List<string> RecipientMailAddress { get; set; }
        /// <summary>
        /// 抄送
        /// </summary>
        public List<string> CopyRecipientMailAddress { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string MailTitle { get; set; }
        /// <summary>
        /// 正文
        /// </summary>
        public string MailBody { get; set; }
        /// <summary>
        /// 正文是否有安Html格式发送
        /// </summary>
        public bool IsBodyHtml
        { get;set; }
        /// <summary>
        /// 附件
        /// </summary>
        public List<string> AttachmentsPath { get; set; }
        /// <summary>
        /// 发送内容转化为Html 格式
        /// </summary>
        /// <param name="waittingSendDmm"></param>
        /// <returns></returns>
        public string  SendMailContentToHtml(List<string> waittingSendDmm)
        {
            string mailContext = "<h2>"+ MailTitle + "：</h2>";
            mailContext += "<style type=" + "text/css" + ">table.gridtable {"+	
            " font-family: verdana,arial,sans-serif;"+
            " font-size:13px;	color:#333333;"+
            "border-width: 1px;	"+
             " border-color: #666666;	"+
            " border-collapse: collapse;}"+
            "table.gridtable th {	border-width: 1px;	padding: 8px;"+
            "border-style: solid;	border-color: #666666;"+
            " background-color: #dedede;}"+
            "table.gridtable td {	border-width: 1px;	padding: 8px;	border-style: solid;"+
            " border-color: #666666;	background-color: #ffffff;}"+
            " </style><!-- Table goes in the document BODY --><table class=" + "gridtable" + ">"+
            "<tr></tr>";
            foreach (var tem in waittingSendDmm)
            {
                mailContext += "<tr>";
                mailContext += string.Format("<td>{0}</td>", tem);
                mailContext += "</tr>";
            }
            mailContext += "</table>";
            return mailContext;
        }
    }
}
