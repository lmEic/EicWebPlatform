using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.HwCollaboration.Business;
using Lm.Eic.App.HwCollaboration.Model;
using Lm.Eic.App.HwCollaboration.Model.LmErp;
using Lm.Eic.Uti.Common.YleeOOMapper;

namespace Lm.Eic.App.HwCollaboration.Business.MaterialManage
{
    /// <summary>
    /// 物料基础信息设置器
    /// </summary>
    public class MaterialBaseInfoSettor : HwCollaborationMaterialConfigBase<VendorItemRelationDto>
    {
        public MaterialBaseInfoSettor() : base(HwModuleName.MaterialBaseInfo, HwAccessApiUrl.MaterialBaseInfoApiUrl)
        {
        }
    }

    /// <summary>
    /// 关键物料BOM信息管理器
    /// </summary>
    public class MaterialKeyBomManager : HwCollaborationBase<KeyMaterialDto>
    {
        public MaterialKeyBomManager() : base(HwModuleName.MaterialKeyBom, HwAccessApiUrl.MaterialKeyBomApiUrl)
        {
        }
    }
}
