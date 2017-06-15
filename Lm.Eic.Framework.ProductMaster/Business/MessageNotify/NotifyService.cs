using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Business.MessageNotify
{
   public static  class NotifyService
    {
        /// <summary>
        /// 开发配置消息通知地址管理
        /// </summary>
        public static NotifyModuleManager NotifyManager
        {
            get { return OBulider.BuildInstance<NotifyModuleManager>(); }
        }
    }
}
