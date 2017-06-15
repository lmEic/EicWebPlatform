using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Model.ITIL
{
   public class ItilEmailManageDto
    {
       
        private string _workerId;
        /// <summary>
        /// 员工工号
        /// </summary>
        public string WorkerId
        {
            get { return _workerId; }
            set { if (_workerId != value) { _workerId = value; } }
         
        }
        private string _email;
        public string Email
        {
            get { return _email; }
            set { if (_email != null) { _email = value; } }
        
        }
        private int _searchMode = 0;
        public int SearchMode
        {
            get { return _searchMode; }
            set { if (_searchMode != value) { _searchMode = value; } }
          
        }

    }
}
