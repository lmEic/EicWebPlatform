using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Quanity
{
    public class AddressBookModel
    {
        #region Model
        private string _name;
        /// <summary>
        ///姓名
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        private string _sex;
        /// <summary>
        ///性名
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
        private string _computerdepartment;
        /// <summary>
        ///单位
        /// </summary>
        public string ComputerDepartment
        {
            set { _computerdepartment = value; }
            get { return _computerdepartment; }
        }
        private string _workerposition;
        /// <summary>
        ///职位
        /// </summary>
        public string WorkerPosition
        {
            set { _workerposition = value; }
            get { return _workerposition; }
        }
        private string _contactmemo;
        /// <summary>
        ///内容
        /// </summary>
        public string ContactMemo
        {
            set { _contactmemo = value; }
            get { return _contactmemo; }
        }
        private string _telephone;
        /// <summary>
        ///手机
        /// </summary>
        public string Telephone
        {
            set { _telephone = value; }
            get { return _telephone; }
        }
        private string _officetelephone;
        /// <summary>
        ///办公电话
        /// </summary>
        public string OfficeTelephone
        {
            set { _officetelephone = value; }
            get { return _officetelephone; }
        }
        private string _faxtelephone;
        /// <summary>
        ///传真
        /// </summary>
        public string FaxTelephone
        {
            set { _faxtelephone = value; }
            get { return _faxtelephone; }
        }
        private string _mail;
        /// <summary>
        ///Email
        /// </summary>
        public string Mail
        {
            set { _mail = value; }
            get { return _mail; }
        }
        private string _adress;
        /// <summary>
        ///公司地址
        /// </summary>
        public string Address
        {
            set { _adress = value; }
            get { return _adress; }
        }
        private string _qqorskype;
        /// <summary>
        ///QQ或Skype
        /// </summary>
        public string QQorSkype
        {
            set { _qqorskype = value; }
            get { return _qqorskype; }
        }
        private string _websiteadress;
        /// <summary>
        ///网址
        /// </summary>
        public string WebsiteAddress
        {
            set { _websiteadress = value; }
            get { return _websiteadress; }
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
