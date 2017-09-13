using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.HwCollaboration.Business;
using Lm.Eic.App.HwCollaboration.Model;
using Lm.Eic.Uti.Common.YleeOOMapper;

namespace Lm.Eic.App.HwCollaboration.Business.ManPower
{
    /// <summary>
    /// 人力管理器
    /// </summary>
    public class ManPowerManager : HwCollaborationBase<ManPowerDto>
    {
        public override OpResult SynchronizeDatas(HwDataEntity entity)
        {
            return this.SynchronizeDatas(HwAccessApiUrl.ManPowerApiUrl, entity);
        }
    }
}
