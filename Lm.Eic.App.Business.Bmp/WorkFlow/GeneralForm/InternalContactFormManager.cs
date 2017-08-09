using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.DomainModel.Bpm.WorkFlow.GeneralForm;
using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using Lm.Eic.Framework.ProductMaster.Business.Itil;
using Lm.Eic.Uti.Common.YleeOOMapper;

namespace Lm.Eic.App.Business.Bmp.WorkFlow.GeneralForm
{
    /// <summary>
    /// 内部联络单管理器
    /// </summary>
    public class InternalContactFormManager
    {

        /// <summary>
        /// 获取人员邮箱信息
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public List<ItilEmailManageModel> GetWorkerMails(string department)
        {
            return ItilService.EmailManager.GetEmails(new ItilEmailManageModelDto() { SearchMode = 4, Department = department });
        }
        /// <summary>
        /// 自动创建内部联络单表单
        /// </summary>
        /// <param name="department">部门信息</param>
        /// <returns></returns>
        public string AutoCreateFormId(string department)
        {
            return GeneralFormCrudFactory.IContctFormCrud.CreateFormId(department);
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
