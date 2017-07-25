using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.Qua8DReportManage
{
    public static class Qua8DService
    {
        public static Qua8DManager Qua8DManager
        {
            get { return OBulider.BuildInstance<Qua8DManager>(); }
        }
    }
}
