using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.DomainModel.Bpm.Hrm.GeneralAffairs
{
    public class QueryGeneralAffairsDto
    {
        string _department;
        /// <summary>
        /// 部门
        /// </summary>
        public string Department
        {
            get { return _department; }
            set { if (_department != value) { _department = value; } }
        }

        /// <summary>
        /// 领取月
        /// </summary>
        public string ReceiveMonth { get; set; }

        private string _workerId;
        /// <summary>
        /// 员工工号
        /// </summary>
        public string WorkerId
        {
            get { return _workerId; }
            set { if (_workerId != value) { _workerId = value; } }
        }
        private int _searchMode = 0;
        /// <summary>
        /// 搜索模式
        /// </summary>
        public int SearchMode
        {
            get { return _searchMode; }
            set { if (_searchMode != value) { _searchMode = value; } }
        }
    }
}
