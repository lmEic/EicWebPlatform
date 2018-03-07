using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DomainModel.Bpm.Pms.LeaveAsk
       
{

   public  class LeaveAskManagerModelDto
   {
        public string WorkerId { get; set; }
        public string Department { get; set; }
        public string LeaveSate { get; set; }
        public string LeaveType { get; set; }
        private int _searchMode = 0;
        public int SearchMode
        {
            set { _searchMode = value; }
            get { return _searchMode; }
        }

    }
}
