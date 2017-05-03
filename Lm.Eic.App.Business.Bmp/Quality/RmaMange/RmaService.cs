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
        /// 检验Ram表单处理管理
        /// </summary>
        public static RmaManager RmaManger
        {
            get { return OBulider.BuildInstance<RmaManager>(); }
        }


    }
}
