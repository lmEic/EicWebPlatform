using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.DomainModel.Bpm.Ast
{
    public class QueryEquipmentDto
    {
        private string assetNumber = string.Empty;

        /// <summary>
        /// 财产编号
        /// </summary>
        public string AssetNumber
        {
            get { return assetNumber; }
            set { if (assetNumber != value) { assetNumber = value; } }
        }

        private int searchMode = 0;
        /// <summary>
        /// 搜索模式
        /// </summary>
        public int SearchMode
        {
            get { return searchMode; }
            set { if (searchMode != value) { searchMode = value; } }
        }

    }
}
