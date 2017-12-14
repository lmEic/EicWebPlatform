using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Pms.LeaveAsk
{
    [Serializable]
  public partial class LeaveAskManagerModels
    {
        public LeaveAskManagerModels()
        { }
        private string _parentDataNodeText;
        /// <summary>
        /// 部级代码
        /// </summary>
        public string ParentDataNodeText
        {
            set { _parentDataNodeText = value; }
            get { return _parentDataNodeText; }
        }
        private string _workerId;
        /// <summary>
        /// 工号
        /// </summary>
        public string WorkerId
        {
            set { _workerId = value; }
            get { return _workerId; }
        }
        private string _workername;
        /// <summary>
        /// 姓名
        /// </summary>
        public string WorkerName
        {
            set { _workername = value; }
            get { return _workername; }
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
        private string _leaveType;
        /// <summary>
        /// 请假类别
        /// </summary>
        public string LeaveType
        {
            set { _leaveType = value; }
            get { return _leaveType; }
        }
        private double _leaveHours;
        /// <summary>
        /// 请假时数
        /// </summary>
        public double LeaveHours
        {
            set { _leaveHours = value; }
            get { return _leaveHours; }
        }
        private DateTime _leaveApplyDate;
        /// <summary>
        /// 申请日期
        /// </summary>
        public DateTime LeaveApplyDate
        {
            set { _leaveApplyDate = value; }
            get { return _leaveApplyDate; }
        }
        private DateTime _leaveAskDate;
        /// <summary>
        /// 请假时间
        /// </summary>
        public DateTime LeaveAskDate
        {
            set { _leaveAskDate = value; }
            get { return _leaveAskDate; }
        }

        private string _leaveMemo;
        /// <summary>
        /// 请假备注
        /// </summary>
        public string LeaveMemo
        {
            set { _leaveMemo = value; }
            get { return _leaveMemo; }
        }
        private string _leaveTimerStart;
        /// <summary>
        /// 请假开始时间
        /// </summary>
        public string LeaveTimerStart
        {
            set { _leaveTimerStart = value; }
            get { return _leaveTimerStart; }
        }
        private string _leaveTimerEnd;
        /// <summary>
        /// 请假结束时间
        /// </summary>
        public string LeaveTimerEnd
        {
            set { _leaveTimerEnd = value; }
            get { return _leaveTimerEnd; }
        }
        private string _leaveState;
        /// <summary>
        /// 请假状态
        /// </summary>
        public string LeaveState
        {
            set { _leaveState = value; }
            get { return _leaveState; }
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
        private string _opperson;

        /// <summary>
        ///操作人1
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
