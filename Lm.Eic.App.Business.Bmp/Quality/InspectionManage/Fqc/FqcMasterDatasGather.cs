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
        /// <param name="MarterialId"></param>
        /// <returns></returns>
        public List<InspectionFqcMasterModel> GetFqcMasterModeListlBy(string MarterialId)
        {
            return InspectionManagerCrudFactory.FqcMasterCrud.GetFqcInspectionMasterListBy(MarterialId);
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
            var existmodel = InspectionManagerCrudFactory.FqcMasterCrud.ExistModel(model.OrderId, model.OrderIdNumber, model.MaterialId);
            if (existmodel != null && existmodel.Id_Key != 0 && model.OpPerson != "StartSetValue")
            {
                if (existmodel.InspectionItems.Contains(model.InspectionItems))
                    model.InspectionItems = existmodel.InspectionItems;
                else model.InspectionItems = existmodel.InspectionItems + "," + model.InspectionItems;
                if (model.InspectionItemCount == this.GetHaveFinishDataNumber(model.InspectionItems))
                {
                    model.InspectionResult = "已完成";
                }
                model.Id_Key = existmodel.Id_Key;
                model.OpSign = "edit";
                return InspectionManagerCrudFactory.FqcMasterCrud.Store(model);
            }
            if (existmodel == null && model.OpPerson == "StartSetValue")
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
