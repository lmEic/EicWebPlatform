using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.HwCollaboration.Business;
using Lm.Eic.App.HwCollaboration.Model;
using Lm.Eic.App.HwCollaboration.Model.LmErp;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.App.HwCollaboration.DbAccess;

namespace Lm.Eic.App.HwCollaboration.Business.MaterialManage
{
    public class MaterialBaseDtoSender : HwCollaborationBase<VendorItemRelationDto>
    {
        public MaterialBaseDtoSender() : base(HwModuleName.MaterialBaseInfo, HwAccessApiUrl.MaterialBaseInfoApiUrl)
        {
        }

        public HwAccessOpResult SendDto(VendorItemRelationDto dto)
        {
            return this.AccessApi(this.apiUrl, dto).AsHwAccessApiResult().AsAccessOpResult(this.moduleName);
        }
    }
    public class MaterialBomDtoSender : HwCollaborationBase<KeyMaterialDto>
    {
        public MaterialBomDtoSender() : base(HwModuleName.MaterialKeyBom, HwAccessApiUrl.MaterialKeyBomApiUrl)
        {
        }

        public HwAccessOpResult SendDto(KeyMaterialDto dto)
        {
            return this.AccessApi(this.apiUrl, dto).AsHwAccessApiResult().AsAccessOpResult(this.moduleName);
        }
    }
    public class MaterialBaseBomManager
    {
        private HwMaterialBaseConfigDb materialBaseConfigDb = null;
        private MaterialBaseDtoSender baseDtoSender = null;
        private MaterialBomDtoSender bomDtoSender = null;

        private List<HwCollaborationMaterialBaseConfigModel> materialBaseDictionary = null;
        /// <summary>
        /// 物料基础数据字典
        /// </summary>
        public List<HwCollaborationMaterialBaseConfigModel> MaterialBaseDictionary
        {
            get
            {
                if (materialBaseDictionary == null)
                    materialBaseDictionary = this.materialBaseConfigDb.GetAll();
                return materialBaseDictionary;
            }
        }
        public MaterialBaseBomManager()
        {
            this.materialBaseConfigDb = new DbAccess.HwMaterialBaseConfigDb();
            this.baseDtoSender = new MaterialBaseDtoSender();
            this.bomDtoSender = new MaterialBomDtoSender();
        }

        #region store method
        private List<HwAccessOpResult> SendDtoToHw(List<HwCollaborationMaterialBaseConfigModel> entities)
        {
            List<HwAccessOpResult> accessOpResults = new List<HwAccessOpResult>();
            var baseDto = CreateMaterialBaseDto(entities);
            accessOpResults.Add(this.baseDtoSender.SendDto(baseDto));
            var bomDto = CreateMaterialBomDto(entities);
            accessOpResults.Add(this.bomDtoSender.SendDto(bomDto));
            return accessOpResults;
        }
        public List<HwAccessOpResult> Store(HwCollaborationMaterialBaseConfigModel entity)
        {
            entity.OpDate = DateTime.Now.ToDate();
            entity.OpTime = DateTime.Now.ToDateTime();
            var opresult = this.materialBaseConfigDb.Store(entity);
            this.RefreshCache(opresult);
            List<HwAccessOpResult> accessOpResults = new List<HwAccessOpResult>() {
                    HwAccessOpResult.SetResult("基础物料信息数据存储", opresult.Message, opresult.Result)
                };
            return accessOpResults;
        }
        private VendorItemRelationDto CreateMaterialBaseDto(List<HwCollaborationMaterialBaseConfigModel> entities)
        {
            VendorItemRelationDto dto = new VendorItemRelationDto() { vendorItemList = new List<SccVendorItemRelationVO>() };
            entities.ForEach(entity =>
            {
                SccVendorItemRelationVO vo = new SccVendorItemRelationVO()
                {
                    customerItemCode = entity.CustomerItemCode.Trim(),
                    customerProductModel = entity.CustomerProductModel,
                    customerVendorCode = entity.CustomerVendorCode.Trim(),
                    goodPercent = entity.GoodPercent,
                    inventoryType = entity.InventoryType,
                    itemCategory = entity.ItemCategory,
                    leadTime = entity.LeadTime,
                    lifeCycleStatus = entity.LifeCycleStatus,
                    unitOfMeasure = entity.UnitOfMeasure,
                    vendorItemCode = entity.MaterialId.Trim(),
                    vendorItemDesc = entity.VendorItemDesc.Trim(),
                    vendorProductModel = entity.VendorProductModel
                };
                if (dto.vendorItemList.FirstOrDefault(e => e.vendorItemCode == entity.MaterialId.Trim()) == null)
                    dto.vendorItemList.Add(vo);
            });
            return dto;
        }
        private KeyMaterialDto CreateMaterialBomDto(List<HwCollaborationMaterialBaseConfigModel> entities)
        {
            KeyMaterialDto dto = new KeyMaterialDto() { keyMaterialList = new List<SccKeyMaterialVO>() };
            entities.ForEach(entity =>
            {
                SccKeyMaterialVO vo = new SccKeyMaterialVO()
                {
                    baseUsedQuantity = entity.Quantity.ToString().ToInt(),
                    standardQuantity = entity.Quantity.ToString().ToInt(),
                    subItemCode = entity.MaterialId,
                    vendorItemCode = entity.ParentMaterialId,
                    substituteGroup = entity.SubstituteGroup
                };
                if (entity.ParentMaterialId.Trim() != "MaterialBaseBomInfo")
                {
                    dto.keyMaterialList.Add(vo);
                }
            });
            return dto;
        }

        /// <summary>
        /// 刷新缓存
        /// </summary>
        private void RefreshCache(OpResult result)
        {
            if (result.Result)
                this.materialBaseDictionary = null;
        }
        /// <summary>
        /// 一键同步数据
        /// </summary>
        /// <returns></returns>
        public List<HwAccessOpResult> AutoSynchironizeData()
        {
            this.RefreshCache(OpResult.SetSuccessResult("OK"));
            var datas = CreateBomCellList();
            if (datas == null || datas.Count == 0)
            {
                List<HwAccessOpResult> accessOpResults = new List<HwAccessOpResult>() {
                    HwAccessOpResult.SetResult("物料基础信息模块", "没有要同步的数据",false)
                };
                return accessOpResults;
            }
            return SendDtoToHw(datas);
        }
        #endregion


        #region query method
        private List<HwCollaborationMaterialBaseConfigModel> CreateBomCellList()
        {
            List<HwCollaborationMaterialBaseConfigModel> configDatas = new List<HwCollaborationMaterialBaseConfigModel>();
            var dftDatas = this.MaterialBaseDictionary.FindAll(e => e.ParentMaterialId == "MaterialBaseBomInfo");
            if (dftDatas == null || dftDatas.Count == 0) return configDatas;
            configDatas.AddRange(dftDatas);
            dftDatas.ForEach(f =>
            {
                var dataList = this.MaterialBaseDictionary.FindAll(e => e.ParentMaterialId == f.MaterialId);
                if (dataList != null && dataList.Count > 0)
                    configDatas.AddRange(dataList);
            });
            return configDatas;
        }
        /// <summary>
        /// 获取所有的BOM配置数据
        /// </summary>
        /// <returns></returns>
        public List<HwCollaborationMaterialBaseConfigModel> GetAllBomConfigDatas()
        {
            List<HwCollaborationMaterialBomModel> rtnDatas = new List<HwCollaborationMaterialBomModel>();
            return CreateBomCellList();
        }
        #endregion
    }
}
