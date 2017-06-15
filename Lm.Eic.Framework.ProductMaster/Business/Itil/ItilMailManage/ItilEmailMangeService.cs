using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Business.Itil.ItilMailManage
{
    /// <summary>
    /// 邮箱管理器
    /// </summary>
   public class ItilEmailMangeService
    {
        public static ItilEmailManager ItilEmailManager
        {
            get { return OBulider.BuildInstance<ItilEmailManager>(); }
        }



    }
}
