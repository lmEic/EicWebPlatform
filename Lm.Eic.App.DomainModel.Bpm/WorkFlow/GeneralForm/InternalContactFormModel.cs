using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.WorkFlow.GeneralForm
{
    /// <summary>
    ///内部联络单模型
    /// </summary>
    [Serializable]
    public partial class InternalContactFormModel
    {
        public InternalContactFormModel()
        { }
        #region Model
        private string _formid;
        /// <summary>
        ///表单编号
        /// </summary>
        public string FormId
        {
            set { _formid = value; }
            get { return _formid; }
        }
        private string _formsubject;
        /// <summary>
        ///表单主题
        /// </summary>
        public string FormSubject
        {
            set { _formsubject = value; }
            get { return _formsubject; }
        }
        private string _formcontent;
        /// <summary>
        ///表单内容
        /// </summary>
        public string FormContent
        {
            set { _formcontent = value; }
            get { return _formcontent; }
        }
        private DateTime _applydate;
        /// <summary>
        ///申请日期
        /// </summary>
        public DateTime ApplyDate
        {
            set { _applydate = value; }
            get { return _applydate; }
        }
        private DateTime _needdate;
        /// <summary>
        ///需求日期
        /// </summary>
        public DateTime NeedDate
        {
            set { _needdate = value; }
            get { return _needdate; }
        }
        private string _participantinfo;
        /// <summary>
        ///参与者信息
        /// </summary>
        public string ParticipantInfo
        {
            set { _participantinfo = value; }
            get { return _participantinfo; }
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
        private string _yearmonth;
        /// <summary>
        ///年月分
        /// </summary>
        public string YearMonth
        {
            set { _yearmonth = value; }
            get { return _yearmonth; }
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



}
