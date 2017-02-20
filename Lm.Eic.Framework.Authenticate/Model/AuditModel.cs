using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.Authenticate.Model
{
    /// <summary>
    /// 审核模型
    /// </summary>
    [Serializable]
    public partial class AuditModel
    {
        public AuditModel()
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
        private string _parameterkey;
        /// <summary>
        ///组合键
        /// </summary>
        public string ParameterKey
        {
            set { _parameterkey = value; }
            get { return _parameterkey; }
        }
        private int _auditrand;
        /// <summary>
        ///审核级别
        /// </summary>
        public int AuditRand
        {
            set { _auditrand = value; }
            get { return _auditrand; }
        }
        private int _startrand;
        /// <summary>
        ///起始级别
        /// </summary>
        public int StartRand
        {
            set { _startrand = value; }
            get { return _startrand; }
        }
        private int _endrand;
        /// <summary>
        ///结束级别
        /// </summary>
        public int EndRand
        {
            set { _endrand = value; }
            get { return _endrand; }
        }
        private string _auditstate;
        /// <summary>
        ///审核状态
        /// </summary>
        public string AuditState
        {
            set { _auditstate = value; }
            get { return _auditstate; }
        }
        private string _audituser;
        /// <summary>
        ///审核人
        /// </summary>
        public string AuditUser
        {
            set { _audituser = value; }
            get { return _audituser; }
        }
        private string _comment;
        /// <summary>
        ///审核语
        /// </summary>
        public string Comment
        {
            set { _comment = value; }
            get { return _comment; }
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
        ///操作标示
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
