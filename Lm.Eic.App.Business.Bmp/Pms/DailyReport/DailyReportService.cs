using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.DailyReport
{
    public static  class DailyReportService
    {
        /// <summary>
        /// 日报输入管理器
        /// </summary>
        public static class DailyReportEnterManager
        {

            /// <summary>
            /// 日报输入管理器
            /// </summary>
            public static DailyReportInputManager DailyReportInputManager
            {
                get { return OBulider.BuildInstance<DailyReportInputManager>(); }
            }

            /// <summary>
            /// 日报模板管理器
            /// </summary>
            public static DailyReportTemplateManager DailyReportTemplateManager
            {
                get { return OBulider.BuildInstance<DailyReportTemplateManager>(); }
            }
        }

        /// <summary>
        /// 日报配置管理器
        /// </summary>
        public static class DailyReportConfigManager
        {
            /// <summary>
            /// 工序管理
            /// </summary>
            public static ProductFlowManager ProductFlowManager
            {
                get { return OBulider.BuildInstance<ProductFlowManager>(); }
            }
        }
      
    }
}
