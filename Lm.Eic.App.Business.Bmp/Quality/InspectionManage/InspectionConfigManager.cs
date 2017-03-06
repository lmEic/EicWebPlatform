using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.Erp.Bussiness.QmsManage;
using Lm.Eic.App.Erp.Domain.QuantityModel;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
    /// <summary>
    /// 检验配置管理器
    /// </summary>
    public class InspectionConfigurationManager
    {
        /// <summary>
        ///检验方式配置管理器
        /// </summary>
        public InspectionModeConfigManager ModeConfigManager
        {
            get { return OBulider.BuildInstance<InspectionModeConfigManager>(); }
        }
        /// <summary>
        ///Iqc检验项目配置管理器
        /// </summary>
        public InspectionIqcItemConfigManager IqcItemConfigManager
        {
            get { return OBulider.BuildInstance<InspectionIqcItemConfigManager>(); }
        }
    }

    /// <summary>
    ///  检验方式的配置管理器
    /// </summary>
    public class InspectionModeConfigManager
    {
        /// <summary>
        /// 存储  检验方式配置数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreInspectionModeConfig(InspectionModeConfigModel model)
        {
            return InspectionIqcManagerCrudFactory.InspectionModeConfigCrud.Store(model, true);
        }
    }




  
}
