using Lm.Eic.Framework.ProductMaster.Business.Itil;
using Lm.Eic.Framework.ProductMaster.Model.CommonManage;
using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Framework.ProductMaster.Business.Common;

namespace Lm.Eic.App.Business.Bmp.WorkFlow
{
    /// <summary>
    /// 电子表单基类
    /// </summary>
    public class ElectronicFormBase
    {
        #region module handler property
        /// <summary>
        /// 表单附件处理器
        /// </summary>
        public FormAttachFileHandler AttachFileHandler
        {
            get { return OBulider.BuildInstance<FormAttachFileHandler>(); }
        }
        #endregion
        /// <summary>
        /// 表单模块名称
        /// </summary>
        protected string FormModuleName { get; private set; }
        public ElectronicFormBase(string formModuleName)
        {
            this.FormModuleName = formModuleName;
        }

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
        /// 存储表单附件数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public OpResult StoreFormAttachFileData(FormAttachFileManageModel entity)
        {

            return OpResult.SetSuccessResult("", true);
        }

    }

    /// <summary>
    /// 表单附件处理句柄
    /// </summary>
    public class FormAttachFileHandler
    {
        /// <summary>
        /// 给表单文件命名
        /// 命名规则：模块名称_表单号+文件扩展名
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="formId"></param>
        /// <returns></returns>
        public string SetAttachFileName(string moduleName, string formId)
        {
            return string.Format("{0}_{1}", moduleName, formId);
        }
        /// <summary>
        /// 存储表单附件数据
        /// 相同模块相同表单号只允许存储一次
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public OpResult StoreOnlyOneTime(FormAttachFileManageModel dto)
        {
            return CommonService.FormAttachFileManager.StoreOnlyOneTime(dto);
        }
    }
}
