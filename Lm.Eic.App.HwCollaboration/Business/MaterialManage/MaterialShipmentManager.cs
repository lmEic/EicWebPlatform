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
    /// 物料发料明细管理器
    /// </summary>
    public class MaterialShipmentManager : HwCollaborationBase<MaterialShipmentDto>
    {
        public MaterialShipmentManager() : base(HwModuleName.MaterialShipment, HwAccessApiUrl.MaterialShipmentApiUrl)
        {
        }
    }
}
