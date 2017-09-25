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
    /// 物料在制明细管理器
    /// </summary>
    public class MaterialMakingManager : HwCollaborationMaterialBase<MaterialMakingDto>
    {
        public MaterialMakingManager() : base(HwModuleName.MaterialMaking, HwAccessApiUrl.MaterialMakingApiUrl)
        {
        }

        public override MaterialMakingDto AutoGetDatasFromErp()
        {
            string productId = "14K1B169600M0RN";
            MaterialMakingDto dto = new MaterialMakingDto() { materialMakingList = new List<SccMaterialMakingVO>() };
            List<ErpMaterialMakingModel> datas = this.erpDbAccess.LoadMaterialMakingDatas(productId);
            if (datas != null && datas.Count > 0)
            {
                datas.ForEach(d =>
                {
                    SccMaterialMakingVO smm = new SccMaterialMakingVO()
                    {
                        vendorFactoryCode = "421072-001",
                        componentCode = d.ComponentCode.Trim(),
                        componentDescription = "BOSA Pigtail",
                        customerVendorCode = "157",
                        orderCompletedQuantity = d.OrderCompletedQuantity,
                        orderNumber = d.OrderNumber.Trim(),
                        orderPublishDateStr = d.OrderPublishDateStr.ToFormatDate(),
                        orderQuantity = d.OrderQuantity,
                        orderStatus = d.OrderStatus.ToDiscriptionOrderStatus()
                    };
                    dto.materialMakingList.Add(smm);
                });
            }
            return dto;
        }
    }
}
