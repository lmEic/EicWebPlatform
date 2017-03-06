using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
  /// <summary>
  /// 检验数据采集管理器
  /// </summary>
  public class InsepctionDataGatherManager
    {
        /// <summary>
        /// Iqc数据采集器
        /// </summary>
        public InspectionIqcDataGather IqcDataGather
        {
            get { return OBulider.BuildInstance<InspectionIqcDataGather>(); }
        }

    }
}
