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
            return InspectionIqcManagerCrudFactory.IqcMasterCrud.GetIqcInspectionMasterModelListBy(inspectionStatus,startTime,endTime);
        }
    }


}
