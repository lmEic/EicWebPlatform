using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.Erp.Bussiness.QmsManage;
using Lm.Eic.App.Erp.Domain.QuantityModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
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

        public IqcInspectionItemConfigShowModel GetIqcspectionItemConfigBy(string materialId)
        {
            return new IqcInspectionItemConfigShowModel()
            {
                ProductMaterailModel = QmsDbManager.MaterialInfoDb.GetProductInfoBy(materialId).FirstOrDefault(),
                InspectionItemConfigModelList = IqcInspectionManagerCrudFactory.InspectionItemConfigCrud.FindIqcInspectionItemConfigsBy(materialId)
            };
        }
    }
  
}
