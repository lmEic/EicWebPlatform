using Lm.Eic.App.DbAccess.Bpm.Repository.PmsRep.DailyReport;
using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    /// 日报录入CRUD
    /// </summary>
    public class DailyReportInputCrud : CrudBase<DailyReportModel, IDailyReportRepository>
    {
        public DailyReportInputCrud() : base(new DailyReportRepository(), "日报录入")
        {
        }

        /// <summary>
        /// 保存日报数据列表
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult SavaDailyReportList(List<DailyReportModel> modelList)
        {
            //添加模板列表       要求：一次保存整个列表

            SetFixFieldValue(modelList, OpMode.Add);

            modelList.ForEach(m =>
            {
                m.ParamenterKey = m.OrderId + "&" + m.UserName + "&" + m.ProductFlowName + "&"
                                  + m.DailyReportDate.ToShortDateString() + "&" + m.ClassType;
                if (m.MachineId != null || m.MachineId != string.Empty)
                { m.ParamenterKey = m.ParamenterKey + "&" + m.MachineId; }
            });

            return irep.Insert(modelList).ToOpResult(OpContext);
         
        }

        /// <summary>
        /// 根据组合值删除符合条件的数据
        /// </summary>
        /// <param name="paramenterKey"></param>
        /// <returns></returns>
        public OpResult DeleteDailyReportListBy(string paramenterKey)
        {
            //TODO：根据组合值清除符合条件的列表 
            return irep.Delete (e=>e.ParamenterKey ==paramenterKey ).ToOpResult (OpContext);;
        }

        #region Store

     

        #endregion

    }


    /// <summary>
    /// 日报临时库 CRUD
    /// </summary>
    public class DailyReportTempCrud : CrudBase<DailyReportModel, IDailyReportTempRepository>
    {
        public DailyReportTempCrud():base(new DailyReportTepmRepository() , "日报临时录入")
        { }
      
        protected override void AddCrudOpItems()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 保存日报数据列表
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult SavaDailyReportList(List<DailyReportModel> modelList)
        {
            //添加模板列表       要求：一次保存整个列表
            SetFixFieldValue(modelList, OpMode.Add);
            if (modelList == null || modelList.Count <= 0) return OpResult.SetResult("保存失败");
            modelList.ForEach(m =>
            {
                m.ParamenterKey = m.OrderId + "&" + m.UserName + "&" + m.ProductFlowName + "&" 
                                  + m.DailyReportDate.ToShortDateString() + "&" + m.ClassType;
                if (m.MachineId != null || m.MachineId != string.Empty)
                { m.ParamenterKey = m.ParamenterKey + "&" + m.MachineId; }
            });

            return irep.Insert(modelList).ToOpResult(OpContext);
        }

        /// <summary>
        /// 根据组合值删除符合条件的数据
        /// </summary>
        /// <param name="paramenterKey"></param>
        /// <returns></returns>
        public OpResult DeleteDailyReportListBy(string paramenterKey)
        {
            //TODO：根据组合值清除符合条件的列表
            return irep.Delete (e=>e.ParamenterKey ==paramenterKey ).ToOpResult (OpContext);
        }

       /// <summary>
       /// 从临时日报表中获取日报表
       /// </summary>
       /// <param name="department">部门</param>
       /// <returns></returns>
        public List<DailyReportModel> GetTempDailyReportModel(string department)
        {
            return irep.Entities.Where(e => e.Department == department).ToList();
        }
    }


}
