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
            return new EmailSenHelper(sendMailModel) { RecipientsMail = recipient }.SendMail();
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
        { set; get; }
        public RecipientsMailModel RecipientsMail
        {
            set; get;
        }
        private  SmtpClient smtpClient
        { get { return new SmtpClient(); } }
        private MailMessage mailMessage
        { get { return new MailMessage(); } }

        public OpResult SendMail()
        {
            if (SendersMail == null) return OpResult.SetErrorResult("发件人不能为空");
            if (SendersMail.SenderMailAddess == null || SendersMail.SenderMailAddess == string.Empty)
                return OpResult.SetErrorResult("发件人地址不能为空");
            //使用指定的邮件地址初始化MailAddress实例
            MailAddress initialSenderMailaddr = new MailAddress(SendersMail.SenderMailAddess);
            if(RecipientsMail==null )return OpResult.SetErrorResult("收件人信息为空，不能发送");
            //向收件人地址集合添加邮件地址
            if (RecipientsMail.RecipientMailAddress == null || RecipientsMail.RecipientMailAddress.Count == 0)
                return OpResult.SetErrorResult("收件人地址不能为空"); 

            for (int i = 0; i < RecipientsMail.RecipientMailAddress.Count; i++)
            {
                mailMessage.To.Add(RecipientsMail.RecipientMailAddress[i].ToString());
            }

            //向抄送收件人地址集合添加邮件地址
            if (RecipientsMail.CopyRecipientMailAddress != null && RecipientsMail.CopyRecipientMailAddress.Count >0)
                for (int i = 0; i < RecipientsMail.CopyRecipientMailAddress.Count; i++)
                {
                    mailMessage.CC.Add(RecipientsMail.CopyRecipientMailAddress[i].ToString());
                }
            
            //发件人地址
            mailMessage.From = initialSenderMailaddr;
            //电子邮件的标题
            mailMessage.Subject = RecipientsMail.MailTitle;
            //电子邮件的主题内容使用的编码
            mailMessage.SubjectEncoding = Encoding.UTF8;
            //电子邮件正文
            mailMessage.Body = RecipientsMail.MailBody;
            mailMessage.Priority = MailPriority.Normal;
            //电子邮件正文的编码
            mailMessage.BodyEncoding = Encoding.Default;
            //电子邮件优先级
            mailMessage.Priority = SendersMail.MailPriority;
            //以Html格式发送 
            mailMessage.IsBodyHtml = RecipientsMail.IsBodyHtml;
            //在有附件的情况下添加附件
                if (RecipientsMail.AttachmentsPath != null && RecipientsMail.AttachmentsPath.Count > 0)
                {
                    Attachment attachFile = null;
                    foreach (string path in RecipientsMail.AttachmentsPath)
                    {
                        attachFile = new Attachment(path);
                        mailMessage.Attachments.Add(attachFile);
                    }
                }
            SmtpClient smtp = new SmtpClient();
            //指定发件人的邮件地址和密码以验证发件人身份
            smtp.Credentials = new System.Net.NetworkCredential(SendersMail.SenderMailAddess, SendersMail.SenderMailPwd);
            //设置SMTP邮件服务器
            smtp.Host = SendersMail.SenderSeverHost;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                //将邮件发送到SMTP邮件服务器
                smtp.Send(mailMessage);
                return OpResult.SetSuccessResult("邮件发送成功");
            }
            catch (SmtpException ex)
            {
                return OpResult.SetErrorResult("邮件发送失败");
                throw new Exception(ex.Message);
            }

        }

    }
}
