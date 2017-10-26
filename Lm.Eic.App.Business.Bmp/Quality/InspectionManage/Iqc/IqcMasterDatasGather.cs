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
        /// <summary>
        /// 存储IQC数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreInspectionIqcMasterModelForm(InspectionItemDataSummaryVM model)
        {
            InspectionIqcMasterModel masterModel = new InspectionIqcMasterModel();
            OOMaper.Mapper<InspectionItemDataSummaryVM, InspectionIqcMasterModel>(model, masterModel);
            masterModel.InspectionItems = model.InspectionItem;
            masterModel.MaterialCount = model.MaterialInCount;
            masterModel.FinishDate = DateTime.Now.Date;
            masterModel.InspectionStatus = "未完成";
            masterModel.InspectionResult = "未完成";
            masterModel.OpSign = OpMode.Add;
            if (IsExistOrderIdAndMaterailId(model.OrderId, model.MaterialId))
                return ChangeMasterModelStatus(masterModel);

            return InspectionManagerCrudFactory.IqcMasterCrud.Store(masterModel, true); ;
        }


        /// <summary>
        ///更新Master结果和状态
        /// </summary>
        /// <param name="masterModel"></param>
        /// <param name="itemSumCount"></param>
        private OpResult ChangeMasterModelStatus(InspectionIqcMasterModel masterModel)
        {
            var haveStoreMasterModel = GetIqcMasterModel(masterModel.OrderId, masterModel.MaterialId);
            if (haveStoreMasterModel == null) return OpResult.SetErrorResult("数据不存在");
            string inspecitonItem = masterModel.InspectionItems.Trim();
            List<string> haveFinishData = new List<string>();
            if (haveStoreMasterModel.InspectionItems != null && haveStoreMasterModel.InspectionItems != string.Empty)
            { haveFinishData = this.GetHaveFinishDatas(haveStoreMasterModel.InspectionItems); }
            if (!haveFinishData.Contains(inspecitonItem) && inspecitonItem != string.Empty)
            {
                haveStoreMasterModel.InspectionItems = haveStoreMasterModel.InspectionItems + "," + inspecitonItem;
                haveFinishData.Add(inspecitonItem);
            }
            /// 如果完成了，处理待审核状态 那就判断所有的测试项是不是 Pass
            /// 测试所以项目只要有一项为 Ng 
            /// Ng数大于0 那么就要判断为NG
            var detailDatas = InspectionManagerCrudFactory.IqcDetailCrud.GetIqcInspectionDetailOrderIdModelBy(masterModel.OrderId, masterModel.MaterialId);
            if (detailDatas != null && detailDatas.Count > 0)
            {
                if (detailDatas.Count() <= haveFinishData.Count)
                {
                    haveStoreMasterModel.InspectionStatus = "待审核";
                    haveStoreMasterModel.InspectionResult = (detailDatas.Count(e => e.InspectionItemResult == "NG") > 0 ? "NG" : "OK");
                }
                else
                {
                    haveStoreMasterModel.InspectionStatus = "未完成";
                    haveStoreMasterModel.InspectionResult = "未完成";
                }
                ///测试所以项目只要有一项为 Ng 

            }
            return InspectionManagerCrudFactory.IqcMasterCrud.Update(haveStoreMasterModel);
        }
        /// <summary>
        /// 订单物料是否存在
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public bool IsExistOrderIdAndMaterailId(string orderId, string materialId)
        {
            return InspectionManagerCrudFactory.IqcMasterCrud.IsExistOrderIdAndMaterailId(orderId, materialId);
        }
        /// <summary>
        /// 得到主表信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public InspectionIqcMasterModel GetIqcMasterModel(string orderId, string materialId)
        {
            return GetIqcMasterContainDatasBy(orderId).FirstOrDefault(e => e.MaterialId == materialId);
        }
        public List<InspectionIqcMasterModel> GetIqcMasterDatasBy(DateTime startTime, DateTime endTime)
        {
            var datas = InspectionManagerCrudFactory.IqcMasterCrud.GetIqcMasterDatasBy(startTime, endTime);
            return datas == null ? new List<InspectionIqcMasterModel>() : datas;
        }
        /// <summary>
        /// 得到主表数据
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<InspectionIqcMasterModel> GetIqcMasterContainDatasBy(string orderId)
        {
            return InspectionManagerCrudFactory.IqcMasterCrud.GetIqcMasterContainDatasBy(orderId);
        }
    }
}
