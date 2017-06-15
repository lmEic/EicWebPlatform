using Lm.Eic.Framework.ProductMaster.Model.Tools;
using System;
using System.Collections.Generic;
using Lm.Eic.Uti.Common.YleeOOMapper;
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
        List<WorkTaskManageModel> _workTaskManageModelList = new List<WorkTaskManageModel>();
        public List<WorkTaskManageModel> GetWorkTaskDatasBy(QueryWorkTaskManageDto queryDto)
        {
            _workTaskManageModelList = tlOnlineCrudFactorty.WorkCrud.FindBy(queryDto);
            return _workTaskManageModelList;     
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
                return tlOnlineCrudFactorty.WorkCrud.Store(model);
            }
            catch (System.Exception ex)
            {
                return ex.ExOpResult();
            }

        }







    }
}

   
