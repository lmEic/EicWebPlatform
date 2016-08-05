using System;

namespace Lm.Eic.App.DomainModel.Bpm.Hrm.GeneralAffairs
{
    /// <summary>
    ///厂服管理模型
    /// </summary>
    [Serializable]
    public partial class WorkClothesManageModel
    {
        public WorkClothesManageModel()
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
        private string _workername;
        /// <summary>
        ///姓名
        /// </summary>
        public string WorkerName
        {
            set { _workername = value; }
            get { return _workername; }
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
        private string _productname;
        /// <summary>
        ///品名
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        private string _productspecify;
        /// <summary>
        ///规格
        /// </summary>
        public string ProductSpecify
        {
            set { _productspecify = value; }
            get { return _productspecify; }
        }
        private string _productcategory;
        /// <summary>
        ///产品类别
        /// </summary>
        public string ProductCategory
        {
            set { _productcategory = value; }
            get { return _productcategory; }
        }
        private int _percount;
        /// <summary>
        ///领取数量
        /// </summary>
        public int PerCount
        {
            set { _percount = value; }
            get { return _percount; }
        }
        private string _unit;
        /// <summary>
        ///单位
        /// </summary>
        public string Unit
        {
            set { _unit = value; }
            get { return _unit; }
        }
        private DateTime _inputdate;
        /// <summary>
        ///录入日期
        /// </summary>
        public DateTime InputDate
        {
            set { _inputdate = value; }
            get { return _inputdate; }
        }
        private string  _dealwithtype;
        /// <summary>
        ///处理类型
        /// </summary>
        public string  DealwithType
        {
            set { _dealwithtype = value; }
            get { return _dealwithtype; }
        }
        private string _receiveuser;
        /// <summary>
        ///领取人
        /// </summary>
        public string ReceiveUser
        {
            set { _receiveuser = value; }
            get { return _receiveuser; }
        }
        private string _receivemonth;
        /// <summary>
        ///领取月份
        /// </summary>
        public string ReceiveMonth
        {
            set { _receivemonth = value; }
            get { return _receivemonth; }
        }
        private DateTime  _opdate;
        /// <summary>
        ///操作日期
        /// </summary>
        public DateTime  OpDate
        {
            set { _opdate = value; }
            get { return _opdate; }
        }
        private DateTime  _optime;
        /// <summary>
        ///操作时间
        /// </summary>
        public DateTime  OpTime
        {
            set { _optime = value; }
            get { return _optime; }
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
