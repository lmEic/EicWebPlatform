using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.GeneralAffairs;
using Lm.Eic.App.DomainModel.Bpm.Hrm.GeneralAffairs;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
using CrudFactory = Lm.Eic.App.Business.Bmp.Hrm.GeneralAffairs.GeneralAffairsFactory;
using System.IO;

namespace Lm.Eic.App.Business.Bmp.Hrm.GeneralAffairs
{
    public class ReportMealManager
    {
        private const string reportWorkerTypeLG = "陆干餐";
        private const string reportWorkerTypeYG = "员工餐";

        #region method
        public OpResult Store(IList<MealReportManageModel> entities)
        {
            try
            {
                if (entities == null || entities.Count == 0) return OpResult.SetErrorResult("没有要存储的数据！");
                string errMsg = string.Empty;
                if (!CheckCanStoreRule(entities.ToList(), out errMsg))
                {
                    return OpResult.SetErrorResult(errMsg);
                }
                bool result = true;
                foreach (var m in entities)
                {
                    result = result && GeneralAffairsFactory.ReportMealStore.Store(m).Result;
                    if (!result) break;
                }
                return OpResult.SetResult(result ? "存储数据成功" : "存储数据失败", result);
            }
            catch (System.Exception ex)
            {
                return ex.ExOpResult();
            }
        }

        public List<MealReportManageModel> GetReportMealDatas(string reportType, string yearMonth, string department = null, string workerId = null)
        {
            return GeneralAffairsFactory.ReportMealStore.GetReportMealDatas(reportType, yearMonth, department, workerId);
        }
        /// <summary>
        /// 汇总报餐数据
        /// </summary>
        /// <param name="yearMonth"></param>
        /// <param name="ygMealDatas"></param>
        /// <param name="lgMealDatas"></param>
        public MealReportedAnalogModel GetAnalogReportMealDatas(string yearMonth)
        {
            MealReportedAnalogModel analogData = new MealReportedAnalogModel();
            var sumerizeDatas = GeneralAffairsFactory.ReportMealStore.GetReportMealDatas(yearMonth);
            if (sumerizeDatas == null || sumerizeDatas.Count == 0) return analogData;
            analogData.SumerizeDatasOfYG = SumerizeReportMealDatas(sumerizeDatas.FindAll(e => e.WorkerType == reportWorkerTypeYG), yearMonth);
            analogData.SumerizeDatasOfLG = SumerizeReportMealDatas(sumerizeDatas.FindAll(e => e.WorkerType == reportWorkerTypeLG), yearMonth);
            analogData.TotalOfYG = new MealReportSumerizeModel()
            {
                YearMonth = yearMonth,
                Department = "总计",
                TotalCountOfBreakfast = analogData.SumerizeDatasOfYG.Sum(s => s.TotalCountOfBreakfast),
                TotalCountOfLunch = analogData.SumerizeDatasOfYG.Sum(s => s.TotalCountOfLunch),
                TotalCountOfMidnight = analogData.SumerizeDatasOfYG.Sum(s => s.TotalCountOfMidnight),
                TotalCountOfSupper = analogData.SumerizeDatasOfYG.Sum(s => s.TotalCountOfSupper)
            };
            analogData.TotalOfLG = new MealReportSumerizeModel()
            {
                YearMonth = yearMonth,
                Department = "总计",
                TotalCountOfBreakfast = analogData.SumerizeDatasOfLG.Sum(s => s.TotalCountOfBreakfast),
                TotalCountOfLunch = analogData.SumerizeDatasOfLG.Sum(s => s.TotalCountOfLunch),
                TotalCountOfMidnight = analogData.SumerizeDatasOfLG.Sum(s => s.TotalCountOfMidnight),
                TotalCountOfSupper = analogData.SumerizeDatasOfLG.Sum(s => s.TotalCountOfSupper)
            };
            return analogData;
        }

        public DownLoadFileModel ExportAnalogData(MealReportedAnalogModel data)
        {
            if (data == null) return null;
            data.SumerizeDatasOfLG.Add(data.TotalOfLG);
            data.SumerizeDatasOfYG.Add(data.TotalOfYG);
            Dictionary<string, List<MealReportSumerizeModel>> dicDatas = new Dictionary<string, List<MealReportSumerizeModel>>() {
                {reportWorkerTypeYG,data.SumerizeDatasOfYG }, { reportWorkerTypeLG,data.SumerizeDatasOfLG}
            };
            List<FileFieldMapping> fieldMapps = new List<FileFieldMapping>() {
                new FileFieldMapping("YearMonth","年月份"),
                new FileFieldMapping("Department","部门"),
                new FileFieldMapping("TotalCountOfBreakfast","早餐数量汇总"),
                new FileFieldMapping("TotalCountOfLunch","午餐数量汇总"),
                new FileFieldMapping("TotalCountOfSupper","晚餐数量汇总"),
                new FileFieldMapping("TotalCountOfMidnight","夜宵数量汇总"),
            };
            return dicDatas.ExportToExcelMultiSheets<MealReportSumerizeModel>(fieldMapps).CreateDownLoadExcelFileModel("报餐数据汇总");
        }
        private List<MealReportSumerizeModel> SumerizeReportMealDatas(List<MealReportManageModel> mealDatas, string yearMonth)
        {
            List<MealReportSumerizeModel> sumerizeDatas = null;
            if (mealDatas == null || mealDatas.Count == 0) return sumerizeDatas;
            var departments = mealDatas.Select(f => f.Department).Distinct().ToList();
            if (departments != null && departments.Count > 0)
            {
                sumerizeDatas = new List<MealReportSumerizeModel>();
                departments.ForEach(d =>
                {
                    var datasOfDepartment = mealDatas.FindAll(e => e.Department == d).ToList();
                    if (datasOfDepartment != null && datasOfDepartment.Count > 0)
                    {
                        sumerizeDatas.Add(new MealReportSumerizeModel()
                        {
                            Department = d,
                            YearMonth = yearMonth,
                            TotalCountOfBreakfast = datasOfDepartment.Sum(s => s.CountOfBreakfast),
                            TotalCountOfLunch = datasOfDepartment.Sum(s => s.CountOfLunch),
                            TotalCountOfMidnight = datasOfDepartment.Sum(s => s.CountOfMidnight),
                            TotalCountOfSupper = datasOfDepartment.Sum(s => s.CountOfSupper)
                        });
                    }
                });
            }
            return sumerizeDatas;
        }
        private bool CheckCanStoreRule(List<MealReportManageModel> entities, out string msg)
        {
            msg = string.Empty;
            DateTime now = DateTime.Now;
            int nowday = now.Day;
            DateTime targetTime = new DateTime(now.Year, now.Month, now.Day, 16, 0, 0);

            StringBuilder sbMsg = new StringBuilder();
            entities.ForEach(m =>
            {
                if (m.OpSign == OpMode.Edit)
                {
                    if (now > targetTime)
                    {
                        nowday = now.AddDays(1).Day;
                    }
                    if (m.ReportDayAt < nowday)
                    {
                        sbMsg.AppendLine(string.Format("工号：{0},姓名:{1}的报餐时间{2}已经超过指定时间期限，数据已冻结，禁止修改！", m.WorkerId, m.WorkerName, m.ReportDay));
                    }
                }
            });
            if (sbMsg.Length > 0)
            {
                sbMsg.AppendLine("本次数据操作失败！");
                msg = sbMsg.ToString();
                return false;
            }
            return true;
        }
        #endregion
    }
    /// <summary>
    /// 报餐汇总统计模型
    /// </summary>
    public class MealReportedAnalogModel
    {
        public List<MealReportSumerizeModel> SumerizeDatasOfLG { get; set; }
        public List<MealReportSumerizeModel> SumerizeDatasOfYG { get; set; }
        /// <summary>
        /// 陆干餐总汇总模型
        /// </summary>
        public MealReportSumerizeModel TotalOfLG { get; set; }
        /// <summary>
        /// 员工餐总汇总模型
        /// </summary>
        public MealReportSumerizeModel TotalOfYG { get; set; }

    }
    public class MealReportSumerizeModel
    {
        public string YearMonth { get; set; }

        public string Department { get; set; }
        /// <summary>
        ///早餐数量
        /// </summary>
        public int TotalCountOfBreakfast { get; set; }
        /// <summary>
        ///午餐数量
        /// </summary>
        public int TotalCountOfLunch { get; set; }
        /// <summary>
        ///晚餐数量
        /// </summary>
        public int TotalCountOfSupper { get; set; }
        /// <summary>
        ///夜宵数量
        /// </summary>
        public int TotalCountOfMidnight { get; set; }

    }
    internal class ReportMealStore : CrudBase<MealReportManageModel, IMealReportManageRepository>
    {
        public ReportMealStore() : base(new MealReportManageRepository(), "报餐")
        {

        }

        #region store method
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, this.Add);
            this.AddOpItem(OpMode.Edit, this.Update);
            this.AddOpItem(OpMode.Delete, this.Delete);
        }

        private OpResult Add(MealReportManageModel entity)
        {
            entity.ReportTime = DateTime.Now;
            var existItem = this.irep.FirstOfDefault(e => e.Department == entity.Department && e.WorkerId == entity.WorkerId && e.ReportDay == entity.ReportDay);
            if (existItem == null)
            {
                return this.irep.Insert(entity).ToOpResult_Add(this.OpContext);
            }
            entity.Id_Key = existItem.Id_Key;
            return this.Update(entity);
        }
        private OpResult Update(MealReportManageModel entity)
        {
            entity.ReportTime = DateTime.Now;
            return this.irep.Update(e => e.Id_Key == entity.Id_Key, entity).ToOpResult_Eidt(this.OpContext);
        }

        private OpResult Delete(MealReportManageModel entity)
        {
            return this.irep.Delete(f => f.Id_Key == entity.Id_Key).ToOpResult_Delete(this.OpContext);
        }
        #endregion

        #region query method
        internal List<MealReportManageModel> GetReportMealDatas(string reportType, string yearMonth, string department = null, string workerId = null)
        {
            Expression<Func<MealReportManageModel, bool>> predicate = null;
            if (string.IsNullOrEmpty(workerId))
            {
                predicate = e => e.WorkerType == reportType && e.YearMonth == yearMonth && e.Department == department;
            }
            else if (string.IsNullOrEmpty(workerId) && string.IsNullOrEmpty(department))
            {
                predicate = e => e.WorkerType == reportType && e.YearMonth == yearMonth;
            }
            else
            {
                predicate = e => e.WorkerType == reportType && e.YearMonth == yearMonth && e.Department == department && e.WorkerId == workerId;
            }
            return this.irep.Entities.Where(predicate).ToList();
        }

        internal List<MealReportManageModel> GetReportMealDatas(string yearMonth)
        {
            return this.irep.Entities.Where(e => e.YearMonth == yearMonth).ToList();
        }
        #endregion

    }
}
