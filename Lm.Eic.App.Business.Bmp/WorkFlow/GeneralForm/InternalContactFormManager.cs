using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.DomainModel.Bpm.WorkFlow.GeneralForm;
using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using Lm.Eic.Framework.ProductMaster.Business.Itil;

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
    }
}
