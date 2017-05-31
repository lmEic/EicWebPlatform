using Lm.Eic.Framework.ProductMaster.Model.EmailConfigInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Business.MailSend
{
    public class MailManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SendersMailModel GetMailSenderInfo()
        {
            var mailInfo = MailCrudFactory.EmialConfigCrud.GetMailSenderInfoDatas().FirstOrDefault();
            if (mailInfo == null) return null;
            return new SendersMailModel()
            {
                SenderMailAddess = mailInfo.Email,
                SenderMailPwd = mailInfo.Password,
                SmtpHost = mailInfo.SmtpHost,
                SmtpPort = mailInfo.SmtpPost,
                SenderName = mailInfo.NickName
            };
        }
        /// <summary>
        /// 0：总经理级别 1：经理 2：副经理
        /// 3：课级  4：普通 5：没有接受权限
        /// </summary>
        /// <param name="receiveGrade">接受邮件级别</param>
        /// <returns></returns>
        public List<string> GetReceiveAdress(int receiveGrade)
        {
            List<string> receiveAdress = new List<string>();
            var receiveMailInfo = MailCrudFactory.EmialConfigCrud.GetResiveMailInfoDatas();
            if (receiveMailInfo == null) return receiveAdress;
            receiveMailInfo.ForEach(e =>
            {
                if (e.IsValidity == 1 && e.ReceiveGrade == receiveGrade)
                {
                    if (!receiveAdress.Contains(e.Email))
                        receiveAdress.Add(e.Email);
                }
            });
            return receiveAdress;
        }
        public RecipientsMailModel getRecipientsMailMessges(string mailBody, string mailTitle)
        {
            var recipientMailAddress = GetReceiveAdress(2);
            var CcRecipientMailAddress = GetReceiveAdress(3);
            return new RecipientsMailModel()
            {
                IsBodyHtml = true,
                RecipientMailAddress = recipientMailAddress,
                CcRecipientMailAddress = recipientMailAddress,
                MailBody = mailBody,
                MailTitle = mailTitle,
            };
        }
        public string RowDatas(PunchcardinfoDto model)
        {
            string stringRowData = "<TR>";
            if (model == null) return stringRowData + "</TR>";
            List<string> propertyList = new List<string>();
            PunchcardinfoDto mc = new PunchcardinfoDto();
            Type t = mc.GetType();
            string rowbody = string.Empty;
            foreach (PropertyInfo pi in t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                rowbody += string.Format("<TD>{0}</TD>", pi.Name.ToString());
            }
            stringRowData = rowbody + "</TR>";
            return stringRowData;
        }
        public string SendMailFomartDoing(List<PunchcardinfoDto> PunchcardDatas)
        {
            string sendMailBody = string.Empty;
            string sendMailFomartdatas = "<TABLE border=1><CAPTION>2017-05-28日异常考勤数据汇总</CAPTION><THEAD>" +
                                          "<TR>" +
                                          "<TH> 部门 </TH>" +
                                          "<TH> 工号 </TH>" +
                                          "<TH> 姓名 </TH>" +
                                          "<TH> 时间1 </TH>" +
                                          "<TH> 时间2 </TH>" +
                                          "</TR> ";
            if (PunchcardDatas == null || PunchcardDatas.Count == 0)
                return sendMailFomartdatas + "</TABLE>";
            sendMailFomartdatas += "<TBODY>";
            string RowDatasList = string.Empty;
            PunchcardDatas.ForEach(e =>
            {
                RowDatasList += RowDatas(e);
            });
            sendMailBody = sendMailFomartdatas + RowDatasList + "</TBODY></TABLE>";
            return sendMailBody;
        }
    }
 
}
