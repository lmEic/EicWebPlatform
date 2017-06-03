using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Model.Tools
{
    /// <summary>
    ///合作联系人库模型
    /// </summary>
    [Serializable]
    public partial class CollaborateContactLibModel
    {
        public CollaborateContactLibModel()
        { }
        #region Model
        private string _department;
        /// <summary>
        ///部门
        /// </summary>
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        private string _contactperson;
        /// <summary>
        ///联系人
        /// </summary>
        public string ContactPerson
        {
            set { _contactperson = value; }
            get { return _contactperson; }
        }
        private string _sex;
        /// <summary>
        ///性别
        /// </summary>
        public string Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        private string _customercategory;
        /// <summary>
        ///类别
        /// </summary>
        public string CustomerCategory
        {
            set { _customercategory = value; }
            get { return _customercategory; }
        }
        private string _contactcompany;
        /// <summary>
        ///单位
        /// </summary>
        public string ContactCompany
        {
            set { _contactcompany = value; }
            get { return _contactcompany; }
        }
        private string _workerposition;
        /// <summary>
        ///职务
        /// </summary>
        public string WorkerPosition
        {
            set { _workerposition = value; }
            get { return _workerposition; }
        }
        private string _contactmemo;
        /// <summary>
        ///往来业务
        /// </summary>
        public string ContactMemo
        {
            set { _contactmemo = value; }
            get { return _contactmemo; }
        }
        private string _telephone;
        /// <summary>
        ///手机号码
        /// </summary>
        public string Telephone
        {
            set { _telephone = value; }
            get { return _telephone; }
        }
        private string _officetelephone;
        /// <summary>
        ///固话
        /// </summary>
        public string OfficeTelephone
        {
            set { _officetelephone = value; }
            get { return _officetelephone; }
        }
        private string _fax;
        /// <summary>
        ///传真
        /// </summary>
        public string Fax
        {
            set { _fax = value; }
            get { return _fax; }
        }
        private string _mail;
        /// <summary>
        ///邮箱
        /// </summary>
        public string Mail
        {
            set { _mail = value; }
            get { return _mail; }
        }
        private string _qqorskype;
        /// <summary>
        ///QQ
        /// </summary>
        public string QqOrSkype
        {
            set { _qqorskype = value; }
            get { return _qqorskype; }
        }
        private string _contactadress;
        /// <summary>
        ///地址
        /// </summary>
        public string ContactAdress
        {
            set { _contactadress = value; }
            get { return _contactadress; }
        }
        private string _websiteadress;
        /// <summary>
        ///网址
        /// </summary>
        public string WebsiteAdress
        {
            set { _websiteadress = value; }
            get { return _websiteadress; }
        }
        private int _isdelete;
        /// <summary>
        ///是否删除 0表示删除 1表示启用
        /// </summary>
        public int IsDelete
        {
            set { _isdelete = value; }
            get { return _isdelete; }
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
        #endregion Model
    }
    public partial class QueryContactDto
    {
        string department;
        /// <summary>
        /// 部门
        /// </summary>
        public string Department
        {
            get { return department; }
            set { if (department != value) { department = value; } }
        }
        int _isDelete = 0;
        /// <summary>
        /// 联系人是否失效  1：表示有效， 0：表示无效
        /// </summary>
        public int IsDelete
        {
            get { return _isDelete; }
            set { if (_isDelete != value) { _isDelete = value; } }
        }
        string queryContent = string.Empty;
        /// <summary>
        /// 查询内容
        /// </summary>
        public string QueryContent
        {
            get { return queryContent; }
            set { if (queryContent != value) { queryContent = value; } }
        }
      
        /// <summary>
        /// 是否精确查询
        /// </summary>
        bool isExactQuery = false ;
        public bool IsExactQuery
        {
            get { return isExactQuery; }
            set { if (isExactQuery != value) { isExactQuery = value; } }
        }
        private int searchMode = 0;
        /// <summary>
        /// 搜索模式
        /// </summary>
        public int SearchMode
        {
            get { return searchMode; }
            set { if (searchMode != value) { searchMode = value; } }
        }


    }
}
