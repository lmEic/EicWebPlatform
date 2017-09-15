using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeMessage.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Lm.Eic.Framework.ProductMaster.Business.Itil
{
    /// <summary>
    /// 邮箱管理器
    /// </summary>
    public class EmailManager
    {
        // List<ItilEmailManageModel> _ItilEmailManageModelList = new List<ItilEmailManageModel>();
        public List<ItilEmailManageModel> GetEmails(ItilEmailManageModelDto dto)
        {
            return ItilEmailFactory.ItilEmailManageCrud.FindBy(dto);

        }
        /// <summary>
        /// 存储邮箱列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreEmailManage(ItilEmailManageModel model)
        {
            try
            {
                return ItilEmailFactory.ItilEmailManageCrud.Store(model);
            }
            catch (Exception ex)
            {
                return ex.ExOpResult();
            }
        }
        private string OutputMessage(List<string> mailNameList)
        {
            StringBuilder sb = new StringBuilder();
            int index = 1;
            mailNameList.ForEach(name =>
            {
                sb.Append(name + ",");
                index += 1;
                if (index > 3)
                    return;
            });
            return sb.ToString().TrimEnd(',');
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailAddressList">邮件地址列表模型</param>
        /// <param name="mailContent">邮件内容模型</param>
        /// <returns></returns>
        public OpResult SendEmail(List<ItilEmailManageModel> mailAddressList, MailContentModel mailContent)
        {
            try
            {
                if (mailAddressList == null || mailAddressList.Count == 0 || mailContent == null)
                    return OpResult.SetErrorResult("邮件地址信息或者内容信息不能null！");
                MailCell cell = new MailCell() { MessageBody = mailContent.Body, Subject = mailContent.Body };
                cell.AddressToList = new MailAddressCollection();
                List<string> nameList = new List<string>();
                mailAddressList.ForEach(mailAddress =>
                {
                    cell.AddressToList.Add(new MailAddress(mailAddress.Email, mailAddress.Name));
                    nameList.Add(mailAddress.Name);
                });
                MessageLogger.NotifyMessageTo(cell);
                return OpResult.SetSuccessResult(string.Format("向{0}发送邮件成功！", OutputMessage(nameList)));
            }
            catch (System.Exception ex)
            {
                return ex.ExOpResult();
            }
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailAddress">邮件地址模型</param>
        /// <param name="mailContent">邮件内容模型</param>
        /// <returns></returns>
        public OpResult SendEmail(ItilEmailManageModel mailAddress, MailContentModel mailContent)
        {
            List<ItilEmailManageModel> mailAddressList = new List<ItilEmailManageModel>() { mailAddress };
            return SendEmail(mailAddressList, mailContent);
        }
        /// <summary>
        /// 向管理员发送邮件
        /// </summary>
        /// <param name="mailContent"></param>
        /// <returns></returns>
        public OpResult SendToAdmin(MailContentModel mailContent)
        {
            if (mailContent == null) return OpResult.SetErrorResult("邮件内容不能为null");
            MessageLogger.NotifyMessageToSoftAdmin(mailContent.Subject, mailContent.Body);
            return OpResult.SetSuccessResult("向管理员发送邮件成功！");
        }
    }

    /// <summary>
    /// 消息内容模型
    /// </summary>
    public class MailContentModel
    {
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Body { get; private set; }
        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Subject { get; private set; }

        public MailContentModel(string body, string subject)
        {
            this.Body = body;
            this.Subject = subject;
        }
    }
}
