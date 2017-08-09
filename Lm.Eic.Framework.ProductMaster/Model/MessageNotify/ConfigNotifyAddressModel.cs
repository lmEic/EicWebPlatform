using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Model.MessageNotify
{
    /// <summary>
    ///配置消息通知地址模块
    /// </summary>
    [Serializable]
    public partial class ConfigNotifyAddressModel
    {
        public ConfigNotifyAddressModel()
        { }
        #region Model
        private string _modulename;
        /// <summary>
        ///模块名称
        /// </summary>
        public string ModuleName
        {
            set { _modulename = value; }
            get { return _modulename; }
        }
        private string _businessname;
        /// <summary>
        ///业务名称
        /// </summary>
        public string BusinessName
        {
            set { _businessname = value; }
            get { return _businessname; }
        }
        private string _transactionname;
        /// <summary>
        ///事务名称
        /// </summary>
        public string TransactionName
        {
            set { _transactionname = value; }
            get { return _transactionname; }
        }
        private string _emaillist;
        /// <summary>
        ///邮箱列表
        /// </summary>
        public string EmailList
        {
            set { _emaillist = value; }
            get { return _emaillist; }
        }
        private string _contactslist;
        /// <summary>
        ///联系人消息列表
        /// </summary>
        public string ContactsList
        {
            set { _contactslist = value; }
            get { return _contactslist; }
        }
        private string _weChatlist;
        /// <summary>
        ///微信号列表
        /// </summary>
        public string WeChatList
        {
            set { _weChatlist = value; }
            get { return _weChatlist; }
        }
        private int _notifymode;
        /// <summary>
        ///通知模式
        /// </summary>
        public int NotifyMode
        {
            set { _notifymode = value; }
            get { return _notifymode; }
        }
        private string _opstatus;
        /// <summary>
        ///操作状态
        /// </summary>
        public string OpStatus
        {
            set { _opstatus = value; }
            get { return _opstatus; }
        }
        private string _parameterkey;
        /// <summary>
        ///关键字
        /// </summary>
        public string ParameterKey
        {
            set { _parameterkey = value; }
            get { return _parameterkey; }
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
        private string _opsign;
        /// <summary>
        ///操作标识
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
        #endregion Model
    }


}
