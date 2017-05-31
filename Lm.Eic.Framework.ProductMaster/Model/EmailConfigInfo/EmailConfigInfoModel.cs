using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Model.EmailConfigInfo
{
    /// <summary>
    ///邮件配置信息
    /// </summary>
    [Serializable]
    public partial class Config_MailInfoModel
    {
        public Config_MailInfoModel()
        { }
        #region Model
        private string _wokerid;
        /// <summary>
        ///工号
        /// </summary>
        public string WokerId
        {
            set { _wokerid = value; }
            get { return _wokerid; }
        }
        private string _name;
        /// <summary>
        ///姓名
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        private string _email;
        /// <summary>
        ///Email地址
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        private string _nickname;
        /// <summary>
        ///昵称
        /// </summary>
        public string NickName
        {
            set { _nickname = value; }
            get { return _nickname; }
        }
        private int _receivegrade;
        /// <summary>
        ///接受级别
        /// </summary>
        public int ReceiveGrade
        {
            set { _receivegrade = value; }
            get { return _receivegrade; }
        }
        private int _issender;
        /// <summary>
        ///是否为发送者
        /// </summary>
        public int IsSender
        {
            set { _issender = value; }
            get { return _issender; }
        }
        private string _password;
        /// <summary>
        ///发送者密码
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
        private int _isvalidity;
        /// <summary>
        ///是否有效
        /// </summary>
        public int IsValidity
        {
            set { _isvalidity = value; }
            get { return _isvalidity; }
        }
        private string _smtphost;
        /// <summary>
        ///发送者服务器
        /// </summary>
        public string SmtpHost
        {
            set { _smtphost = value; }
            get { return _smtphost; }
        }
        private int _smtppost;
        /// <summary>
        ///发送者端口
        /// </summary>
        public int SmtpPost
        {
            set { _smtppost = value; }
            get { return _smtppost; }
        }
        private string _opperson;
        /// <summary>
        ///操作人
        /// </summary>
        public string OpPerson
        {
            set { _opperson = value; }
            get { return _opperson; }
        }
        private string _opsign;
        /// <summary>
        ///操作标识
        /// </summary>
        public string OpSign
        {
            set { _opsign = value; }
            get { return _opsign; }
        }
        private DateTime _opdate;
        /// <summary>
        ///操作日期
        /// </summary>
        public DateTime OpDate
        {
            set { _opdate = value; }
            get { return _opdate; }
        }
        private DateTime _optime;
        /// <summary>
        ///操作时间
        /// </summary>
        public DateTime OpTime
        {
            set { _optime = value; }
            get { return _optime; }
        }
        private decimal _id_key;
        /// <summary>
        ///自增键
        /// </summary>
        public decimal Id_Key
        {
            set { _id_key = value; }
            get { return _id_key; }
        }
        #endregion Model
    }

    /// <summary>
    /// 发送者基本数据
    /// </summary>
    public class SendersMailModel
    {
        /// <summary>
        /// 发邮人地址
        /// </summary>
        public string SenderMailAddess
        {
            set; get;
        }
        /// <summary>
        /// 发件人名称
        /// </summary>
        public string SenderName
        {
            set; get;
        }
        /// <summary>
        /// 发件密码
        /// </summary>
        public string SenderMailPwd
        {
            set; get;
        }
        /// <summary>
        /// Smtp 服务器
        /// </summary>
        public string SmtpHost
        {
            set; get;
        }

        private int _smtpPort = 25;
        /// <summary>
        ///Smtp 服务器端口 默认为25
        /// </summary>
        public int SmtpPort
        {
            set { _smtpPort = value; }
            get { return _smtpPort; }
        }
        public MailPriority _mailPriority = MailPriority.Normal;
        /// <summary>
        /// 指定邮件的优先级
        /// </summary>
        public MailPriority MailPriority
        {
            set { _mailPriority = value; }
            get { return _mailPriority; }
        }
    }
    /// <summary>
    /// 收件人
    /// </summary>
    public class RecipientsMailModel
    {
        /// <summary>
        /// 收件人
        /// </summary>
        public List<string> RecipientMailAddress { get; set; }
        /// <summary>
        /// 抄送人  
        /// </summary>
        public List<string> CcRecipientMailAddress { get; set; }
        /// <summary>
        /// 密送人 
        /// </summary>
        public List<string> BccRecipientMailAddress { set; get; }
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
        { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public List<string> AttachmentsPath { get; set; }
    }
    /// <summary>
    ///打卡信息
    /// </summary>
    public class PunchcardinfoDto
    {
        /// <summary>
        ///部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string WorkerId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string WorkerName { get; set; }
        /// <summary>
        /// 打卡时间1
        /// </summary>
        public string CardTime1 { get; set; }
        /// <summary>
        /// 打卡时间2
        /// </summary>
        public string CardTime2 { get; set; }
    }
}
