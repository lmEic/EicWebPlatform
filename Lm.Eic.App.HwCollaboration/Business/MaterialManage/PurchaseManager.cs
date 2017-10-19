using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.HwCollaboration.Business;
using Lm.Eic.App.HwCollaboration.Model;
using Lm.Eic.App.HwCollaboration.Model.LmErp;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.App.HwCollaboration.Business.MaterialManage;

namespace Lm.Eic.App.HwCollaboration.Business.MaterialManage
{
    public class PurchaseManager : HwCollaborationMaterialBase<PurchaseOnWayDto>
    {
        public PurchaseManager() : base(HwModuleName.PurchaseOnWay, HwAccessApiUrl.PurchaseOnWayApiUrl)
        {

        }

        public override PurchaseOnWayDto AutoGetDatasFromErp(ErpMaterialQueryCell materialQueryCell)
        {
            //string materialId = "349213C0350P0RT";//物料料号
            PurchaseOnWayDto dto = new PurchaseOnWayDto() { sccOpenPOList = new List<SccOpenPOVO>() };
            if (materialQueryCell == null) return dto;
            materialQueryCell.MaterialIdList.ForEach(materialId =>
            {
                List<ErpPurchaseOnWayModel> datas = this.erpDbAccess.LoadPurchaseOnWayDatas(materialId);
                if (datas != null && datas.Count > 0)
                {
                    datas.ForEach(d =>
                    {
                        SccOpenPOVO sop = new SccOpenPOVO()
                        {
                            customerVendorCode = "157",
                            vdFactoryCode = "421072-001",
                            businessMode = d.BusinessMode,
                            poPublishDateStr = d.PoPublishDateStr.ToFormatDate(),
                            componentVendorCode = d.ComponentVendorCode,
                            componentVendorName = d.ComponentVendorName,
                            demandArrivalDateStr = d.DemandArrivalDateStr.ToFormatDate(),
                            itemCode = d.ItemCode,
                            openPoQuantity = d.OpenPoQuantity,
                            poNumber = d.PoNumber,
                            promisedDeliveryDateStr = d.PromisedDeliveryDateStr.ToFormatDate()
                        };
                        dto.sccOpenPOList.Add(sop);
                    });
                }
            });
            return dto;
        }
    }
}
