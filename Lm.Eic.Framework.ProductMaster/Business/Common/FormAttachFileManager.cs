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
using System.IO;

namespace Lm.Eic.Framework.ProductMaster.Business.Common
{
    public class FormAttachFileManager
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
        /// 相同模块相同表单号的只允许上传单个附件
        /// </summary>
        /// <param name="fileModel"></param>
        /// <returns></returns>
        public OpResult StoreOnlyOneTime(FormAttachFileManageModel fileModel)
        {
            fileModel.OpSign = OpMode.Add;
            return CommonManageCurdFactory.FormAttachFileCrud.Store(fileModel);
        }
    }
}
