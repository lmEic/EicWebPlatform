using Lm.Eic.Framework.ProductMaster.Model.Tools;
using System;
using System.Collections.Generic;
using Lm.Eic.Uti.Common.YleeOOMapper;
using static Lm.Eic.Framework.ProductMaster.Model.Tools.WorkTaskManageModel;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;

namespace Lm.Eic.Framework.ProductMaster.Business.Tools.tlOnline
{
    /// <summary>
    /// 工作任务管理器
    /// </summary>
    public class WorkTaskManager
    {
        public List<WorkTaskManageModel> GetWorkTaskDatasBy(QueryWorkTaskDto queryDto)
        {
            try
            {
                if (queryDto == null) return new DataList<WorkTaskManageModel>();
                return WorkTaskCrudFactorty.WorkCrud.FindBy(queryDto);

                //_workTaskModelList= WorkTaskCrudFactorty.WorkCrud.FindBy(queryDto);
                //return _workTaskModelList;
            }
            catch (Exception ex)
            {

                return ex.ExDataList<WorkTaskManageModel>();
                //throw new Exception(ex.InnerException.Message);
            }
        }
        /// <summary>
        ///存储数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreTaskData(WorkTaskManageModel model)
        {
            try
            {
                return WorkTaskCrudFactorty.WorkCrud.Store(model);

            }
            catch (System.Exception ex)
            {

                return ex.ExOpResult();
            }

        }







    }
}

   
