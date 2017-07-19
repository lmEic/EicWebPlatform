using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Model.Tools
{
   public class ReportImproveProblemModelsDto
    {
        public string ProblemSolve { get; set; }
        private int _searchMode = 0;
        public int SearchMode
        {
            set { _searchMode = value; }
            get { return _searchMode; }
        }
    }
}
