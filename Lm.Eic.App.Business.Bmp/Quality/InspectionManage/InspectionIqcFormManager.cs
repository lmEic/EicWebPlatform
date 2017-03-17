using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.Erp.Bussiness.QmsManage;
using Lm.Eic.App.Erp.Bussiness.QuantityManage;
using Lm.Eic.App.Erp.Domain.QuantityModel;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
    /// <summary>
    /// IQC进料检验单管理
    /// </summary>
    public class InspectionIqcFormManager
    {
        /// <summary>
        /// 得到IQC检验表单信息 （数量不超过100）
        /// </summary>
        /// <param name="inspectionStatus"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<InspectionIqcMasterModel> GetInspectionFormManagerListBy(string inspectionStatus, DateTime startTime,DateTime endTime)
        {
            //查询ERP中所有物料和单号 
            switch (inspectionStatus)
            {
                case "未完成":
                    return GetErpNotStoreToSqlOrderAndMaterialBy(startTime, endTime);
                case "全部":
                    return GetERPOrderAndMaterialBy(startTime, endTime);
                case "待审核":
                   return InspectionIqcManagerCrudFactory.IqcMasterCrud.GetIqcInspectionMasterModelListBy(inspectionStatus, startTime, endTime);
                case "已审核":
                    return InspectionIqcManagerCrudFactory.IqcMasterCrud.GetIqcInspectionMasterModelListBy(inspectionStatus, startTime, endTime);
                default:
                    return new List<InspectionIqcMasterModel>();   
            }
          
        }





        List<InspectionIqcMasterModel> GetERPOrderAndMaterialBy(DateTime startTime, DateTime endTime)
        {
            List<InspectionIqcMasterModel> retrunList = new List<InspectionIqcMasterModel>();
            var OrderIdList = GetOrderIdList(startTime  ,endTime );
            if (OrderIdList == null || OrderIdList.Count <= 0)return  retrunList;
            OrderIdList.ForEach(e => {
                if (InspectionIqcManagerCrudFactory.IqcMasterCrud.IsExistOrderIdAndMaterailId(e.OrderID, e.ProductID))
                {
                    retrunList.Add(InspectionIqcManagerCrudFactory.IqcMasterCrud.GetIqcInspectionMasterModelListBy(e.OrderID, e.ProductID));
                }
                else
                {
                    retrunList.Add(MaterialModelToInspectionIqcMasterModel(e));
                }
            });
            return retrunList.OrderByDescending(e => e.MaterialInDate).ToList();
        }

        List<InspectionIqcMasterModel> GetErpNotStoreToSqlOrderAndMaterialBy(DateTime startTime, DateTime endTime)
        {
            List<InspectionIqcMasterModel> retrunList = new List<InspectionIqcMasterModel>();
            var OrderIdList = GetOrderIdList(startTime, endTime);
            if (OrderIdList == null || OrderIdList.Count <= 0) return retrunList;
            OrderIdList.ForEach(e =>
            {
                if (!InspectionIqcManagerCrudFactory.IqcMasterCrud.IsExistOrderIdAndMaterailId(e.OrderID, e.ProductID))
                {
                    retrunList.Add(MaterialModelToInspectionIqcMasterModel(e));
                }
            });
            return retrunList.OrderByDescending (e=>e.MaterialInDate).ToList ();
        }

        InspectionIqcMasterModel MaterialModelToInspectionIqcMasterModel(MaterialModel model)
        {
            return model != null ? (new InspectionIqcMasterModel()
            {
                OrderId = model.OrderID,
                MaterialName = model.ProductName,
                MaterialSpec = model.ProductStandard,
                MaterialSupplier = model.ProductSupplier,
                MaterialDrawId = model.ProductDrawID,
                MaterialId = model.ProductID,
                InspectionStatus = "未完成",
                MaterialCount = model.ProduceNumber,
                MaterialInDate = model.ProduceInDate,
                InspectionResult = string.Empty,
                InspectionItems = "还没有抽检",
                InspectionMode = "正常"
            }) : new InspectionIqcMasterModel();
        }
        List<MaterialModel> GetOrderIdList(DateTime starDate, DateTime endDate)
        {
            return QualityDBManager.OrderIdInpectionDb.FindErpAllMasterilBy(starDate, endDate);
        }
    }


}
