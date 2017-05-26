using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeObjectBuilder;

namespace Lm.Eic.Framework.ProductMaster.Business.Tools.tlOnline
{
    /// <summary>
    /// 工作任务管理工厂
    /// </summary>
   public static class WorkTaskManageFactory
    {
       internal static WorkTaskManageCrud WorkTaskManageCrud
        {
            get { return OBulider.BuildInstance<WorkTaskManageCrud>(); }
        }

    }
}
