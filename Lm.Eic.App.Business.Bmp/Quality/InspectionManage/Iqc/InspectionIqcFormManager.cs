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
        /// 得到IQC检验表单信息 
        /// </summary>
        /// <param name="formQueryString"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<InspectionIqcMasterModel> GetInspectionFormManagerDatas(string formQueryString, int queryOpModel, DateTime startTime, DateTime endTime)
        {
            //查询所有物料和单号 
            if (formQueryString == null || formQueryString == string.Empty) return InspectionManagerCrudFactory.IqcMasterCrud.GetIqcInspectionMasterModelListBy(startTime, endTime); ;
            switch (queryOpModel)
            {
                case 0:
                    return GetInspectionFormManagerListBy(formQueryString, startTime, endTime);
                case 1:
                    return InspectionManagerCrudFactory.IqcMasterCrud.GetIqcInspectionMasterMaterialIdDatasBy(startTime, endTime, formQueryString);
                case 2:
                    return InspectionManagerCrudFactory.IqcMasterCrud.GetIqcInspectionMasterMaterialSupplierDatasBy(startTime, endTime, formQueryString);
                case 3:
                    return InspectionManagerCrudFactory.IqcMasterCrud.GetIqcInspectionMasterInspectionItemsDatasBy(startTime, endTime, formQueryString);
                default:
                    return new List<InspectionIqcMasterModel>();
            }
        }

        /// <summary>
        ///审核主表数据
        /// </summary>
        /// <returns></returns>
        public OpResult AuditIqcInspectionMasterModel(InspectionIqcMasterModel model)
        {
            if (model == null) return null;

            var retrunResult = InspectionManagerCrudFactory.IqcMasterCrud.Store(model, true);
            if (retrunResult.Result)
                return InspectionManagerCrudFactory.IqcMasterCrud.UpAuditDetailData(model.OrderId, model.MaterialId, "Done");

            else return retrunResult;
        }

        /// <summary>
        /// 得到IQC检验表单信息 （数量不超过100）
        /// </summary>
        /// <param name="inspectionStatus"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        private List<InspectionIqcMasterModel> GetInspectionFormManagerListBy(string inspectionStatus, DateTime startTime, DateTime endTime)
        {

            switch (inspectionStatus)
            {
                case "待检验":
                    return GetMasterTableDatasToSqlOrderAndMaterialBy(startTime, endTime);
                case "未完成":
                    return InspectionManagerCrudFactory.IqcMasterCrud.GetIqcInspectionMasterStatusDatasBy(startTime, endTime, "未完成");
                case "待审核":
                    return InspectionManagerCrudFactory.IqcMasterCrud.GetIqcInspectionMasterStatusDatasBy(startTime, endTime, "待审核");
                case "已审核":
                    return InspectionManagerCrudFactory.IqcMasterCrud.GetIqcInspectionMasterStatusDatasBy(startTime, endTime, "已审核");
                case "全部":
                    return GetERPOrderAndMaterialBy(startTime, endTime);
                default:
                    return new List<InspectionIqcMasterModel>();
            }

        }

        private List<InspectionIqcMasterModel> GetERPOrderAndMaterialBy(DateTime startTime, DateTime endTime)
        {
            List<InspectionIqcMasterModel> retrunList = new List<InspectionIqcMasterModel>();
            InspectionIqcMasterModel model = null;
            var OrderIdList = GetOrderIdList(startTime, endTime);
            if (OrderIdList == null || OrderIdList.Count <= 0) return retrunList;
            OrderIdList.ForEach(e =>
            {
                if (InspectionManagerCrudFactory.IqcMasterCrud.IsExistOrderIdAndMaterailId(e.OrderID, e.ProductID))
                    model = InspectionManagerCrudFactory.IqcMasterCrud.GetIqcInspectionMasterDatasBy(e.OrderID, e.ProductID);
                else model = MaterialModelToInspectionIqcMasterModel(e);
                if (!retrunList.Contains(model) && model != null)
                    retrunList.Add(model);
            });
            return retrunList;
        }
        /// <summary>
        ///    
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        private List<InspectionIqcMasterModel> GetMasterTableDatasToSqlOrderAndMaterialBy(DateTime startTime, DateTime endTime)
        {
            List<InspectionIqcMasterModel> retrunList = new List<InspectionIqcMasterModel>();
            var OrderIdList = GetOrderIdList(startTime, endTime);
            if (OrderIdList == null || OrderIdList.Count <= 0) return retrunList;
            OrderIdList.ForEach(e =>
            {
                if (!InspectionManagerCrudFactory.IqcMasterCrud.IsExistOrderIdAndMaterailId(e.OrderID, e.ProductID))
                {
                    retrunList.Add(MaterialModelToInspectionIqcMasterModel(e));
                }
            });
            return retrunList;
        }

        private InspectionIqcMasterModel MaterialModelToInspectionIqcMasterModel(MaterialModel model)
        {
            return model != null ? (new InspectionIqcMasterModel()
            {
                OrderId = model.OrderID,
                MaterialName = model.ProductName,
                MaterialSpec = model.ProductStandard,
                MaterialSupplier = model.ProductSupplier,
                MaterialDrawId = model.ProductDrawID,
                MaterialId = model.ProductID,
                InspectionStatus = "侍检验",
                MaterialCount = model.ProduceNumber,
                MaterialInDate = model.ProduceInDate,
                InspectionResult = "侍检验",
                InspectionItems = "还没有抽检",
                InspectionMode = "正常"
            }) : null;
        }
        private List<MaterialModel> GetOrderIdList(DateTime starDate, DateTime endDate)
        {
            return QualityDBManager.OrderIdInpectionDb.FindErpAllMasterilBy(starDate, endDate);
        }
    }


}
