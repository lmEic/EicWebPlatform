using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Business.Itil
{
    public class ItilService
    {
        /// <summary>
        /// 开发管理
        /// </summary>
        public static ItilDevelopModuleManager ItilDevelopModuleManager
        {
            get { return OBulider.BuildInstance<ItilDevelopModuleManager>(); }
        }
    }
}
