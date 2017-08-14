using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Framework.ProductMaster.Model.CommonManage;
using Lm.Eic.Framework.ProductMaster.DbAccess.Repository.CommonManageRepository;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeOOMapper;

namespace Lm.Eic.Framework.ProductMaster.Business.Common
{
    internal class FormAttachFileCrud : CrudBase<FormAttachFileManageModel, IFormIAttachFileManageRepository>
    {
        public FormAttachFileCrud() : base(new FormIAttachFileManageRepository(), "表单附件")
        { }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, Add);
        }

        public OpResult Add(FormAttachFileManageModel entity)
        {
            if (!irep.IsExist(e => e.ModuleName == entity.ModuleName && e.FormId == entity.FormId))
            {
                return irep.Insert(entity).ToOpResult_Add(OpContext);
            }
            return OpResult.SetSuccessResult("上传文件成功！");
        }
    }
}
