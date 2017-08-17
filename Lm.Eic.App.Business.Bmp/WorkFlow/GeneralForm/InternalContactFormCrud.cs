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
using Lm.Eic.Uti.Common.YleeOOMapper;

namespace Lm.Eic.App.Business.Bmp.WorkFlow.GeneralForm
{
    /// <summary>
    /// 内部联络单数据操作Crud
    /// </summary>
    internal class InternalContactFormCrud : CrudBase<InternalContactFormModel, IInternalContactFormRepository>
    {
        public InternalContactFormCrud() : base(new InternalContactFormRepository(), "内部联络单")
        {
        }

        private string formModuleName = "InternalContactForm";

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, Add);
        }

        private OpResult Add(InternalContactFormModel entity)
        {
            entity.ApplyDate = DateTime.Now.ToDate();
            entity.YearMonth = DateTime.Now.ToYearMonth();
            if (!this.irep.IsExist(e => e.FormId == entity.FormId))
            {
                var opresult = irep.Insert(entity).ToOpResult_Add(OpContext);
                if (opresult.Result)
                    entity.FormId.NormalizeFormId(formModuleName);
                return opresult;
            }
            return OpResult.SetSuccessResult("创建成功！");
        }
    }
}
