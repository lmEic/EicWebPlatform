using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Business.Tools.tlOnline
{
    /// <summary>
    /// 在线助手服务门面
    /// </summary>
    public static class ToolOnlineService
    {
        /// <summary>
        /// 
        /// </summary>
        public static CollaborateContactManager ContactManager
        {
            get { return OBulider.BuildInstance<CollaborateContactManager>(); }
        }
    }
}
