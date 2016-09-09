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
        /// <summary>
        /// 日报模板
        /// </summary>
        public static DailyReportTemplateCrud DailyReportTemplateCrud
        { get { return OBulider.BuildInstance<DailyReportTemplateCrud>(); } }

    }


    /// <summary>
    /// 日报录入CRUD
    /// </summary>
    public class DailyReportInputCrud : CrudBase<DailyReportModel, IDailyReportRepositoryRepository>
    {
        public DailyReportInputCrud() : base(new DailyReportRepositoryRepository(), "日报录入")
        {
        }

        public OpResult AddDailyReportList(List<DailyReportModel> modelList)
        {
            //添加模板列表       要求：一次保存整个列表
            return DailyDailyReportOp(modelList, "add");
        }
        /// <summary>
        /// 日报模块操作
        /// </summary>
        /// <param name="modelList">日报模块列表</param>
        /// <param name="opSign">操作标识</param>
        /// <returns></returns>
        private OpResult DailyDailyReportOp(List<DailyReportModel> modelList, string opSign)
        {
            try
            {
                if (modelList != null && modelList.Count > 0)
                {
                    int errCout = 0;
                    modelList.ForEach((model) =>
                    {
                        model.OpSign = opSign;
                        OpResult deleteResult = Store(model);
                        if (!deleteResult.Result) errCout++;
                    });
                    int total = modelList.Count;
                    int victory = total - errCout;
                    return errCout > 0 ?
                        OpResult.SetResult(string.Format("{0}失败！ 总数：{1} 成功:{2} 失败:{1}",
                        opSign, total, victory, errCout)) : OpResult.SetResult(string.Format("{0}记录数{1}", opSign, total), true);
                }
                else return OpResult.SetResult("列表为空");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

        }
        protected override void AddCrudOpItems()
        {
            AddOpItem(OpMode.Add, AddDailyReportModel);
            AddOpItem(OpMode.Edit, EditDailyReportModel);
            AddOpItem(OpMode.Delete, DeleteDailyReportModel);
        }


        private OpResult AddDailyReportModel(DailyReportModel model)
        {

            return irep.Insert(model).ToOpResult_Add (OpContext);
        }

        private OpResult EditDailyReportModel(DailyReportModel model)
        {
           ////添加条件
            return irep.Update(u => u.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);

        }
        private OpResult DeleteDailyReportModel(DailyReportModel model)
        {
            return(model.Id_Key > 0) ?
                 irep.Delete(u => u.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext) :
                 OpResult.SetResult("未执行任何操作");
        }
    }

    /// <summary>
    /// 日报模板CRUD
    /// </summary>
    public class DailyReportTemplateCrud : CrudBase<DailyReportTemplateModel, IDailyReportTemplateRepositoryRepository>
    {
        public DailyReportTemplateCrud() : base(new DailyReportTemplateRepositoryRepository(), "日报模板")
        {
        }

        protected override void AddCrudOpItems() { }

        /// <summary>
        /// 添加模板
        /// </summary>
        /// <param name="modelList">模板列表</param>
        /// <returns></returns>
        public OpResult AddTemplateList(List<DailyReportTemplateModel> modelList)
        {
            SetFixFieldValue(modelList, OpMode.Add);
            return irep.Insert(modelList).ToOpResult_Add(OpContext);
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public OpResult DeleteTemplateListBy(string department)
        {
            return irep.Delete(m => m.Department == department).ToOpResult_Delete(OpContext);
        }
 
        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        public List<DailyReportTemplateModel> GetTemplateListBy(string department)
        {
            return irep.Entities.Where(e => e.Department == department).ToList();
        }

    }

}
