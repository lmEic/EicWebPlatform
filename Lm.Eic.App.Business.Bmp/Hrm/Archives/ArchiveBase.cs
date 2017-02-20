using Lm.Eic.Framework.ProductMaster.Business.Config;
using Lm.Eic.Framework.ProductMaster.Model;
using System.Collections.Generic;

namespace Lm.Eic.App.Business.Bmp.Hrm.Archives
{
    /// <summary>
    /// 档案管理基类
    /// </summary>
    public abstract class ArchiveBase
    {
        /// <summary>
        /// 档案管理部门架构配置数据
        /// </summary>
        public List<ConfigDataDictionaryModel> ArchiveDepartmentConfigDatas
        {
            get
            {
                var departments = PmConfigService.DataDicManager.FindConfigDatasBy("Organization", "HrBaseInfoManage");
                return departments;
            }
        }
    }
}