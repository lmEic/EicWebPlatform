using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Model.ITIL
{
    /// <summary>
    /// 邮箱管理模型
    /// </summary>
    [Serializable]
  public partial  class ItilEmailManageModel
    {
        public ItilEmailManageModel()
        { }

        private string _workerId;
        /// <summary>
        /// 工号
        /// </summary>
        public string WorkerId
        {
            set { _workerId = value; }
            get { return _workerId; }

        }
        private string _name;
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        private string _department;
        /// <summary>
        /// 部门
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        private string _email;
        /// <summary>
        /// 邮件帐号
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        private string _nickName;
        /// <summary>
        /// 用户别名
        /// </summary>
        public string NickName
        {
            set { _nickName = value; }
            get { return _nickName; }
        }
        private int _receiveGrade;
        /// <summary>
        /// 邮件接收等级
        /// </summary>
        public int ReceiveGrade
        {
            set { _receiveGrade = value; }
            get { return _receiveGrade; }

        }
        private int _isSender;
        /// <summary>
        /// 是否允许发送
        /// </summary>
        public int IsSender
        {
            set { _isSender = value; }
            get { return _isSender; }
        }
        private string _password;
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
        private DateTime _regDate;
        public DateTime RegDate
        {
            set { _regDate = value; }
            get { return _regDate; }
        }

        private int _isValidity;
        /// <summary>
        /// 邮箱是否可用
        /// </summary>
        public int IsValidity
        {
            set { _isValidity = value; }
            get { return _isValidity; }

        }
        private string _smtpHost;
        /// <summary>
        /// 邮箱发送服务器地址
        /// </summary>
        public string SmtpHost
        {
            set { _smtpHost = value; }
            get { return _smtpHost; }
        }
        private string _pop3Host;
        /// <summary>
        /// 邮箱接收服务器地址
        /// </summary>
        public string Pop3Host
        {
            set { _pop3Host = value; }
            get { return _pop3Host; }
        }
        private int _smtpPost;
        /// <summary>
        /// 邮箱发送端口
        /// </summary>
        public int SmtpPost
        {
            set { _smtpPost = value; }
            get { return _smtpPost; }
        }
        private int _pop3Post;
        /// <summary>
        /// 邮箱接收端口
        /// </summary>
        public int Pop3Post
        {
            set { _pop3Post = value; }
            get { return _pop3Post; }
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
        private string _operson;
        /// <summary>
        ///操作人
        /// </summary>
        public string OpPerson
        {
            set { _operson = value; }
            get { return _operson; }
        }
        private string _opsign;
        /// <summary>
        ///操作标志
        /// </summary>
        public string OpSign
        {
            set { _opsign = value; }
            get { return _opsign; }
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



    }

}
