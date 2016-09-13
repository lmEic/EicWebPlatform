using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.App.Erp.Bussiness.MocManage;
using Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel;
using Lm.Eic.Uti.Common.YleeExtension.Validation;
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
        /// <param name="paramenterKey">部门</param>
        /// <returns></returns>
        public List<DailyReportModel> GetDailyReportTemplate(string department)
        {
            //TODO:从临时表中获取本部门所有的日报数据
            return DailyReportInputCrudFactory.DailyReportTempCrud.GetDailyReportListBy(department);
        }

        /// <summary>
        /// 保存日报列表
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult SavaDailyReportList(List<DailyReportModel> modelList)
        {
            //清空本部门所有日报列表 =》存入当前列表
            var department = string.Empty;
            if (modelList.IsNullOrEmpty())
                department = modelList[0].Department;

            if (department.IsNullOrEmpty())
                return OpResult.SetResult("部门不能为空！");

            var deleteResult = DailyReportInputCrudFactory.DailyReportTempCrud.DeleteDailyReportListBy(department);
            if (!deleteResult.Result)
                return OpResult.SetResult("清除日报历史记录失败！");

            return DailyReportInputCrudFactory.DailyReportTempCrud.SavaDailyReportList(modelList);
        }

        /// <summary>
        /// 日报审核
        /// </summary>
        /// <returns></returns>
        public OpResult AuditDailyReport(string department)
        {
            //将临时表中的本部门的所有列表 克隆至正式日报表中
            var dailyReportTempList = DailyReportInputCrudFactory.DailyReportTempCrud.GetDailyReportListBy(department);

            if (!dailyReportTempList.IsNullOrEmpty())
                return OpResult.SetResult("未找到本部门的任何日报记录！");

            return DailyReportInputCrudFactory.DailyReportCrud.SavaDailyReportList(dailyReportTempList);
        }


        #region Find

        /// <summary>
        /// 获取工序列表
        /// </summary>
        /// <param name="orderId">工单单号</param>
        /// <returns></returns>
        public List<ProductFlowModel> GetProductFlowListBy(string orderId)
        {
            return DailyReportService.ConfigManager.ProductFlowSetter.GetProductFlowListBy(orderId);
        }


        /// <summary>
        /// 获取工单详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderModel GetOrderDetails(string orderId)
        {
            return MocService.OrderManage.GetOrderDetails(orderId);
        }
        #endregion


    }


}
