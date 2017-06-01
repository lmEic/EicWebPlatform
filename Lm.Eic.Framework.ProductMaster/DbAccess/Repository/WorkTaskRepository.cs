using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Framework.ProductMaster.DbAccess.Mapping;
using Lm.Eic.Framework.ProductMaster.Model.Tools;
using Lm.Eic.Uti.Common.YleeDbHandler;

namespace Lm.Eic.Framework.ProductMaster.DbAccess.Repository
{
    public interface IWorkTaskRepository : IRepository<WorkTaskManageModel> { }
    /// <summary>
    /// 工作任务技久化
    /// </summary>
    public class WorkTaskRepository : LmProMasterRepositoryBase<WorkTaskManageModel>, IWorkTaskRepository { }

}
