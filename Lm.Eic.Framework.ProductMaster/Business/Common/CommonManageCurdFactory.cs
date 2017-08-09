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
    /// <summary>
    /// 公共数据管理数据操作工厂
    /// </summary>
    internal class CommonManageCurdFactory
    {
        /// <summary>
        /// 表单编号管理Crud数据操作助手
        /// </summary>
        internal static FormIdManageCrud FormIdCrud
        {
            get { return OBulider.BuildInstance<FormIdManageCrud>(); }
        }
    }


}
