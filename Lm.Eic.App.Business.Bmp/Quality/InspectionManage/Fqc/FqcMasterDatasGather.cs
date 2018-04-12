using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
    public class FqcMasterDatasGather : InspectionDateGatherManageBase
    {
        /// <summary>
        /// 物料得到 MasterDatas
        /// </summary>
        /// <param name="marterialId"></param>
        /// <returns></returns>
        public List<InspectionFqcMasterModel> GetFqcMasterModeListlBy(string marterialId)
        {
            return InspectionManagerCrudFactory.FqcMasterCrud.GetFqcInspectionMasterListBy(marterialId);
        }
        /// <summary>
        ///  得到Mater列表数据
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<InspectionFqcMasterModel> GetFqcMasterOrderIdDatasBy(string orderId)
        {
            try
            {
                return InspectionManagerCrudFactory.FqcMasterCrud.GetFqcInspectionMasterModelListBy(orderId);
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception(ex.InnerException.Message);
            }
        }
        /// <summary>
        /// 存储主表信息
        /// </summary>
        /// <param name="masterModel"></param>
        /// <returns></returns>
        public OpResult storeInspectionMasterial(InspectionFqcMasterModel masterModel)
        {
            var haveStoreMasterModel = InspectionManagerCrudFactory.FqcMasterCrud.GetStroeOldModel(masterModel.OrderId, masterModel.OrderIdNumber, masterModel.MaterialId);
            ///初始化保存数据
            if (haveStoreMasterModel == null)
            {
                ///没有必要保存初化保存的信息
                masterModel.InspectionItemInspectors = masterModel.InspectionItemInspectors.Contains("StartSetValue") ? string.Empty : masterModel.InspectionItemInspectors;
                return InspectionManagerCrudFactory.FqcMasterCrud.Store(masterModel, true);
            }
            ///初始化数据
            List<string> haveFinishData = new List<string>();
            List<string> havenIspectionItemInspectors = new List<string>();
            List<string> hanveInspectionItemDetails = new List<string>();
            if (masterModel.OpPerson == "StartSetValue") return OpResult.SetSuccessResult("初始已经保存", true);
            string inspecitonItem = masterModel.InspectionItems != null ? masterModel.InspectionItems.Trim() : string.Empty;
            if (haveStoreMasterModel.InspectionItems != null && haveStoreMasterModel.InspectionItems != string.Empty)
            {
                haveFinishData = this.GetHaveFinishDatas(haveStoreMasterModel.InspectionItems);
                if (!haveFinishData.Contains(inspecitonItem) && inspecitonItem != string.Empty)
                {
                    masterModel.InspectionItems = haveStoreMasterModel.InspectionItems + "," + inspecitonItem;
                    haveFinishData.Add(inspecitonItem);
                }
                else masterModel.InspectionItems = haveStoreMasterModel.InspectionItems;
            }
            /// 最大数量
            if (masterModel.InspectionMaxNumber < haveStoreMasterModel.InspectionMaxNumber)
            {
                masterModel.InspectionMaxNumber = haveStoreMasterModel.InspectionMaxNumber;
                /// Ng数量
                masterModel.InspectionNgNumber = haveStoreMasterModel.InspectionNgNumber + masterModel.InspectionNgNumber;
            }
            if (haveStoreMasterModel.InspectionItemInspectors != null && haveStoreMasterModel.InspectionItemInspectors != string.Empty)
            {
                havenIspectionItemInspectors = this.GetHaveFinishDatas(haveStoreMasterModel.InspectionItemInspectors);
                if (!havenIspectionItemInspectors.Contains(masterModel.InspectionItemInspectors) && masterModel.InspectionItemInspectors != string.Empty)
                {
                    ///检验员
                    masterModel.InspectionItemInspectors = haveStoreMasterModel.InspectionItemInspectors + "," + masterModel.InspectionItemInspectors;
                    havenIspectionItemInspectors.Add(masterModel.InspectionItemInspectors);
                }
                else masterModel.InspectionItemInspectors = haveStoreMasterModel.InspectionItemInspectors;
            }
            // 没有必要对每项的详细数据进行收集
            if (masterModel.InspectionItemDetails != null && masterModel.InspectionItemDetails != string.Empty)
            {
                hanveInspectionItemDetails = this.GetHaveItemDetialDatas(haveStoreMasterModel.InspectionItemDetails);
                if (!hanveInspectionItemDetails.Contains(masterModel.InspectionItemDetails))
                {
                    ///具体详细数量
                    masterModel.InspectionItemDetails = haveStoreMasterModel.InspectionItemDetails + "&" + masterModel.InspectionItemDetails;
                    hanveInspectionItemDetails.Add(masterModel.InspectionItemDetails);
                }
                else masterModel.InspectionItemDetails = haveStoreMasterModel.InspectionItemDetails;
            }
            var detailDatas = InspectionManagerCrudFactory.FqcDetailCrud.GetFqcInspectionDetailDatasBy(masterModel.OrderId, masterModel.OrderIdNumber);
            if (detailDatas != null && detailDatas.Count > 0)
            {
                if (detailDatas.Count() == haveFinishData.Count)
                {
                    masterModel.InspectionStatus = "待审核";
                    masterModel.InspectionResult = (detailDatas.Count(e => e.InspectionItemResult == "NG") > 0 ? "NG" : "OK");
                }
                else masterModel.InspectionStatus = "未完成";
            }
            /// 如果 是新增 只添加一次 
            masterModel.Id_Key = haveStoreMasterModel.Id_Key;
            masterModel.OpSign = OpMode.Edit;
            return InspectionManagerCrudFactory.FqcMasterCrud.Store(masterModel, true);

        }
        /// <summary>
        /// 获取已经检验的数量
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public double GetFqcMasterHaveInspectionCountBy(string orderId)
        {
            var listdatas = GetFqcMasterOrderIdDatasBy(orderId);
            return (listdatas == null || listdatas.Count <= 0) ? 0 : listdatas.Sum(e => e.InspectionCount);
        }
    }

}
