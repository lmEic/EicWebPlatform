using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Business.Common
{
    /// <summary>
    /// 公共表单编号管理器
    /// </summary>
    public class FormIdManager
    {
        /// <summary>
        /// 创建表单编号数据
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        public FormIdComposeModel CreateFormIdData(string moduleName, string department)
        {
            return CommonManageCurdFactory.FormIdCrud.CreateFormIdModel(department, moduleName);
        }
    }
}
