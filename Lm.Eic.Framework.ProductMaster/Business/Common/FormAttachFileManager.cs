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
    public class FormAttachFileManager
    {
        /// <summary>
        /// 相同模块相同表单号的只允许上传单个附件
        /// 
        /// </summary>
        /// <param name="fileModel"></param>
        /// <returns></returns>
        public OpResult UploadSingleAttachFile(FormAttachFileManageModel fileModel)
        {
            return CommonManageCurdFactory.FormAttachFileCrud.Store(fileModel);
        }
    }
}
