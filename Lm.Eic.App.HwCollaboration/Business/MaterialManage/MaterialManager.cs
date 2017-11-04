using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.HwCollaboration.Model;
using Lm.Eic.Uti.Common.YleeOOMapper;

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
        public MaterialBaseBomManager BaseBomManager
        {
            get { return OBulider.BuildInstance<MaterialBaseBomManager>(); }
        }
        public PurchaseManager PurchaseManager
        {
            get { return OBulider.BuildInstance<PurchaseManager>(); }
        }
        #endregion


        #region method
        public MaterialComposeDto AutoLoadDataFromErp()
        {
            MaterialComposeDto dto = new MaterialComposeDto();
            var bomConfigDatas = new ErpMaterialQueryCell(this.BaseBomManager.GetAllBomConfigDatas());
            dto.InvertoryDto = this.InventoryManager.AutoGetDatasFromErp(bomConfigDatas);
            dto.MakingDto = this.MakingManager.AutoGetDatasFromErp(bomConfigDatas);
            dto.ShippmentDto = this.ShipmentManager.AutoGetDatasFromErp(bomConfigDatas);
            dto.PurchaseDto = this.PurchaseManager.AutoGetDatasFromErp(bomConfigDatas);
            return dto;
        }

        public List<HwAccessOpResult> SaveMaterialDetail(MaterialComposeEntity entity)
        {
            List<HwAccessOpResult> opResultList = new List<Model.HwAccessOpResult>() {
                this.InventoryManager.SynchronizeDatas(entity.InvertoryEntity),
                this.MakingManager.SynchronizeDatas(entity.MakingEntity),
                this.ShipmentManager.SynchronizeDatas(entity.ShippmentEntity),
                this.PurchaseManager.SynchronizeDatas(entity.PurchaseEntity)
            };
            return opResultList;
        }
        #endregion
    }
}
