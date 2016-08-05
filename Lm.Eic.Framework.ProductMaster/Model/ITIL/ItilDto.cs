using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Model.ITIL
{
    public class ItilDto
    {
        /// <summary>
        /// 进度标识列表
        /// </summary>
        public List<string> ProgressSignList { get; set; }

        /// <summary>
        /// 函数名
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// 搜索模式
        /// </summary>
        public int SearchMode { get; set; }
        
    }
}
