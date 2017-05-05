using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Lm.Eic.App.Business.Bmp.Quality.RmaManage
{
    public static class RmaService
    {
        /// <summary>
        /// 检验Ram表单处理管理
        /// </summary>
        public static RmaManager RmaManager
        {
            get { return OBulider.BuildInstance<RmaManager>(); }
        }
    }
}
