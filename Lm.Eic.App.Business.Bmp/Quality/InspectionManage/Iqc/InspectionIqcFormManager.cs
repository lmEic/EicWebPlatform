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
            var datas = InspectionManagerCrudFactory.IqcMasterCrud.GetIqcInspectionMasterModelListBy(startTime, endTime);
            if (formQueryString == null || formQueryString == string.Empty) return datas;
            switch (queryOpModel)
            {
                case 0:
                    return GetInspectionFormManagerListBy(datas, formQueryString, startTime, endTime);
                case 1:
                    return datas.Where(e => e.MaterialId == formQueryString).ToList();
                case 2:
                    return datas.Where(e => e.MaterialSupplier.Contains(formQueryString)).ToList();
                case 3:
                    return datas.Where(e => e.InspectionItems.Contains(formQueryString)).ToList();
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
        private List<InspectionIqcMasterModel> GetInspectionFormManagerListBy(List<InspectionIqcMasterModel> datas, string inspectionStatus, DateTime startTime, DateTime endTime)
        {
            switch (inspectionStatus)
            {
                case "待检验":
                    return GetMasterTableDatasToSqlOrderAndMaterialBy(startTime, endTime);
                case "未完成":
                    return datas.Where(e => e.InspectionStatus == "未完成").ToList();
                case "待审核":
                    return datas.Where(e => e.InspectionStatus == "待审核").ToList();
                case "已审核":
                    return datas.Where(e => e.InspectionStatus == "已审核").ToList();
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
                if (!retrunList.Contains(model)&&model !=null )
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
                InspectionStatus = "未完成",
                MaterialCount = model.ProduceNumber,
                MaterialInDate = model.ProduceInDate,
                InspectionResult = string.Empty,
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
