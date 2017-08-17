using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeObjectBuilder;

namespace Lm.Eic.Framework.ProductMaster.Business.Common
{
    /// <summary>
    /// 公共访问服务接口
    /// </summary>
    public class CommonService
    {
        /// <summary>
        /// 表单编号管理器
        /// </summary>
        public static FormIdManager FormIdManager
        {
            get { return OBulider.BuildInstance<FormIdManager>(); }
        }
        /// <summary>
        /// 表单附件管理器
        /// </summary>
        public static FormAttachFileManager FormAttachFileManager
        {
            get { return OBulider.BuildInstance<FormAttachFileManager>(); }
        }
    }
}
