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
        /// 进料检验项目配置器
        /// </summary>
        public static  IqcInspectionItemConfigManager InspectionItemConfigurator
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
    }
}
