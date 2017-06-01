using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Model.Tools
{
    /// <summary>
    /// 工作任务管理模型类
    /// </summary>
    [Serializable]
    public partial class WorkTaskManageModel
    {
        public WorkTaskManageModel()
        { }
        /// <summary>
        /// 部门
        /// </summary>
        private string _department;
        public string Department
        {
            set { _department = value; }
            get { return _department; }
        }
        private string _systemName;
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName
        {
            set { _systemName = value; }
            get { return _systemName; }

        }
        private string _moduleName;
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName
        {
            set { _moduleName = value; }
            get { return _moduleName; }

        }
        private string _workItem;
        /// <summary>
        /// 项目功能
        /// </summary>
        public string WorkItem
        {
            set { _workItem = value; }
            get { return _workItem; }
        }
        private string _workDescription;
        /// <summary>
        /// 项目描述
        /// </summary>
        public string WorkDescription
        {
            set { _workDescription = value; }
            get { return _workDescription; }
        }
        private int _difficultyCoefficient;
        /// <summary>
        /// 难度系数
        /// </summary>
        public int DifficultyCoefficient
        {
            set { _difficultyCoefficient = value; }
            get { return _difficultyCoefficient; }
        }
        private int _workPriority;
        /// <summary>
        /// 优先级别
        /// </summary>
        public int WorkPriority
        {
            set { _workPriority = value; }
            get { return _workPriority; }
        }
        private DateTime _startDate;
        /// <summary>
        ///开始日期
        /// </summary>
        public DateTime StartDate
        {
            set { _startDate = value; }
            get { return _startDate; }
        }
        private DateTime _endDate;
        /// <summary>
        ///完成日期
        /// </summary>
        public DateTime EndDate
        {
            set { _endDate = value; }
            get { return _endDate; }
        }
        private string _progressStatus;
        /// <summary>
        /// 进度状态
        /// </summary>
        public string ProgressStatus
        {
            set { _progressStatus = value; }
            get { return _progressStatus; }
        }
        private string _progressDescription;
        /// <summary>
        /// 进度描述
        /// </summary>
        public string ProgressDescription
        {
            set { _progressDescription = value; }
            get { return _progressDescription; }
        }
        private string _orderPerson;
        /// <summary>
        /// 项目执行人
        /// </summary>
        public string OrderPerson
        {
            set { _orderPerson = value; }
            get { return _orderPerson; }
        }

        private string _checkPerson;
        /// <summary>
        /// 项目审核人
        /// </summary>
        public string CheckPerson
        {
            set { _checkPerson = value; }
            get { return _checkPerson; }
        }
        private string _remark;
        /// <summary>
        /// 项目审核人
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        private string _opperson;

        private int _isdelete=1;
        public int IsDelete
        {
            set { _isdelete = value; }
            get { return _isdelete; }
        }


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
        public partial class QueryWorkTaskDto
        {
            public string department;
            public string Department
            {
                get { return department; }
                set { if (department != value) { department = value; } }
            }
            int _isDelete = 1;
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
            bool isExactQuery = false;
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
}
