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
    /// 库存明细管理器
    /// </summary>
    public class MaterialInventoryManager : HwCollaborationMaterialBase<FactoryInventoryDto>
    {
        public MaterialInventoryManager() : base(HwModuleName.MaterialInventory, HwAccessApiUrl.FactoryInventoryApiUrl)
        {

        }

        public override FactoryInventoryDto AutoGetDatasFromErp(ErpMaterialQueryCell materialQueryCell)
        {
            //string materialId = "349213C0350P0RT";//物料料号
            FactoryInventoryDto dto = new FactoryInventoryDto() { factoryInventoryList = new List<SccFactoryInventory>() };
            if (materialQueryCell == null) return dto;
            materialQueryCell.MaterialIdList.ForEach(materialId =>
            {
                List<ErpMaterialInventoryModel> datas = this.erpDbAccess.LoadMeterialInventoryDatas(materialId);
                if (datas != null && datas.Count > 0)
                {
                    datas.ForEach(d =>
                    {
                        SccFactoryInventory sfi = new SccFactoryInventory()
                        {
                            customerCode = "157",
                            vendorFactoryCode = "421072-001",
                            goodQuantity = d.GoodQuantity,
                            stockTime = d.StockTime.ToFormatDate(),
                            vendorItemCode = d.MaterialId.Trim(),
                            vendorLocation = "",
                            vendorStock = d.VendorStock.Trim(),
                            vendorItemRevision = "",
                        };
                        dto.factoryInventoryList.Add(sfi);
                    });
                }
            });
            return dto;
        }

        protected override FactoryInventoryDto HandleDto(FactoryInventoryDto dto)
        {
            List<SccFactoryInventory> dataList = new List<SccFactoryInventory>();
            if (dto != null && dto.factoryInventoryList != null && dto.factoryInventoryList.Count > 0)
            {
                dto.factoryInventoryList.ForEach(m =>
                {
                    SccFactoryInventory vo = new SccFactoryInventory()
                    {
                        customerCode = m.customerCode.Trim(),
                        faultQty = m.faultQty,
                        goodQuantity = m.goodQuantity,
                        inspectQty = m.inspectQty,
                        stockTime = m.stockTime,
                        vendorFactoryCode = m.vendorFactoryCode.Trim(),
                        vendorItemCode = m.vendorItemCode.Trim(),
                        vendorItemRevision = m.vendorItemRevision,
                        vendorLocation = m.vendorLocation,
                        vendorStock = m.vendorStock
                    };
                    dataList.Add(vo);
                });
                dto.factoryInventoryList = dataList;
            }
            return dto;
        }
    }
}
