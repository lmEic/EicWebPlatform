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
        internal static WorkTaskManageCrud WorkCrud
        {
            get { return OBulider.BuildInstance<WorkTaskManageCrud>(); }
        }


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
            return irep.Update(e => e.Id_Key == model.Id_Key, s => new WorkTaskManageModel { Id_Key=model.Id_Key }).ToOpResult_Delete(OpContext);
        }
        #endregion

        #region 分类查询      
        /// <summary>
        ///分类查询
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        internal List<WorkTaskManageModel> FindBy(QueryWorkTaskManageDto queryDto)
        {
            if (queryDto == null) return new List<WorkTaskManageModel>();
            try
            {
                
                switch (queryDto.SearchMode)
                {    
                    //按系统名称查询
                    case 1:
                        return irep.Entities.Where(m => m.SystemName==queryDto.SystemName).ToList();
                    //按模块类别查询
                    case 2:
                        return irep.Entities.Where(m => m.ModuleName==queryDto.ModuleName).ToList();
                    default:
                        return new List<WorkTaskManageModel>();
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
