using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Hrm.WorkOverHours
{
   public class WorkOverHoursService
   {
        /// <summary>
        /// 加班管理器
        /// </summary>
        public static WorkOverHoursManager WorkOverHoursManager
        {
            get { return OBulider.BuildInstance<WorkOverHoursManager>(); }
        }


   }
}
