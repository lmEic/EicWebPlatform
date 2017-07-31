using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.App.DomainModel.Bpm.WorkFlow.GeneralForm;
using Lm.Eic.App.DbAccess.Bpm.Repository.WorkFlow.GeneralForm;

namespace Lm.Eic.App.Business.Bmp.WorkFlow.GeneralForm
{
    /// <summary>
    /// 通用表单数据操作工厂
    /// </summary>
    public class GeneralFormCrudFactory
    {
    }
    /// <summary>
    /// 内部联络单数据操作Crud
    /// </summary>
    public class InternalContactFormCrud : CrudBase<InternalContactFormModel, IInternalContactFormRepository>
    {
        public InternalContactFormCrud() : base(new InternalContactFormRepository(), "内部联络单")
        {
        }

        protected override void AddCrudOpItems()
        {
            throw new NotImplementedException();
        }
    }
}
