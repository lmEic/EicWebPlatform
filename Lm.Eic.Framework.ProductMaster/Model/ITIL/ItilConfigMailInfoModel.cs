using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Model.ITIL
{
    /// <summary>
    ///Email配置模块
    /// </summary>
    [Serializable]
    public partial class ConfigMailInfoModel
    {
        public ConfigMailInfoModel()
        { }
        #region Model
        private string _workerid;
        /// <summary>
        ///工号
        /// </summary>
        public string WorkerId
        {
            set { _workerid = value; }
            get { return _workerid; }
        }
        private string _department;
        /// <summary>
        ///部门
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        private string _pop3host;
        /// <summary>
        ///发Pop3协议
        /// </summary>
        public string Pop3Host
        {
            set { _pop3host = value; }
            get { return _pop3host; }
        }
        private int _pop3post;
        /// <summary>
        ///收Pop3协议
        /// </summary>
        public int Pop3Post
        {
            set { _pop3post = value; }
            get { return _pop3post; }
        }
        private DateTime _regdate;
        /// <summary>
        ///注册日期
        /// </summary>
        public DateTime RegDate
        {
            set { _regdate = value; }
            get { return _regdate; }
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
}
