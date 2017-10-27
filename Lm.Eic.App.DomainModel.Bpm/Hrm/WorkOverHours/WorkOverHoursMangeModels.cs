using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Hrm.WorkOverHours
{
    [Serializable]
  public partial class WorkOverHoursMangeModels
    {
        public WorkOverHoursMangeModels()
        { }
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
        private string _departmentText;
        /// <summary>
        ///部门
        /// </summary>
        public string DepartmentText
        {
            set { _departmentText = value; }
            get { return _departmentText; }
        }
        private DateTime _workdate;
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime WorkDate
        {
            set { _workdate = value; }
            get { return _workdate; }
        }
        private double _workOverHours;
        /// <summary>
        /// 加班时数
        /// </summary>
        public double  WorkOverHours
        {
            set { _workOverHours = value; }
            get { return _workOverHours; }
        }
        private string _workoverType;
        /// <summary>
        /// 加班类型
        /// </summary>
        public string WorkoverType
        {
            set { _workoverType = value; }
            get { return _workoverType; }
        }
        private string _workClassType;
        /// <summary>
        /// 班别
        /// </summary>
        public string WorkClassType
        {
            set { _workClassType = value; }
            get { return _workClassType; }
        }
        private string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        //private DateTime _opdate;
        /// <summary>
        ///操作日期
        /// </summary>
        //public DateTime OpDate
        //{
        //    set { _opdate = value; }
        //    get { return _opdate; }
        //}
        //private DateTime _optime;
        /// <summary>
        ///操作时间
        /// </summary>
        //public DateTime OpTime
        //{
        //    set { _optime = value; }
        //    get { return _optime; }
        //}
        private string _opsign;
        /// <summary>
        ///操作标志
        /// </summary>
        public string OpSign
        {
            set { _opsign = value; }
            get { return _opsign; }
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
