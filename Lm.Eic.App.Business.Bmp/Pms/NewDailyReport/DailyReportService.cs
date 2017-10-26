using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.NewDailyReport
{
    public static class DailyProductionReportService
    {
        /// <summary>
        /// 日报配置管理器
        /// </summary>
        public static DailyProductionReportManager ProductionConfigManager
        {
            get { return OBulider.BuildInstance<DailyProductionReportManager>(); }
        }
    }
}
