using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeObjectBuilder;

namespace Lm.Eic.App.Business.Bmp.WorkFlow.GeneralForm
{
    /// <summary>
    /// 通用表单服务接口
    /// </summary>
    public static class GeneralFormService
    {
        /// <summary>
        /// 内部联络单管理器
        /// </summary>
        public static InternalContactFormManager InternalContactFormManager
        {
            get { return OBulider.BuildInstance<InternalContactFormManager>(); }
        }
    }
}
