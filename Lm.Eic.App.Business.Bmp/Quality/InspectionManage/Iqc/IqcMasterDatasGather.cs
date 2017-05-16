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

        public OpResult StoreInspectionIqcMasterModelForm(InspectionItemDataSummaryVM model)
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
                InspectionStatus = "未完成",
                InspectionResult = model.InspectionItemResult,
                OpSign = OpMode.Add
            };
            if (InspectionManagerCrudFactory.IqcMasterCrud.IsExistOrderIdAndMaterailId(model.OrderId, model.MaterialId))
            {
                MasterModel = InspectionManagerCrudFactory.IqcMasterCrud.GetIqcInspectionMasterDatasBy(model.OrderId, model.MaterialId);
                if (!MasterModel.InspectionItems.Contains(model.InspectionItem))
                    MasterModel.InspectionItems += "," + model.InspectionItem;
                if (model.InspectionItemSumCount == GetHaveFinishDataNumber(MasterModel.InspectionItems))
                {
                    MasterModel.InspectionStatus = "待审核";
                    /// 如果完成了，处理待审核状态 那就判断所有的测试项是不是 Pass
                    /// 测试所以项目只要有一项为 Ng 
                    /// Ng数大于0 那么就要判断为NG
                    var detailDatas = InspectionManagerCrudFactory.IqcDetailCrud.GetIqcInspectionDetailOrderIdModelBy(model.OrderId, model.MachineId);
                    if (detailDatas != null && detailDatas.Count > 0)
                    {
                        int DetailNgCount = detailDatas.Count(e => e.InspectionItemResult == "NG");
                        ///测试所以项目只要有一项为 Ng 
                        if (DetailNgCount >= 0)
                            MasterModel.InspectionResult = "FAIL";
                    }
                }
                MasterModel.OpSign = OpMode.Edit;
            }
            return InspectionManagerCrudFactory.IqcMasterCrud.Store(MasterModel, true); ;
        }
    }
}
