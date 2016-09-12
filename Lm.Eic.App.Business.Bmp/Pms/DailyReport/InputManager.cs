using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.DailyReport
{
    /// <summary>
    /// 日报输入管理器
    /// </summary>
    public class InputManager
    {
        /// <summary>
        /// 日报输入管理器
        /// </summary>
        public DailyReportInputManager DailyReportInputManager
        {
            get { return OBulider.BuildInstance<DailyReportInputManager>(); }
        }

    }

    /// <summary>
    /// 日报录入管理器
    /// </summary>
    public class DailyReportInputManager
    {
      
        /// <summary>
        /// 获取日报模板
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public List<DailyReportModel> GetDailyReportTemplate(string department)
        {
            //TODO:先从日报中查找是否有当前的日报记录，如果有 返回当前，没有 从模板中生成
            //确定一个日报是否唯一：部门&日期&班别
            return null;
        }

    }


}
