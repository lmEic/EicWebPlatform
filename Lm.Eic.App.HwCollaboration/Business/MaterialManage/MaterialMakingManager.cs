using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.HwCollaboration.Business;
using Lm.Eic.App.HwCollaboration.Model;
using Lm.Eic.Uti.Common.YleeOOMapper;

namespace Lm.Eic.App.HwCollaboration.Business.MaterialManage
{
    /// <summary>
    /// 物料在制明细管理器
    /// </summary>
    public class MaterialMakingManager : HwCollaborationBase<MaterialMakingDto>
    {
        public MaterialMakingManager() : base(HwModuleName.MaterialMaking, HwAccessApiUrl.MaterialMakingApiUrl)
        {
        }
    }
}
