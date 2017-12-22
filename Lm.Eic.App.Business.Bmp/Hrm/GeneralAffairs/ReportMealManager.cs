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

        private bool CheckCanStoreRule(List<MealReportManageModel> entities, out string msg)
        {
            msg = string.Empty;
            DateTime now = DateTime.Now;
            DateTime limitTime = new DateTime(now.Year, now.Month, now.AddDays(-1).Day, 16, 0, 0);
            StringBuilder sbMsg = new StringBuilder();
            entities.ForEach(m =>
            {
                if (m.OpSign == OpMode.Edit)
                {
                    if (m.ReportTime <= limitTime)
                    {
                        sbMsg.AppendLine(string.Format("工号：{0},姓名:{1}的报餐时间{2}已经超过指定修改时间{3}期限，数据已冻结，禁止修改！", m.WorkerId, m.WorkerName, m.ReportTime, limitTime));
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
        #endregion

    }
}
