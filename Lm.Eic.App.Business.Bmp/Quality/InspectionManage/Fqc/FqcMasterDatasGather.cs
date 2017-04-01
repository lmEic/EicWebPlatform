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
            { return null; throw new Exception(ex.InnerException.Message); }
        }
        /// <summary>
        /// 存储主表信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult storeInspectionMasterial(InspectionFqcMasterModel model)
        {

            var oldModel = InspectionManagerCrudFactory.FqcMasterCrud.GetStroeOldModel(model);
            /// model 如果是新加的  model.OpPerson为StartSetValue
            /// 先排除是否是新增的
            if (oldModel != null && oldModel.Id_Key != 0 && model.OpPerson != "StartSetValue")
            {
                if (oldModel.InspectionItems.Contains(model.InspectionItems))
                    model.InspectionItems = oldModel.InspectionItems;
                else model.InspectionItems = oldModel.InspectionItems + "," + model.InspectionItems;
                if (model.InspectionItemCount == this.GetHaveFinishDataNumber(model.InspectionItems))
                { model.InspectionResult = "已完成"; }
                model.Id_Key = oldModel.Id_Key;
                model.OpSign = OpMode.Edit;
                return InspectionManagerCrudFactory.FqcMasterCrud.Store(model);
            }
            /// 如果 是新增 只添加一次 
            if (oldModel == null && model.OpPerson == "StartSetValue")
                return InspectionManagerCrudFactory.FqcMasterCrud.Store(model);
            return new OpResult("不必添加了");



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
