using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Framework.ProductMaster.Model.Tools;
using static Lm.Eic.Framework.ProductMaster.Model.Tools.WorkTaskManageModel;
using Lm.Eic.Framework.ProductMaster.DbAccess.Repository;

namespace Lm.Eic.Framework.ProductMaster.Business.Tools.tlOnline
{
   internal class WorkTaskCrudFactorty
    {
        

    }
    internal class WorkTaskManageCrud : CrudBase<WorkTaskManageModel, IWorkTaskRepository>
    {
        public WorkTaskManageCrud() : base(new WorkTaskRepository(), "工作任务列表")
        {
        }
        #region 工作任务Crud操作
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, Add);
            this.AddOpItem(OpMode.Edit, Edit);
            this.AddOpItem(OpMode.Delete, UpdateIsDelete); ;
        }
        OpResult Add(WorkTaskManageModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
        OpResult Edit(WorkTaskManageModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        OpResult UpdateIsDelete(WorkTaskManageModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, s => new WorkTaskManageModel { IsDelete = model.IsDelete }).ToOpResult_Delete(OpContext);
        }
        #endregion

        #region 分类查询
        /// <summary>
        ///部门查找
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        internal List<WorkTaskManageModel> GetWorkTaskDatasBy(string department)
        {
            return irep.Entities.Where(e => e.Department == department).ToList();
        }

        /// <summary>
        ///分类查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        internal List<WorkTaskManageModel> FindBy(QueryWorkTaskManageDto queryDto)
        {          
                try
                {
                    if (queryDto.Department == null || queryDto.Department == string.Empty)
                    return new List<WorkTaskManageModel>();
                queryDto.IsDelete = 0;
                switch (queryDto.SearchMode)
                {
                    ///部门
                    case 0:
                        return irep.Entities.Where(e => e.IsDelete == queryDto.IsDelete && e.Department == queryDto.Department).ToList();

                    ///系统名称
                    case 1:
                        return irep.Entities.Where(e => e.IsDelete == queryDto.IsDelete && e.Department == queryDto.Department && e.SystemName.Contains(queryDto.QueryContent)).ToList();

                    ///模块类别
                    case 2:
                        return irep.Entities.Where(e => e.IsDelete == queryDto.IsDelete && e.Department == queryDto.Department && e.ModuleName.Contains(queryDto.QueryContent)).ToList();

                    ///具体功能
                    case 3:
                        return irep.Entities.Where(e => e.IsDelete == queryDto.IsDelete && e.Department == queryDto.Department && e.WorkItem.Contains(queryDto.QueryContent)).ToList();

                    ///进度状态
                    case 4:
                        return irep.Entities.Where(e => e.IsDelete == queryDto.IsDelete && e.Department == queryDto.Department && e.ProgressStatus.Contains(queryDto.QueryContent)).ToList();

                    ///执行人
                    case 5:
                        return irep.Entities.Where(e => e.IsDelete == queryDto.IsDelete && e.Department == queryDto.Department && e.OrderPerson.Contains(queryDto.QueryContent)).ToList();

                    ///审核人
                    case 6:
                        return irep.Entities.Where(e => e.IsDelete == queryDto.IsDelete && e.Department == queryDto.Department && e.CheckPerson.Contains(queryDto.QueryContent)).ToList();
                    default: return new List<WorkTaskManageModel>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            #endregion
        }
    }
}
