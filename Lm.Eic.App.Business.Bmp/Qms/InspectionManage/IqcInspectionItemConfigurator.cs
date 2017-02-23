using Lm.Eic.App.DomainModel.Bpm.Qms;
using Lm.Eic.App.Erp.Bussiness.QmsManage;
using Lm.Eic.App.Erp.Domain.QuantityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Qms.InspectionManage
{

    /// <summary>
    /// IQC 进料检验配置器
    /// </summary>
    public class IqcInspectionItemConfigurator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>

        public List<IqcInspectionItemConfigShowModel> GetIqcspectionItemConfigBy(string materialId)
        {
            var retunListModel = new List<IqcInspectionItemConfigShowModel>();
             var InspectionItemConfigList =  IqcInspectionManagerCrudFactory.InspectionItemConfigCrud.FindIqcInspectionItemConfigsBy(materialId);
            if (retunListModel == null || retunListModel.Count == 0)
            {
                var materialIdInfo = QmsDbManager.MaterialInfoDb.GetProductInfoBy(materialId).FirstOrDefault();
                InspectionItemConfigList.ForEach(m =>
                {
                    retunListModel.Add(
                        new IqcInspectionItemConfigShowModel()
                        {
                            MaterailName = materialIdInfo.MaterailName,
                            MaterialSpecify = materialIdInfo.MaterialSpecify,
                            MaterialrawID=materialIdInfo.MaterialrawID,
                            MaterialBelongDepartment=materialIdInfo .MaterialBelongDepartment,
                            EquipmentID=m.EquipmentID,
                            InspectionAQL=m.InspectionAQL,
                            InspectionItem=m.InspectionItem,
                            InspectionLevel=m.InspectionLevel,
                            InspectionMode=m.InspectionMode,
                            InspectionMethod=m.InspectionMethod,
                            InspectiontermNumber=m.InspectiontermNumber,
                            MaterialId= materialId,
                            SizeLSL=m.SizeLSL,
                            SizeUSL=m.SizeUSL,
                            SizeMemo=m.SizeMemo
                        });
                });
            }
            return retunListModel;
        }
    }
  
}
