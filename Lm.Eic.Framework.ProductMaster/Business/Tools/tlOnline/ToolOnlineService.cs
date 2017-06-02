using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeObjectBuilder;

namespace Lm.Eic.Framework.ProductMaster.Business.Tools.tlOnline
{
    /// <summary>
    /// 在线助手服务门面
    /// </summary>
    public class ToolOnlineService
    {
        /// <summary>
        /// 
        /// </summary>
        public static CollaborateContactManager ContactManager
        {
            get { return OBulider.BuildInstance<CollaborateContactManager>(); }
        }

        /// <summary>
        /// 工作任务管理器
        /// </summary>
        public static WorkTaskManager WorkTaskManage
        {
            get { return OBulider.BuildInstance<WorkTaskManager>(); }
        }
    }
}
