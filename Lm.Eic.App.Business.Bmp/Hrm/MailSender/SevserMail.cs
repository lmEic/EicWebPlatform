using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.DomainModel.Bpm.MailSendersModel;
using System.Net.Mail;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeObjectBuilder;

namespace Lm.Eic.App.Business.Bmp.Hrm.MailSender
{
    public static  class  MailSend
    {
        public static EmailSendManager EmailSend
        {
            get { return OBulider.BuildInstance<EmailSendManager>(); }
        }
    }
    public class EmailSendManager
    {
        public SendersMailModel sendMailModel
        { set;get; }
        public RecipientsMailModel recipient
        { set; get; }
        public OpResult sendMail()
        {
            string msesgestring = string.Empty;

           var isSuccessResult = new EmailSendHelper(sendMailModel) { RecipientsMail = recipient }.SendMail(out  msesgestring);
            return OpResult.SetResult(msesgestring ,isSuccessResult);
        }
    }
    public class EmailSendHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailFrom">发送人地址</param>
        /// <param name="mailPwd">发送人登录密码</param>
        /// <param name="isbodyHtml">发送人正文格式</param>
        /// <param name="severHost">发送人服务器</param>
        public EmailSendHelper(SendersMailModel model)
        {
            this.SendersMail = model;
        }
        public SendersMailModel SendersMail
        { set; get; }
        public RecipientsMailModel RecipientsMail
        {
            set; get;
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mssges"></param>
        /// <returns></returns>
        public bool  SendMail(out string mssges)
        {
            if (SendersMail == null)
            {
                mssges = "发件人基本信息不能为空";
                return false;
            }
            if (SendersMail.SenderMailAddess == null || SendersMail.SenderMailAddess == string.Empty)
            {
                mssges = "发件人地址不能为空";
                return false;
            }
           
            //向收件人地址集合添加邮件地址
            SmtpClient smtp = SetSmtpClient(
                SendersMail.SenderMailAddess,
                SendersMail.SenderMailPwd,
                SendersMail.SmtpHost, 
                SendersMail.SmtpPort);
            if (RecipientsMail == null)
            {
                mssges = "收件人基本信息信息为空，不能发送";
                return false ;
            }
            if (RecipientsMail.RecipientMailAddress == null || RecipientsMail.RecipientMailAddress.Count == 0)
            {
                mssges = "收件人地址不能为空";
                return false;
            }
            MailMessage mailMessage = SetmailMessage(
                SendersMail.SenderMailAddess,
                SendersMail.SenderName,
                RecipientsMail.RecipientMailAddress,
                RecipientsMail.IsBodyHtml,
                RecipientsMail.MailTitle,
                RecipientsMail.CcRecipientMailAddress,
                RecipientsMail.BccRecipientMailAddress,
                RecipientsMail.AttachmentsPath,
                RecipientsMail.MailBody
                );
            try
            {
                //将邮件发送到SMTP邮件服务器
                smtp.Send(mailMessage);
                mssges = "邮件发送成功";
                return true;
            }
            catch (SmtpException ex)
            {
                mssges = "邮件发送失败";
                return false ;
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// 收正文必要信息
        /// </summary>
        /// <param name="senderMailAddess">发件人地址</param>
        /// <param name="senderName">发件人名</param>
        /// <param name="recipientMailAddress">收件人地址</param>
        /// <param name="isBodyHtml">是否以Html格式</param>
        /// <param name="mailTitle">标题</param>
        /// <param name="ccRecipientMailAddress">抄送地址</param>
        /// <param name="rCcRecipientMailAddress">密送地址</param>
        /// <param name="attachmentsPath">附件</param>
        /// <param name="mailBody">正文</param>
        /// <returns></returns>
        private MailMessage SetmailMessage(string senderMailAddess,string senderName,
            List<string> recipientMailAddress,
            bool isBodyHtml= true ,
            string mailTitle="无",
            List<string> ccRecipientMailAddress=null ,
            List<string> rCcRecipientMailAddress=null , 
            List<string> attachmentsPath=null ,
           string mailBody=null  )
        {
            MailMessage mailMessage = new MailMessage();
            //电子邮件优先级
            mailMessage.Priority = MailPriority.Normal;
            //使用指定的邮件地址初始化MailAddress实例
            MailAddress initialSenderMailaddr = new MailAddress(senderMailAddess, senderName);
            //发件人地址
            mailMessage.From = initialSenderMailaddr;
            //收件人
            for (int i = 0; i < recipientMailAddress.Count; i++)
            {
                if (!string.IsNullOrEmpty(recipientMailAddress[i].ToString().Trim()))
                    mailMessage.To.Add(recipientMailAddress[i].ToString());
            }
            //向抄送收件人地址集合添加邮件地址
            if (ccRecipientMailAddress != null && ccRecipientMailAddress.Count > 0)
                for (int i = 0; i < ccRecipientMailAddress.Count; i++)
                {
                    if (!string.IsNullOrEmpty(ccRecipientMailAddress[i].ToString().Trim()))
                        mailMessage.CC.Add(ccRecipientMailAddress[i].ToString().Trim());
                }
            //密送人  
            if (rCcRecipientMailAddress != null && rCcRecipientMailAddress.Count > 0)
                for (int i = 0; i < rCcRecipientMailAddress.Count; i++)
                {
                    if (!string.IsNullOrEmpty(rCcRecipientMailAddress[i].ToString().Trim()))
                        mailMessage.CC.Add(rCcRecipientMailAddress[i].ToString().Trim());
                }
            //电子邮件的标题
            mailMessage.Subject = mailTitle;
            //电子邮件的主题内容使用的编码
            mailMessage.SubjectEncoding = Encoding.GetEncoding(936);
            //以Html格式发送 
            mailMessage.IsBodyHtml = isBodyHtml;
            //电子邮件正文
            mailMessage.Body = mailBody;
            //电子邮件正文的编码
            mailMessage.BodyEncoding = Encoding.GetEncoding(936);
            //在有附件的情况下添加附件
            if (attachmentsPath != null && attachmentsPath.Count > 0)
            {
                Attachment attachFile = null;
                foreach (string path in attachmentsPath)
                {
                    attachFile = new Attachment(path);
                    mailMessage.Attachments.Add(attachFile);
                }
            }

            return mailMessage;
        }
        /// <summary>
        /// 发件必要信息
        /// </summary>
        /// <param name="senderMailAddess">发件人地址</param>
        /// <param name="senderMailPwd">邮件密码</param>
        /// <param name="smtpHost">发送SMTP服务器</param>
        /// <param name="smtpPort">SMTP服务器端口</param>
        /// <returns></returns>
        private SmtpClient SetSmtpClient(string senderMailAddess, string senderMailPwd,string smtpHost,int smtpPort=25)
        {
            SmtpClient smtp = new SmtpClient();
            //设置SMTP邮件服务器
            smtp.Host = smtpHost;
            smtp.Port = smtpPort;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = true;
            //指定发件人的邮件地址和密码以验证发件人身份
            smtp.Credentials = new System.Net.NetworkCredential(senderMailAddess, senderMailPwd);
            return smtp;
        }
    }
}
