using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.App.Erp.Bussiness.MocManage;
using Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel;
using Lm.Eic.Framework.Authenticate.Business;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExtension.Validation;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;

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

        /// <summary>
        /// 日报出勤输入管理器
        /// </summary>
        public ReportAttendenceManager ReportAttendenceManager
        {
            get { return OBulider.BuildInstance<ReportAttendenceManager>(); }
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
            DateTime dailyDate = dailyReportDate.ToDate();
            if (departmentAllDailyReportList.Count > 0)
            {
                var dailyReportList = departmentAllDailyReportList.Where(date => date.DailyReportDate == dailyDate).OrderBy(e => e.InPutId).ToList();
                if (dailyReportList.Count > 0)
                {
                    //获取最近日期的日报
                    return dailyReportList;
                }
                else
                {
                    //已审核数据模板
                    var maxDailyReportDate = departmentAllDailyReportList.Where(m => m.CheckSign == "已审核").Max(m => m.DailyReportDate);
                    //得到最近数据
                    var maxDailyReportList = departmentAllDailyReportList.Where(m => m.DailyReportDate == maxDailyReportDate).OrderBy(e => e.InPutId).ToList();
                    var returnList = new List<DailyReportTempModel>();
                    maxDailyReportList.ForEach(m =>
                    {
                        DailyReportTempModel mdl = (DailyReportTempModel)m.Clone();
                        mdl.Qty = 0;
                        mdl.QtyBad = 0;
                        mdl.QtyGood = 0;
                        mdl.ReceiveHours = 0;
                        mdl.AttendanceHours = 0;
                        mdl.NonProductionHours = 0;
                        mdl.ProductionHours = 0;
                        mdl.SetHours = 12;
                        mdl.InputHours = 12;
                        mdl.OperationEfficiency = "100%";
                        mdl.ProductionEfficiency = "100%";
                        mdl.EquipmentEifficiency = "100%";
                        returnList.Add(mdl);
                    });
                    return returnList;
                }
            }
            return new List<DailyReportTempModel>();

        }
        /// <summary>
        /// 导出Excel列表
        /// </summary>
        private List<FileFieldMapping> fieldmappping = new List<FileFieldMapping>(){
                  new FileFieldMapping ("MachineId","機台號"),
                  new FileFieldMapping ("OrderId","制令单"),
                  new FileFieldMapping ("ProductName","品名"),
                  new FileFieldMapping ("ProductSpecification","规格"),
                  new FileFieldMapping ("UserName","作业员"),
                  new FileFieldMapping ("MasterName","师傅"),
                  new FileFieldMapping ("ClassType","班别"),
                  new FileFieldMapping ("ProductFlowName","工序"),
                  new FileFieldMapping ("StandardHours","标准工时"),
                  new FileFieldMapping ("ReceiveHours","得到時數"),
                  new FileFieldMapping ("Qty","产量"),
                  new FileFieldMapping ("QtyBad","不良数"),
                  new FileFieldMapping ("SetHours","設置時數"),
                  new FileFieldMapping ("InputHours","投入時數"),
                  new FileFieldMapping ("AttendanceHours","出勤時數"),
                  new FileFieldMapping ("NonProductionHours","非生产時數"),
                  new FileFieldMapping ("EquipmentEifficiency","稼动率" )
                };
        /// <summary>
        /// 生成日报表清单
        /// </summary>
        /// <param name="department"></param>
        /// <param name="dailyReportDate"></param>
        /// <returns></returns>
        public DownLoadFileModel BuildDailyReportTempList(string department, DateTime dailyReportDate)
        {
            
            var datas = DailyReportInputCrudFactory.DailyReportTempCrud.GetDailyReportListBy(department, dailyReportDate);
            if (datas == null && datas.Count <= 0) return new DownLoadFileModel().Default();
            var dataGroupping = datas.GetGroupList<DailyReportTempModel>();
            return dataGroupping.ExportToExcelMultiSheets<DailyReportTempModel>(fieldmappping).CreateDownLoadExcelFileModel(department + "日报数据(" + dailyReportDate.ToShortDateString() + ")");
        }
        /// <summary>
        /// 保存日报列表
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult SavaDailyReportList(List<DailyReportTempModel> modelList, DateTime inPutReportDate)
        {
            //先获取待保存的数据列表 如果新数据保存失败 则将清空的数据还原回数据库
            string department = string.Empty;
            var dailyReportDate = DateTime.Now.ToDate();
            if (modelList.IsNullOrEmpty())
            {
                department = modelList[0].Department;

                dailyReportDate = inPutReportDate.ToDate();
            }
            var temDailyList = DailyReportInputCrudFactory.DailyReportTempCrud.GetDailyReportListBy(department, dailyReportDate);

            try
            {
                //更新日报表
                //先删除
                DailyReportInputCrudFactory.DailyReportTempCrud.DeleteDailyReportListBy(department, dailyReportDate);
                //后保存
                var savaResult = DailyReportInputCrudFactory.DailyReportTempCrud.SavaDailyReportList(modelList, dailyReportDate);
                //如果保存失败 恢复原来的数据
                if (!savaResult.Result)
                {
                    DailyReportInputCrudFactory.DailyReportTempCrud.SavaDailyReportList(temDailyList, dailyReportDate);
                    return OpResult.SetErrorResult("数据保存失败！");
                }
                else
                    return savaResult;
            }
            catch (Exception ex)
            {
                DailyReportInputCrudFactory.DailyReportTempCrud.SavaDailyReportList(temDailyList, dailyReportDate);
                return OpResult.SetErrorResult("数据保存失败！");
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
                return OpResult.SetErrorResult("未找到本部门的任何日报记录！");

            //清除正式表中的本部门的日报数据
            DailyReportInputCrudFactory.DailyReportCrud.DeleteDailyReportListBy(department, dailyReportDate.ToDate());
            //改审核状态
            DailyReportInputCrudFactory.DailyReportTempCrud.ChangeChackSign(department, dailyReportDate.ToDate(), "已审核");
            //由DailyReportTempModel 转化为 DailyReportModel
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
                orderDetails = DailyReportCrudFactory.DailyOrderCrud.GetOrderDetails(orderId);

            if (orderDetails != null)
                _orderDetailsList.Add(orderDetails);
            return orderDetails;
        }
    }


}
