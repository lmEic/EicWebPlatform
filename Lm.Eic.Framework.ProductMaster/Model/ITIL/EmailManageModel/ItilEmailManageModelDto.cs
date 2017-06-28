using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Model.ITIL
{
   public  class ItilEmailManageModelDto
    {
        /// <summary>
        /// 根据工号查询 
        /// </summary>
        public string WorkerId { get; set; }
        /// <summary>
        /// 根据帐号查询
        /// </summary>
        public string Email { get; set; }
        private int _searchMode = 0;

        /// <summary>
        /// 根据搜索模式查询
        /// </summary>
        public int SearchMode
        {
            set{ _searchMode = value; }
            get { return _searchMode; }
        }
        
    }
}
