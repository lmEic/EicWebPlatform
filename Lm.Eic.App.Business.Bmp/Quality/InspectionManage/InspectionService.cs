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
        ///检验配置管理器
        /// </summary>
        public static InspectionConfigurationManager ConfigManager
        {
            get { return OBulider.BuildInstance<InspectionConfigurationManager>(); }
        }


        /// <summary>
        ///数据采集管理器
        /// </summary>
        public static InsepctionDataGatherManager DataGatherManager
        {
            get { return OBulider.BuildInstance<InsepctionDataGatherManager>(); }
        }


        ///<summary>
        ///进料检验单管理模块
        ///<summary>
        public static InspectionFormManager InspectionFormManager
        {
            get { return OBulider.BuildInstance<InspectionFormManager>(); }
        }



      
    }
}
