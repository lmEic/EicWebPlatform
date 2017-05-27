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
        public EmailMessageHelper(string mailFrom, string mailPwd, bool isbodyHtml,string severHost=null )
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
        public string  Host
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

}
