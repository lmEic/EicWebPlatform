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
    /// 物料发料明细管理器
    /// </summary>
    public class MaterialShipmentManager : HwCollaborationMaterialBase<MaterialShipmentDto>
    {
        public MaterialShipmentManager() : base(HwModuleName.MaterialShipment, HwAccessApiUrl.MaterialShipmentApiUrl)
        {
        }

        public override MaterialShipmentDto AutoGetDatasFromErp(ErpMaterialQueryCell materialQueryCell)
        {
            MaterialShipmentDto dto = new MaterialShipmentDto() { materialShipmentList = new List<SccMaterialShipmentVO>() };
            if (materialQueryCell == null) return dto;
            materialQueryCell.KeyMaterialBomList.ForEach(materialBom =>
            {
                List<ErpMaterialShipmentModel> datas = this.erpDbAccess.LoadMaterialShipmentDatas(materialBom);
                if (datas != null && datas.Count > 0)
                {
                    datas.ForEach(d =>
                    {
                        SccMaterialShipmentVO sms = new SccMaterialShipmentVO()
                        {
                            vendorFactoryCode = "421072-001",
                            customerVendorCode = "157",
                            bomUsage = d.BomUsage,
                            itemCode = d.ItemCode,
                            orderNumber = d.OrderNumber,
                            shippedQuantity = d.ShippedQuantity,
                            shouldShipQuantity = d.ShouldShipQuantity,
                            substituteGroup = ""
                        };
                        dto.materialShipmentList.Add(sms);
                    });
                }
            });
            return dto;
        }

        protected override bool CanSendDto(MaterialShipmentDto dto)
        {
            return dto.materialShipmentList != null && dto.materialShipmentList.Count > 0;
        }

        protected override MaterialShipmentDto HandleDto(MaterialShipmentDto dto)
        {
            List<SccMaterialShipmentVO> dataList = new List<SccMaterialShipmentVO>();
            if (dto != null && dto.materialShipmentList != null && dto.materialShipmentList.Count > 0)
            {
                dto.materialShipmentList.ForEach(m =>
                {
                    SccMaterialShipmentVO vo = new SccMaterialShipmentVO()
                    {
                        bomUsage = m.bomUsage,
                        customerVendorCode = m.customerVendorCode.Trim(),
                        itemCode = m.itemCode.Trim(),
                        orderNumber = m.orderNumber.Trim(),
                        shippedQuantity = m.shippedQuantity,
                        shouldShipQuantity = m.shouldShipQuantity,
                        substituteGroup = m.substituteGroup,
                        vendorFactoryCode = m.vendorFactoryCode.Trim()
                    };
                    dataList.Add(vo);
                });
                dto.materialShipmentList = dataList;
            }
            return dto;
        }
    }
}
