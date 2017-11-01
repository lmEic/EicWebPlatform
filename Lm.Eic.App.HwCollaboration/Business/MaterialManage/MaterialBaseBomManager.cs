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

        public OpResult SendDto(VendorItemRelationDto dto)
        {
            return this.AccessApi(this.apiUrl, dto).AsHwAccessApiResult().AsOpResult();
            //return OpResult.SetSuccessResult("OK");
        }
    }
    public class MaterialBomDtoSender : HwCollaborationBase<KeyMaterialDto>
    {
        public MaterialBomDtoSender() : base(HwModuleName.MaterialKeyBom, HwAccessApiUrl.MaterialKeyBomApiUrl)
        {
        }

        public OpResult SendDto(KeyMaterialDto dto)
        {
            return this.AccessApi(this.apiUrl, dto).AsHwAccessApiResult().AsOpResult();
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
        private OpResult SendDtoToHw(HwCollaborationMaterialBaseConfigModel entity)
        {
            var baseDto = CreateMaterialBaseDto(entity);
            var opResult = this.baseDtoSender.SendDto(baseDto);
            if (!opResult.Result) return opResult;
            if (entity.ParentMaterialId.Trim() != "MaterialBaseBomInfo")
            {
                var bomDto = CreateMaterialBomDto(entity);
                opResult = this.bomDtoSender.SendDto(bomDto);
                if (!opResult.Result) return opResult;
            }
            return OpResult.SetSuccessResult("OK");
        }
        public OpResult Store(HwCollaborationMaterialBaseConfigModel entity)
        {
            entity.OpDate = DateTime.Now.ToDate();
            entity.OpTime = DateTime.Now.ToDateTime();
            var opresult = SendDtoToHw(entity);
            if (opresult.Result)
                opresult = this.materialBaseConfigDb.Store(entity);
            return opresult;
        }
        private VendorItemRelationDto CreateMaterialBaseDto(HwCollaborationMaterialBaseConfigModel entity)
        {
            VendorItemRelationDto dto = new VendorItemRelationDto() { vendorItemList = new List<SccVendorItemRelationVO>() };
            SccVendorItemRelationVO vo = new SccVendorItemRelationVO()
            {
                customerItemCode = entity.CustomerItemCode,
                customerProductModel = entity.CustomerProductModel,
                customerVendorCode = entity.CustomerVendorCode,
                goodPercent = entity.GoodPercent,
                inventoryType = entity.InventoryType,
                itemCategory = entity.ItemCategory,
                leadTime = entity.LeadTime,
                lifeCycleStatus = entity.LifeCycleStatus,
                unitOfMeasure = entity.UnitOfMeasure,
                vendorItemCode = entity.MaterialId,
                vendorItemDesc = entity.VendorItemDesc,
                vendorProductModel = entity.VendorProductModel
            };
            dto.vendorItemList.Add(vo);
            return dto;
        }
        private KeyMaterialDto CreateMaterialBomDto(HwCollaborationMaterialBaseConfigModel entity)
        {
            KeyMaterialDto dto = new KeyMaterialDto() { keyMaterialList = new List<SccKeyMaterialVO>() };
            SccKeyMaterialVO vo = new SccKeyMaterialVO()
            {
                baseUsedQuantity = entity.Quantity.ToString().ToInt(),
                standardQuantity = entity.Quantity.ToString().ToInt(),
                subItemCode = entity.MaterialId,
                vendorItemCode = entity.ParentMaterialId,
                substituteGroup = entity.SubstituteGroup
            };
            dto.keyMaterialList.Add(vo);
            return dto;
        }


        /// <summary>
        /// 一键同步数据
        /// </summary>
        /// <returns></returns>
        public OpResult AutoSynchironizeData()
        {
            var datas = CreateBomCellList();
            bool success = true;
            if (datas == null || datas.Count == 0)
                return OpResult.SetErrorResult("没有要同步的数据");
            datas.ForEach(d =>
            {
                success = SendDtoToHw(d).Result && success;
            });
            if (success)
                return OpResult.SetSuccessResult("向华为平台发送数据成功！");
            else
                return OpResult.SetSuccessResult("向华为平台发送数据失败！");
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
        public List<SccKeyMaterialVO> GetAllBomConfigDatas()
        {
            List<SccKeyMaterialVO> rtnDatas = new List<Model.SccKeyMaterialVO>();
            var configDatas = CreateBomCellList();
            if (configDatas != null && configDatas.Count > 0)
            {
                SccKeyMaterialVO vo = null;
                configDatas.ForEach(m =>
                {
                    vo = new SccKeyMaterialVO()
                    {
                        vendorItemCode = m.ParentMaterialId,
                        subItemCode = m.MaterialId
                    };
                    rtnDatas.Add(vo);
                });
            }
            return rtnDatas;
        }
        #endregion
    }
}
