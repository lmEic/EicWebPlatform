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

    /// <summary>
    /// 物料查询结构模型
    /// </summary>
    [Serializable]
    public class ErpMaterialQueryCell
    {
        /// <summary>
        /// 关键物料BOM列表
        /// </summary>
        public List<HwCollaborationMaterialBomModel> KeyMaterialBomList { get; private set; }
        /// <summary>
        /// 产品品号列表
        /// </summary>
        public List<HwCollaborationMaterialBomModel> ProductIdList { get; private set; }
        /// <summary>
        /// 物料料号列表
        /// </summary>
        public List<HwCollaborationMaterialBomModel> MaterialIdList { get; private set; }


        public ErpMaterialQueryCell(List<HwCollaborationMaterialBaseConfigModel> bomList)
        {
            if (bomList != null)
            {
                var productModelList = bomList.FindAll(e => e.ParentMaterialId == "MaterialBaseBomInfo");
                this.ProductIdList = CreateProductIdModelList(productModelList);
                this.MaterialIdList = CreateMaterialIdModelList(productModelList, bomList);
                this.KeyMaterialBomList = CreateBomList(productModelList, bomList);
            }
            else
            {
                this.ProductIdList = new List<HwCollaborationMaterialBomModel>();
                this.KeyMaterialBomList = new List<HwCollaborationMaterialBomModel>();
                this.MaterialIdList = new List<HwCollaborationMaterialBomModel>();
            }
        }
        private List<HwCollaborationMaterialBomModel> CreateProductIdModelList(List<HwCollaborationMaterialBaseConfigModel> ProductIdModelList)
        {
            List<HwCollaborationMaterialBomModel> list = new List<HwCollaborationMaterialBomModel>();
            if (ProductIdModelList == null) return list;
            ProductIdModelList.ForEach(m => { list.Add(MapDto(m)); });
            return list;
        }
        private List<HwCollaborationMaterialBomModel> CreateMaterialIdModelList(List<HwCollaborationMaterialBaseConfigModel> productIdModelList, List<HwCollaborationMaterialBaseConfigModel> bomList)
        {
            List<HwCollaborationMaterialBomModel> list = new List<HwCollaborationMaterialBomModel>();
            if (productIdModelList == null || bomList == null) return list;
            //遍历成品料号
            productIdModelList.ForEach(m =>
            {
                //遍历物料料号
                var datas = bomList.FindAll(f => f.ParentMaterialId == m.MaterialId);
                if (datas != null)
                {
                    datas.ForEach(material =>
                    {
                        if (list.FirstOrDefault(mdl => mdl.subItemCode == material.MaterialId) == null)
                        {
                            list.Add(MapDto(material));
                        }
                    });
                }
            });
            return list;
        }
        private List<HwCollaborationMaterialBomModel> CreateBomList(List<HwCollaborationMaterialBaseConfigModel> productIdModelList, List<HwCollaborationMaterialBaseConfigModel> bomList)
        {
            List<HwCollaborationMaterialBomModel> list = new List<HwCollaborationMaterialBomModel>();
            if (productIdModelList == null || bomList == null) return list;
            //遍历成品料号
            productIdModelList.ForEach(m =>
            {
                //遍历物料料号
                var datas = bomList.FindAll(f => f.ParentMaterialId == m.MaterialId);
                if (datas != null)
                {
                    datas.ForEach(material =>
                    {
                        if (list.FirstOrDefault(mdl => mdl.vendorItemCode == material.ParentMaterialId && mdl.subItemCode == material.MaterialId) == null)
                        {
                            list.Add(MapDto(material));
                        }
                    });
                }
            });
            return list;
        }

        private HwCollaborationMaterialBomModel MapDto(HwCollaborationMaterialBaseConfigModel model)
        {
            HwCollaborationMaterialBomModel dto = new HwCollaborationMaterialBomModel()
            {
                customerItemCode = model.CustomerItemCode.Trim(),
                customerVendorCode = model.CustomerVendorCode.Trim(),
                subItemCode = model.MaterialId.Trim(),
                vendorItemCode = model.ParentMaterialId.Trim(),
                MaterialId = model.MaterialId.Trim()
            };
            return dto;
        }
    }
}
