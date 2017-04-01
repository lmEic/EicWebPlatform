using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{



    public class IqcMasterDatasGather : InspectionDateGatherManageBase
    {

        public OpResult StoreInspectionIqcMasterModelForm(InspectionItemDataSummaryLabelModel model)
        {
            InspectionIqcMasterModel MasterModel = new InspectionIqcMasterModel()
            {
                OrderId = model.OrderId,
                MaterialId = model.MaterialId,
                MaterialName = model.MaterialName,
                MaterialSpec = model.MaterialSpec,
                MaterialDrawId = model.MaterialDrawId,
                MaterialSupplier = model.MaterialSupplier,
                MaterialCount = model.MaterialInCount,
                MaterialInDate = model.MaterialInDate,
                InspectionMode = model.InspectionMode,
                InspectionItems = model.InspectionItem,
                FinishDate = DateTime.Now.Date,
                InspectionStatus = "待审核",
                InspectionResult = "未完成",
                OpSign = "add"
            };
            if (InspectionManagerCrudFactory.IqcMasterCrud.IsExistOrderIdAndMaterailId(model.OrderId, model.MaterialId))
            {
                MasterModel = InspectionManagerCrudFactory.IqcMasterCrud.GetIqcInspectionMasterModelListBy(model.OrderId, model.MaterialId);
                if (!InspectionManagerCrudFactory.IqcMasterCrud.IsExistOrderIdAndMaterailId(model.OrderId, model.MaterialId, model.InspectionItem))
                    MasterModel.InspectionItems += "," + model.InspectionItem;
                if (model.InspectionItemSumCount == GetHaveFinishDataNumber(MasterModel.InspectionItems))
                {
                    MasterModel.InspectionResult = "已完成";
                }
                MasterModel.OpSign = "edit";
            }
            return InspectionManagerCrudFactory.IqcMasterCrud.Store(MasterModel, true); ;
        }
    }
}
