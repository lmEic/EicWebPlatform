using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.DailyReport
{
    public static class DailyReportService
    {
        /// <summary>
        /// 日报输入管理器
        /// </summary>
        public static InputManager InputManager
        {
            get { return OBulider.BuildInstance<InputManager>(); }
        }

        /// <summary>
        /// 日报配置管理器
        /// </summary>
        public static ConfigManager ConfigManager
        {
            get { return OBulider.BuildInstance<ConfigManager>(); }
        }

    }
}
