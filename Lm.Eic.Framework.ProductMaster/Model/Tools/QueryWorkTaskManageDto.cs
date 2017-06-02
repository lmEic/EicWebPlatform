using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Model.Tools
{
   public class QueryWorkTaskManageDto
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
        private string _moduleName;
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName
        {
            get { return _moduleName; }
            set { if (_moduleName != value) { _moduleName = value; } }
        }

        private string _systemName;
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName
        {
            get { return _systemName; }
            set { if (_systemName != value) { _systemName = value; } }
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

        public string QueryContent { get; set; }

        public int IsDelete { get; set; }
    }
}
