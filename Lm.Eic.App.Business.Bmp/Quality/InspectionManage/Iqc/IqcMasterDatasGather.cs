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
            InspectionIqcMasterModel masterModel = new InspectionIqcMasterModel();
            OOMaper.Mapper<InspectionItemDataSummaryVM, InspectionIqcMasterModel>(model, masterModel);
            masterModel.InspectionItems = model.InspectionItem;
            masterModel.MaterialCount = model.MaterialInCount;
            masterModel.FinishDate = DateTime.Now.Date;
            masterModel.InspectionStatus = "未完成";
            masterModel.InspectionResult = string.Empty;
            masterModel.OpSign = OpMode.Add;
            if (IsExistOrderIdAndMaterailId(model.OrderId, model.MaterialId))
                return ChangeMasterModelStatus(masterModel);

            return InspectionManagerCrudFactory.IqcMasterCrud.Store(masterModel, true); ;
        }
        public bool IsExistOrderIdAndMaterailId(string orderId, string materialId)
        {
            return InspectionManagerCrudFactory.IqcMasterCrud.IsExistOrderIdAndMaterailId(orderId, materialId);
        }
        public InspectionIqcMasterModel GetMasterModel(string orderId, string materialId)
        {
            return InspectionManagerCrudFactory.IqcMasterCrud.GetIqcInspectionMasterDatasBy(orderId, materialId);
        }
        /// <summary>
        ///更新Master结果和状态
        /// </summary>
        /// <param name="masterModel"></param>
        /// <param name="itemSumCount"></param>
        private OpResult ChangeMasterModelStatus(InspectionIqcMasterModel masterModel)
        {
            var haveStoreMasterModel = GetMasterModel(masterModel.OrderId, masterModel.MaterialId);
            if (haveStoreMasterModel == null) return OpResult.SetErrorResult("数据不存在");
                //if (haveStoreMasterModel.InspectionStatus != "未完成") return OpResult.SetSuccessResult("已保存", true);

                string inspecitonItem = masterModel.InspectionItems.Trim();
            if (!haveStoreMasterModel.InspectionItems.Contains(inspecitonItem))
                masterModel.InspectionItems = haveStoreMasterModel.InspectionItems + "," + inspecitonItem;
            /// 如果完成了，处理待审核状态 那就判断所有的测试项是不是 Pass
            /// 测试所以项目只要有一项为 Ng 
            /// Ng数大于0 那么就要判断为NG
            var detailDatas = InspectionManagerCrudFactory.IqcDetailCrud.GetIqcInspectionDetailOrderIdModelBy(masterModel.OrderId, masterModel.MaterialId);
            if (detailDatas != null && detailDatas.Count > 0)
            {
                int itemSumCount = detailDatas.Count();
                if (itemSumCount >= GetHaveFinishDataNumber(masterModel.InspectionItems))
                {
                    masterModel.InspectionStatus = "待审核";
                }
                ///测试所以项目只要有一项为 Ng 
                masterModel.InspectionResult = (detailDatas.Count(e => e.InspectionItemResult == "NG") > 0 ? "NG" : masterModel.InspectionResult);
            }
            return InspectionManagerCrudFactory.IqcMasterCrud.Update(masterModel);
        }
    }
}
