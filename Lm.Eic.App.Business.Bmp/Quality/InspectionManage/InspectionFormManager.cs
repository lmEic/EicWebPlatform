using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
  /// <summary>
  /// 检验表单管理器
  /// </summary>
  public class InspectionFormManager
    {
        /// <summary>
        /// IQC 表单管理器
        /// </summary>
        public InspectionIqcFormManager IqcFromManager
        {
            get { return OBulider.BuildInstance<InspectionIqcFormManager>(); }
        }
    }
}
