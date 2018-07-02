using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Hrm.Archives
{
    /// <summary>
    /// 查询职工档案转输入对象
    /// </summary>
    public class QueryWorkerArchivesDto
    {
       public  QueryWorkerArchivesDto()
        {

        }
       
         string _Department;
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Department
        {
            get
            {
                return _Department;
            }
            set
            {
                if (_Department != value)
                {
                    _Department = value;
                }
            }
        }
        
        private string _WorkerId;
        /// <summary>
        /// 作业工号
        /// </summary>
        public string WorkerId
        {
            get
            {
                return _WorkerId;
            }
            set
            {
                if (_WorkerId != value)
                {
                    _WorkerId = value;
                }
            }
        }
        private string postNature;
        /// <summary>
        /// 直接/间接
        /// </summary>

        public string PostNature
        {
            get { return postNature; }
            set { postNature = value; }
        }
        private string workerIdType;
        /// <summary>
        /// 工号属性
        /// </summary>
        public string WorkerIdType
        {
            get { return workerIdType; }
            set { workerIdType = value; }
        }
        /// <summary>
        /// 报到日期起始日期
        /// </summary>
        private DateTime _RegistedDateStart;

        public DateTime RegistedDateStart
        {
            get
            {
                return _RegistedDateStart;
            }
            set
            {
                if (_RegistedDateStart != value)
                {
                    _RegistedDateStart = value;
                }
            }
        }

        private DateTime _RegistedDateEnd;

        /// <summary>
        /// 报到日期截至日期
        /// </summary>
        public DateTime RegistedDateEnd
        {
            get
            {
                return _RegistedDateEnd;
            }
            set
            {
                if (_RegistedDateEnd != value)
                {
                    _RegistedDateEnd = value;
                }
            }
        }


        private string _BirthMonth;
        /// <summary>
        /// 出生年月
        /// </summary>
        public string BirthMonth
        {
            get { return _BirthMonth; }
            set
            {
                if (_BirthMonth!=value)
                { _BirthMonth = value; }
            }
        }

       public  string WorkingStatus
        {
            set;get;
        }


        private string _MarryStatus;
        /// <summary>
        /// 婚姻状态
        /// </summary>
        public string MarryStatus
        {
            get { return _MarryStatus; }
            set
            {
                if (_MarryStatus != value)
                { _MarryStatus = value; }
            }
        }
        private int searchMode = 0;
        /// <summary>
        /// 搜索模式
        /// </summary>
        public int SearchMode
        {
            get { return searchMode; }
            set {  searchMode = value;  }
        }
    }

    /// <summary>
    /// 查询职工档案转输入对象
    /// 0: 默认载入全部在职数据
    /// 1：根据人员工号查询
    /// 2：根据人员所属部门载入数据
    /// 3：依入职时间段查询
    /// 4：直接/间接
    /// 5：职工属性
    /// 6：出生年月
    /// 7：婚姻状况
    /// </summary>
    public class QueryWorkerArchivesDtostring
    {
        public QueryWorkerArchivesDtostring(string querystring)
        {
            _Querystring = querystring;
        }
        string _Querystring;
        /// <summary>
        /// 查询字符串
        /// </summary>
        public string Querystring
        {
            get
            {
                return _Querystring;
            }
            set
            {
                if (_Querystring != value)
                {
                    _Querystring = value;
                }
            }
        }
        string _Department;
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Department
        {
            get
            {
                return _Department;
            }
            set
            {
                if (_Department != value)
                {
                    _Department = value;
                }
            }
        }

        private string _WorkerId=string.Empty ;
        /// <summary>
        /// 作业工号
        /// </summary>
        public string WorkerId
        {
            get
            {
                if (SearchMode==1)
                    return _Querystring;
                else return _WorkerId;
            }
           
        }
        private string postNature;
        /// <summary>
        /// 直接/间接
        /// </summary>

        public string PostNature
        {
            get { return postNature; }
            set { postNature = value; }
        }
        private string workerIdType;
        /// <summary>
        /// 工号属性
        /// </summary>
        public string WorkerIdType
        {
            get { return workerIdType; }
            set { workerIdType = value; }
        }
        /// <summary>
        /// 报到日期起始日期
        /// </summary>
        private DateTime _RegistedDateStart;

        public DateTime RegistedDateStart
        {
            get
            {
                return _RegistedDateStart;
            }
            set
            {
                if (_RegistedDateStart != value)
                {
                    _RegistedDateStart = value;
                }
            }
        }

        private DateTime _RegistedDateEnd;

        /// <summary>
        /// 报到日期截至日期
        /// </summary>
        public DateTime RegistedDateEnd
        {
            get
            {
                return _RegistedDateEnd;
            }
            set
            {
                if (_RegistedDateEnd != value)
                {
                    _RegistedDateEnd = value;
                }
            }
        }


        private string _BirthMonth;
        /// <summary>
        /// 出生年月
        /// </summary>
        public string BirthMonth
        {
            get { return _BirthMonth; }
            set
            {
                if (_BirthMonth != value)
                { _BirthMonth = value; }
            }
        }



        private string _MarryStatus;
        /// <summary>
        /// 婚姻状态
        /// </summary>
        public string MarryStatus
        {
            get { return _MarryStatus; }
            set
            {
                if (_MarryStatus != value)
                { _MarryStatus = value; }
            }
        }
        private int searchMode = 0;
        /// <summary>
        /// 搜索模式
        /// </summary>
        public int SearchMode
        {
            get { return searchMode; }
            set { searchMode = value; }
        }
    }
}
