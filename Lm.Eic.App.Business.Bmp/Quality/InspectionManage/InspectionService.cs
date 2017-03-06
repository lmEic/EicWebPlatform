using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
  
    /// <summary>
    /// 进料检验管理服务接口
    /// </summary>
    public class InspectionService
    {

        /// <summary>
        ///检验方法配置管理
        /// </summary>
        public static InspectionModeConfigManager InspectionModeConfigManager
        {
            get { return OBulider.BuildInstance<InspectionModeConfigManager>(); }
        }



        /// <summary>
        /// 进料检验项目配置器
        /// </summary>
        public static IqcInspectionItemConfigManager InspectionItemConfigurator
        {
            get { return OBulider.BuildInstance<IqcInspectionItemConfigManager>(); }
        }
        /// <summary>
        /// 进料检验项目数据采集器
        /// </summary>
        public static  IqcInspectionDataGather InspectionDataGather
        {
            get { return OBulider.BuildInstance<IqcInspectionDataGather>(); }
        }


        ///<summary>
        ///进料检验单管理模块
        ///<summary>
        public static IqcInspectionFormManager InspectionFormManager
        {
            get { return OBulider.BuildInstance<IqcInspectionFormManager>(); }
        }
    }
}
