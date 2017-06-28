using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Model.Tools
{
   public  class QueryWorkTaskManageDto
    {
        public string SystemName { get; set; }
        public string ModuleName { get; set; }
        private int _searchMode = 0;
        /// <summary>
        /// 根据搜索模式查询
        /// </summary>
        public int SearchMode
        {
            set { _searchMode = value; }
            get { return _searchMode; }
        }



    }
}
