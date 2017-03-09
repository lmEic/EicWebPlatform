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
        /// <param name="inspectionStatus"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<InspectionIqcMasterModel> GetInspectionFormManagerListBy(string inspectionStatus, DateTime startTime,DateTime endTime)
        {
            //查询ERP中所有物料和单号


           return  inspectionStatus == "未完成" ? GetERPOrderAndMaterialBy(startTime, endTime):
            InspectionIqcManagerCrudFactory.IqcMasterCrud.GetIqcInspectionMasterModelListBy(inspectionStatus,startTime,endTime);
        }
        public List<InspectionIqcMasterModel> GetERPOrderAndMaterialBy(DateTime startTime, DateTime endTime)
        {
            List<InspectionIqcMasterModel> retrunList = new List<InspectionIqcMasterModel>();
            var OrderIdList = GetOrderIdList(startTime  ,endTime );
            if (OrderIdList == null || OrderIdList.Count <= 0)return  retrunList;
            OrderIdList.ForEach(e => {
                retrunList.Add(new InspectionIqcMasterModel() {
                    OrderId = e.OrderID,
                    MaterialName = e.ProductName,
                    MaterialSpec = e.ProductStandard,
                    MaterialSupplier = e.ProductSupplier,
                    MaterialDrawId = e.ProductDrawID,
                    MaterialId = e.ProductID,
                    InspectionStatus = "未完成",
                    MaterialCount = e.ProduceNumber,
                    MaterialInDate = e.ProduceInDate,
                    InspctionResult = string.Empty,
                    InspectionItems = "还没有抽检",
                    InspectionMode ="正常"
                });
            });
            return retrunList;
        }
        public List<MaterialModel> GetOrderIdList(DateTime starDate, DateTime endDate)
        {
            return QualityDBManager.OrderIdInpectionDb.FindErpAllMasterilBy(starDate, endDate);
        }
    }


}
