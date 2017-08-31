using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.NewDailyReport
{
    public static class DailyReportService
    {
        /// <summary>
        /// 日报配置管理器
        /// </summary>
        public static DailyReportManager ConfigManager
        {
            get { return OBulider.BuildInstance<DailyReportManager>(); }
        }
    }
}
