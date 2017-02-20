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
}
