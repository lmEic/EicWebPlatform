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
        /// 日报输入管理器
        /// </summary>
        private DailyReportTemplateManager DailyReportTemplateManager
        {
            get { return OBulider.BuildInstance<DailyReportTemplateManager>(); }
        }

        /// <summary>
        /// 获取日报模板
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public List<DailyReportModel> GetDailyReportTemplate(string department)
        {
          return  DailyReportTemplateManager.GetDailyReportTemplateListBy(department);
        }


    }


    /// <summary>
    /// 日报模板管理器
    /// </summary>
    internal class DailyReportTemplateManager
    {

        /// <summary>
        /// 转换为日报模板列表
        /// </summary>
        /// <param name="modelList">日报列表</param>
        /// <returns></returns>
        List<DailyReportTemplateModel> ConventTemplateList(List<DailyReportModel> modelList)
        {
            //TODO：实现日报List 转换为模板List
            return null;
        }

        /// <summary>
        /// 转会为日报列表
        /// </summary>
        /// <param name="modelList">模板列表</param>
        /// <returns></returns>
        List<DailyReportModel> ConventDailyReportList(List<DailyReportTemplateModel> modelList)
        {
            //TODO：实现模板list 转换为日报List
            return null;
        }

        /// <summary>
        /// 获取日报模板列表
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public List<DailyReportModel> GetDailyReportTemplateListBy(string department)
        {
            //TODO:从数据集中找到模板列表
            //将模板列表转换为日报列表 并返回
            return null;
        }

        /// <summary>
        /// 存储模板列表
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult Store(List<DailyReportModel> modelList)
        {
            //TODO:先查找出该部门的模板进行删除 然后进行存储 保证数据库中始终是最新的模板
            return null;
        }
    }


}
