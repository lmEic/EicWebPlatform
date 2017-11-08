using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Hrm.WorkOverHours
{
   public class WorkOverHoursDto
    {
       
      public DateTime WorkDate { set; get; }
      public string DepartmentText { get; set; }
      public string WorkId { get; set; }
        private int _searchMode = 0;
        /// <summary>
        /// 搜索模式
        /// </summary>
        public int SearchMode
        {
            get { return _searchMode; }
            set { if (_searchMode != value) { _searchMode = value; } }
        }
        public string QryDate { set; get; }

    }
}
