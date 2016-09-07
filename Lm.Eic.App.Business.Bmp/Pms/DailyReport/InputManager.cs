using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.DailyReport
{
    /// <summary>
    /// 日报输入管理器
    /// </summary>
    public class InputManager
    {
        /// <summary>
        /// 日报输入管理器
        /// </summary>
        public DailyReportInputManager DailyReportInputManager
        {
            get { return OBulider.BuildInstance<DailyReportInputManager>(); }
        }

        /// <summary>
        /// 日报模板管理器
        /// </summary>
        public DailyReportTemplateManager DailyReportTemplateManager
        {
            get { return OBulider.BuildInstance<DailyReportTemplateManager>(); }
        }
    }


 

    /// <summary>
    /// 日报录入管理器
    /// </summary>
    public class DailyReportInputManager
    {

    }

 
    /// <summary>
    /// 日报模板管理器
    /// </summary>
    public class DailyReportTemplateManager
    {

    }


}
