using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.DomainModel.Bpm.WorkFlow.GeneralForm;
using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using Lm.Eic.Framework.ProductMaster.Business.Itil;
using Lm.Eic.App.Business.Bmp.WorkFlow;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Framework.ProductMaster.Business.Common;
using Lm.Eic.Framework.ProductMaster.Model.CommonManage;

namespace Lm.Eic.App.Business.Bmp.WorkFlow.GeneralForm
{
    /// <summary>
    /// 内部联络单管理器
    /// </summary>
    public class InternalContactFormManager : ElectronicFormBase
    {
        public InternalContactFormManager() : base("InternalContactForm")
        {
        }

        /// <summary>
        /// 自动创建内部联络单表单
        /// </summary>
        /// <param name="department">部门信息</param>
        /// <returns></returns>
        public string AutoCreateFormId(string department)
        {
            var data = CommonService.FormIdManager.CreateFormIdData(this.FormModuleName, department);
            string formId = string.Format("{0}-{1}{2}", data.Department, data.YearMonth, data.SubId);
            formId.SynchronizeFormId(data.PrimaryKey);
            return formId;
        }

        /// <summary>
        /// 创建内部联络单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public OpResult CreateInternalForm(InternalContactFormModel entity)
        {
            return GeneralFormCrudFactory.IContctFormCrud.Store(entity);
        }
    }
}
