using Lm.Eic.App.DbAccess.Bpm.Repository.PmsRep.DailyReport;
using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExtension.Validation;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lm.Eic.App.Business.Bmp.Pms.DailyReport
{

    internal class DailyReportInputCrudFactory
    {
        /// <summary>
        /// 日报录入
        /// </summary>
        public static DailyReportInputCrud DailyReportCrud
        { get { return OBulider.BuildInstance<DailyReportInputCrud>(); } }

        /// <summary>
        ///  临时日报录入
        /// </summary>
        public static DailyReportTempCrud DailyReportTempCrud
        {
            get { return OBulider.BuildInstance<DailyReportTempCrud>(); }
        }
    }


    /// <summary>
    /// 日报录入正式表CRUD
    /// </summary>
    public class DailyReportInputCrud : CrudBase<DailyReportModel, IDailyReportRepository>
    {
        public DailyReportInputCrud() : base(new DailyReportRepository(), "日报录入") { }

        protected override void AddCrudOpItems() { }

        /// <summary>
        /// 保存日报数据列表
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult SavaDailyReportList(List<DailyReportModel> modelList)
        {
            //添加模板列表       要求：一次保存整个列表
            try
            {
                int record = irep.Insert(modelList);
                return OpResult.SetResult("审核成功", "审核失败", record);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 删除日报列表
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public OpResult DeleteDailyReportListBy(string department,DateTime dailyReportDate)
        {
            try
            {
                return irep.Delete(m => m.Department == department && m.DailyReportDate == dailyReportDate).ToOpResult(OpContext);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        /// <summary>
        /// 获取日报列表
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public List<DailyReportModel> GetDailyReportListBy(string department)
        {
            try
            {
                return irep.Entities.Where(e => e.Department == department).ToList();
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
        }
    }


    /// <summary>
    /// 日报临时库 CRUD
    /// </summary>
    public class DailyReportTempCrud : CrudBase<DailyReportTempModel, IDailyReportTempRepository>
    {
        public DailyReportTempCrud() : base(new DailyReportTepmRepository(), "日报临时录入")
        { }

        protected override void AddCrudOpItems() { }


        /// <summary>
        /// 保存日报数据列表
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult SavaDailyReportList(List<DailyReportTempModel> modelList,DateTime inPutDate)
        {
            //添加模板列表       要求：一次保存整个列表
            try
            {
                DateTime date = DateTime.Now.ToDate();
               // SetFixFieldValue(modelList, OpMode.Add);
                SetFixFieldValue(modelList, OpMode.Add,m=>
                {
                    m.DailyReportDate = inPutDate;
                    m.InputTime = date;
                    m.ParamenterKey = m.Department + "&" + m.DailyReportDate.ToString("yyyyMMdd");
                    m.DailyReportMonth = m.DailyReportDate.ToString("yyyyMM");
                    m.CheckSign = "未审核";
                    if(m.FailureRate ==null)
                    { m.FailureRate = "0.00%"; }
                });

                if (!modelList.IsNullOrEmpty())
                    return OpResult.SetResult("日报列表不能为空！ 保存失败");

                return irep.Insert(modelList).ToOpResult_Add(OpContext);
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
        }

      
        public OpResult ChangeChackSign(string department,DateTime dailyReportDate,string chackSign)
        {
            try
            {
                return irep.ChangeCheckSign(department, dailyReportDate, chackSign).ToOpResult("审核成功", "审核未成功");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// 删除日报列表
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public OpResult DeleteDailyReportListBy(string department,DateTime dailyReportDate)
        {
            //根据组合值清除符合条件的列表 
            try
            {
                return irep.Delete(m => m.Department == department && m.DailyReportDate == dailyReportDate).ToOpResult(OpContext);
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }

        }

        /// <summary>
        /// 获取日报列表
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public List<DailyReportTempModel> GetDailyReportListBy(string department)
        {
            try
            {
                  return irep.Entities.Where(m => m.Department == department).ToList();
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
        }

        /// <summary>
        /// 获取日报列表
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public List<DailyReportTempModel> GetDailyReportListBy(string department,DateTime dailyReportDate)
        {
            try
            {
                dailyReportDate = dailyReportDate.ToDate();
                return irep.Entities.Where(m => m.Department == department && m.DailyReportDate == dailyReportDate).ToList();
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
        }
    }


}
