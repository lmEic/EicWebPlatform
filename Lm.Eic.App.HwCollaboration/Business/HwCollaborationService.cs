using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.HwCollaboration.Business.ManPower;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.App.HwCollaboration.Business.MaterialManage;

namespace Lm.Eic.App.HwCollaboration.Business
{
    /// <summary>
    /// 华为协同服务接口
    /// </summary>
    public class HwCollaborationService
    {
        /// <summary>
        /// 人力管理器
        /// </summary>
        public static ManPowerManager ManPowerManager
        {
            get { return OBulider.BuildInstance<ManPowerManager>(); }
        }

        /// <summary>
        /// 库存明细管理
        /// </summary>
        public static MaterialInventoryManager InventoryManager
        {
            get { return OBulider.BuildInstance<MaterialInventoryManager>(); }
        }

        /// <summary>
        /// 在制管理
        /// </summary>
        public static MaterialMakingManager MakingManager
        {
            get { return OBulider.BuildInstance<MaterialMakingManager>(); }
        }

        /// <summary>
        /// 发料管理
        /// </summary>
        public static MaterialShipmentManager ShipmentManager
        {
            get { return OBulider.BuildInstance<MaterialShipmentManager>(); }
        }
    }
}
