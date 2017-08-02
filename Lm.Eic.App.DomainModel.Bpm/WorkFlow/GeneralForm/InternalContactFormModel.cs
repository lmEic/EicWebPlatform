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
        private string _orderid;
        /// <summary>
        ///单号
        /// </summary>
        public string OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
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
        private string _applydepartment;
        /// <summary>
        ///申请单位
        /// </summary>
        public string ApplyDepartment
        {
            set { _applydepartment = value; }
            get { return _applydepartment; }
        }
        private string _applyperson;
        /// <summary>
        ///申请人
        /// </summary>
        public string ApplyPerson
        {
            set { _applyperson = value; }
            get { return _applyperson; }
        }
        private string _relatedtoperson;
        /// <summary>
        ///相关人
        /// </summary>
        public string RelatedToPerson
        {
            set { _relatedtoperson = value; }
            get { return _relatedtoperson; }
        }
        private string _relatedtodepartment;
        /// <summary>
        ///相关单位
        /// </summary>
        public string RelatedToDepartment
        {
            set { _relatedtodepartment = value; }
            get { return _relatedtodepartment; }
        }
        private string _confirmor;
        /// <summary>
        ///确认人
        /// </summary>
        public string Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        private string _approver;
        /// <summary>
        ///核准人
        /// </summary>
        public string Approver
        {
            set { _approver = value; }
            get { return _approver; }
        }
        private string _field2;
        /// <summary>
        ///2
        /// </summary>
        public string Field2
        {
            set { _field2 = value; }
            get { return _field2; }
        }
        private string _field3;
        /// <summary>
        ///3
        /// </summary>
        public string Field3
        {
            set { _field3 = value; }
            get { return _field3; }
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
