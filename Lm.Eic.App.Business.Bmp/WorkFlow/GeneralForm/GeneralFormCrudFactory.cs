using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.App.DomainModel.Bpm.WorkFlow.GeneralForm;
using Lm.Eic.App.DbAccess.Bpm.Repository.WorkFlow.GeneralForm;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Framework.ProductMaster.Business.Common;

namespace Lm.Eic.App.Business.Bmp.WorkFlow.GeneralForm
{
    /// <summary>
    /// 通用表单数据操作工厂
    /// </summary>
    internal class GeneralFormCrudFactory
    {
        /// <summary>
        /// 内部联络单数据操作助手
        /// </summary>
        internal static InternalContactFormCrud IContctFormCrud
        {
            get { return OBulider.BuildInstance<InternalContactFormCrud>(); }
        }
        /// <summary>
        /// 表单流程签核数据操作助手
        /// </summary>
        internal static FormCheckFlowTableCrud FormCheckFlowCrud
        {
            get { return OBulider.BuildInstance<FormCheckFlowTableCrud>(); }
        }
    }

    /// <summary>
    /// 表单签核流程数据操作助手
    /// </summary>
    public class FormCheckFlowTableCrud : CrudBase<WfFormCheckFlowModel, IWfFormCheckFlowRepository>
    {
        public FormCheckFlowTableCrud() : base(new WfFormCheckFlowRepository(), "签核流程")
        {

        }

        protected override void AddCrudOpItems()
        {
            throw new NotImplementedException();
        }
    }
}
