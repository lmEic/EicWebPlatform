using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.App.Erp.Bussiness.MocManage;
using Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel;
using Lm.Eic.Framework.Authenticate.Business;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
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
        private List<OrderModel> _orderDetailsList = new List<OrderModel>();  //工单缓存

        /// <summary>
        /// 获取日报模板
        /// </summary>
        /// <param name="department">部门</param>
        /// <param name="dailyReportDate">日报日期</param>
        /// <returns></returns>
        public List<DailyReportTempModel> GetDailyReportTemplate(string department, DateTime dailyReportDate)
        {
            //从临时表中获取本部门的日报数据 如果有今天的就返回今天的 如果没有就返回上一次的
            var departmentAllDailyReportList = DailyReportInputCrudFactory.DailyReportTempCrud.GetDailyReportListBy(department);
            if (departmentAllDailyReportList.Count > 1)
            {
                var dailyReportList = departmentAllDailyReportList.Where(date => date.DailyReportDate == dailyReportDate.ToDate()).ToList();
                if (dailyReportList.Count < 1)
                {
                    //获取最近日期的日报
                    var maxDailyReportDate = departmentAllDailyReportList.Max(m => m.DailyReportDate);
                    return departmentAllDailyReportList.Where(m => m.DailyReportDate == maxDailyReportDate).ToList();
                }
                else return dailyReportList;
            }
            return new List<DailyReportTempModel>();
          
        }

        /// <summary>
        /// 保存日报列表
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult SavaDailyReportList(List<DailyReportTempModel> modelList, DateTime inPutReportDate)
        {
            //先获取待保存的数据列表 如果新数据保存失败 则将清空的数据还原回数据库
            var department = string.Empty;
            var dailyReportDate = DateTime.Now.ToDate();

            if (modelList.IsNullOrEmpty())
            {
                department = modelList[0].Department;
                dailyReportDate = inPutReportDate.ToDate();
            }
            var temDailyList = DailyReportInputCrudFactory.DailyReportTempCrud.GetDailyReportListBy(department, dailyReportDate);

            try
            {
                DailyReportInputCrudFactory.DailyReportTempCrud.DeleteDailyReportListBy(department, dailyReportDate);
                var savaResult = DailyReportInputCrudFactory.DailyReportTempCrud.SavaDailyReportList(modelList, dailyReportDate);
                if (!savaResult.Result)
                {
                    DailyReportInputCrudFactory.DailyReportTempCrud.SavaDailyReportList(temDailyList, dailyReportDate);
                    return OpResult.SetResult("数据保存失败！");
                }
                else
                    return savaResult;
            }
            catch (Exception ex)
            {
                DailyReportInputCrudFactory.DailyReportTempCrud.SavaDailyReportList(temDailyList, dailyReportDate);
                return OpResult.SetResult("数据保存失败！");
                throw new Exception(ex.InnerException.Message);
            }

        }

        /// <summary>
        /// 日报审核
        /// </summary>
        /// <returns></returns>
        public OpResult AuditDailyReport(string department, DateTime dailyReportDate)
        {
            //将临时表中的本部门的所有列表 克隆至正式日报表中
            var dailyReportTempList = DailyReportInputCrudFactory.DailyReportTempCrud.GetDailyReportListBy(department, dailyReportDate);

            if (!dailyReportTempList.IsNullOrEmpty())
                return OpResult.SetResult("未找到本部门的任何日报记录！");

            //清除正式表中的本部门的日报数据
            DailyReportInputCrudFactory.DailyReportCrud.DeleteDailyReportListBy(department, dailyReportDate.ToDate());
            var dailyReportList = OOMaper.Mapper<DailyReportTempModel, DailyReportModel>(dailyReportTempList).ToList();
            return DailyReportInputCrudFactory.DailyReportCrud.SavaDailyReportList(dailyReportList);
        }

        /// <summary>
        /// 添加一条审核记录
        /// </summary>
        /// <param name="department"></param>
        /// <param name="date"></param>
        private OpResult AddAuditRecord(string department, DateTime date)
        {
            var auditModel = new Framework.Authenticate.Model.AuditModel();
            auditModel.ModuleName = "DailyReport";
            auditModel.ParameterKey = string.Format("{0}&{1}&{2}", auditModel.ModuleName, department, date.ToString("yyyyMMdd"));
            auditModel.AuditRand = 0;
            auditModel.AuditState = "已审核";
            auditModel.EndRand = 0;
            auditModel.AuditUser = "admin";
            return AuthenService.AuditManager.AddAuditRecoud(auditModel);
        }

        /// <summary>
        /// 获取工单详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderModel GetOrderDetails(string orderId)
        {
            OrderModel orderDetails = null;

            if (_orderDetailsList.Count > 0)
                orderDetails = _orderDetailsList.FirstOrDefault(m => m.OrderId == orderId);

            if (orderDetails == null)
                orderDetails = MocService.OrderManage.GetOrderDetails(orderId);

            if (orderDetails == null)
                orderDetails = DailyReportConfigCrudFactory.DailyOrderConfigCrud.GetOrderDetails(orderId);

            if (orderDetails != null)
                _orderDetailsList.Add(orderDetails);

            return orderDetails;
        }
    }
}
