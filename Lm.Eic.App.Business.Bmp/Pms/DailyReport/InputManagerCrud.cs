using Lm.Eic.App.DbAccess.Bpm.Repository.PmsRep.DailyReport;
using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
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
        /// 添加日报数据列表
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult AddDailyReportList(List<DailyReportModel> modelList)
        {
            //添加模板列表       要求：一次保存整个列表
            return null;
        }

        /// <summary>
        /// 根据组合值删除符合条件的数据
        /// </summary>
        /// <param name="paramenterKey"></param>
        /// <returns></returns>
        public OpResult DeleteDailyReportListBy(string paramenterKey)
        {
            //TODO：根据组合值清除符合条件的列表
            return null;
        }

        #region Store

        protected override void AddCrudOpItems()
        {
            AddOpItem(OpMode.Add, AddDailyReportModel);
            AddOpItem(OpMode.Edit, EditDailyReportModel);
            AddOpItem(OpMode.Delete, DeleteDailyReportModel);
        }


        private OpResult AddDailyReportModel(DailyReportModel model)
        {

            return irep.Insert(model).ToOpResult_Add(OpContext);
        }

        private OpResult EditDailyReportModel(DailyReportModel model)
        {
            ////添加条件
            return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);

        }
        private OpResult DeleteDailyReportModel(DailyReportModel model)
        {
            return (model.Id_Key > 0) ?
                 irep.Delete(u => u.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext) :
                 OpResult.SetResult("未执行任何操作");
        }

        #endregion

    }


    /// <summary>
    /// 日报临时库 CRUD
    /// </summary>
    public class DailyReportTempCrud : CrudBase<DailyReportModel, IDailyReportTempRepository>
    {
        public DailyReportTempCrud(IDailyReportTempRepository repository, string opContext) : base(repository, opContext)
        {
        }

        protected override void AddCrudOpItems()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 添加日报数据列表
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult AddDailyReportList(List<DailyReportModel> modelList)
        {
            //添加模板列表       要求：一次保存整个列表
            return null;
        }

        /// <summary>
        /// 根据组合值删除符合条件的数据
        /// </summary>
        /// <param name="paramenterKey"></param>
        /// <returns></returns>
        public OpResult DeleteDailyReportListBy(string paramenterKey)
        {
            //TODO：根据组合值清除符合条件的列表
            return null;
        }


    }


}
