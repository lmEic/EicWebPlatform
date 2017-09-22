using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lm.Eic.App.HwCollaboration.Business.MaterialManage
{
    /// <summary>
    /// 物料管理器
    /// </summary>
    public class MaterialManager
    {
        #region property
        /// <summary>
        /// 库存明细管理
        /// </summary>
        public MaterialInventoryManager InventoryManager
        {
            get { return OBulider.BuildInstance<MaterialInventoryManager>(); }
        }

        /// <summary>
        /// 在制管理
        /// </summary>
        public MaterialMakingManager MakingManager
        {
            get { return OBulider.BuildInstance<MaterialMakingManager>(); }
        }

        /// <summary>
        /// 发料管理
        /// </summary>
        public MaterialShipmentManager ShipmentManager
        {
            get { return OBulider.BuildInstance<MaterialShipmentManager>(); }
        }

        /// <summary>
        /// 基础信息设置
        /// </summary>
        public MaterialBaseInfoSettor BaseInfoSettor
        {
            get { return OBulider.BuildInstance<MaterialBaseInfoSettor>(); }
        }

        /// <summary>
        /// 关键物料BOM管理
        /// </summary>
        public MaterialKeyBomManager KeyBomManager
        {
            get { return OBulider.BuildInstance<MaterialKeyBomManager>(); }
        }
        #endregion
    }
}
