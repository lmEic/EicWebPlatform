using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Business.MailSend
{
  public static   class MailSendService
    {
        /// <summary>
        /// 邮件管理
        /// </summary>
        public static MailManager MailManager
        {
            get { return OBulider.BuildInstance<MailManager>(); }
        }
    }
}
