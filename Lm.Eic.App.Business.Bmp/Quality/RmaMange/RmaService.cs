using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Lm.Eic.App.Business.Bmp.Quality.RmaMange
{
    public static class RmaService
    {
        /// <summary>
        /// 创建Ram表单
        /// </summary>
        public static RmaReportManager CreateRmaReport
        {
            get { return OBulider.BuildInstance<RmaReportManager>(); }
        }
        /// <summary>
        /// 检验处理Ram表单
        /// </summary>
        public static RmaManager RmaReport
        {
            get { return OBulider.BuildInstance<RmaManager>(); }
        }


    }
}
