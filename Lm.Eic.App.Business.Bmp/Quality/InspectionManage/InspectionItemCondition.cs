using Lm.Eic.App.DomainModel.Bpm.Quanity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
    public class InspectionItemCondition
    {
        /// <summary>
        /// 加载所有的测试项目
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<InspectionIqcItemConfigModel> getIqcNeedInspectionItemDatas(string materialId, DateTime materialInDate)
        {
            List<InspectionIqcItemConfigModel> needInsepctionItems = InspectionManagerCrudFactory.IqcItemConfigCrud.FindIqcInspectionItemConfigDatasBy(materialId);
            if (needInsepctionItems == null || needInsepctionItems.Count <= 0) return new List<InspectionIqcItemConfigModel>();
            var IsAddOrRemoveItemDic = JudgeIsAddOrRemoveItemDic(materialInDate, needInsepctionItems);

            needInsepctionItems.ForEach(m =>
            {

                if (IsAddOrRemoveItemDic.ContainsKey(m.InspectionItem))
                {
                    if (IsAddOrRemoveItemDic[m.InspectionItem])
                    {
                        needInsepctionItems.Remove(m);
                    }
                }
            });
            return needInsepctionItems;
        }

        /// <summary>
        ///  判定是否需要测试 盐雾测试
        /// </summary>
        /// <param name="materialId">物料料号</param>
        /// <param name="materialInDate">当前物料进料日期</param>
        /// <returns></returns>
        public Dictionary<string, bool> JudgeIsAddOrRemoveItemDic(DateTime materialInDate, List<InspectionIqcItemConfigModel> items)
        {

            return null;
        }



        /// <summary>
        /// 盐雾测试
        /// </summary>
        /// <param name="materialInDate"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool JudgeYwTest(DateTime materialInDate, InspectionIqcItemConfigModel item)
        {
            try
            {
                //调出此物料所有打印记录项
                var inspectionItemsRecords = InspectionManagerCrudFactory.IqcDetailCrud.GetIqcInspectionDetailDatasBy(item.MaterialId);
                //如果第一次打印 
                if (inspectionItemsRecords == null | inspectionItemsRecords.Count() <= 0)
                    return true;
                // 进料日期后退30天 抽测打印记录
                var inspectionItemsMonthRecord = (from t in inspectionItemsRecords
                                                  where t.MaterialInDate >= (materialInDate.AddDays(-30))
                                                        & t.MaterialInDate <= materialInDate
                                                  select t.InspecitonItem).Distinct<string>().ToList();
                //没有 测
                if (inspectionItemsMonthRecord == null) return true;
                // 有  每项中是否有测过  盐雾测试
                foreach (var n in inspectionItemsMonthRecord)
                {
                    if (n.Contains("盐雾")) return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 判定些物料在二年内是否有录入记录 
        /// </summary>
        /// <param name="materialInDate"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool JudgeMaterialTwoYearIsRecord(InspectionIqcItemConfigModel item)
        {
            var inspectionItemsRecords = InspectionManagerCrudFactory.IqcDetailCrud.GetIqcInspectionDetailDatasBy(item.MaterialId);
            if (inspectionItemsRecords == null | inspectionItemsRecords.Count() <= 0)
                return false;
            var returnitem = inspectionItemsRecords.Where(e => e.MaterialInDate >= DateTime.Now.AddYears(-2));
            if (returnitem != null && returnitem.Count() > 0)
                return true;
            return false;
        }

        /// <summary>
        /// ROHS 抽样规则 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>

        private bool JudgeROHSTest(InspectionIqcItemConfigModel item)
        {
            return true;
        }
        /// <summary>
        ///ROT抽样规则 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool JudgeROTest(InspectionIqcItemConfigModel item)
        {
            return true;
        }
    }
        
}
