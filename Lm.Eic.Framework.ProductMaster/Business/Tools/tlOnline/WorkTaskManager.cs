using Lm.Eic.Framework.ProductMaster.Model.Tools;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeOOMapper;
using CrudFactory = Lm.Eic.Framework.ProductMaster.Business.Tools.tlOnline.WorkTaskManageFactory;


namespace Lm.Eic.Framework.ProductMaster.Business.Tools.tlOnline
{
    /// <summary>
    /// 工作任务管理器
    /// </summary>
   public  class WorkTaskManager
    {
        List<WorkTaskManageModel> _workTaskManageModelsList = new List<WorkTaskManageModel>();
        List<FileFieldMapping> filemapping = new List<FileFieldMapping>()
        {
          new FileFieldMapping ("Department","部门"),
          new FileFieldMapping ("SystemName","系统名称"),
          new FileFieldMapping ("ModuleName","模块名称"),
          new FileFieldMapping ("WorkItem","具体功能"),
          new FileFieldMapping ("WorkDescription","功能描述"),
          new FileFieldMapping ("DifficultyCoefficient","难度系数"),
          new FileFieldMapping ("WorkPriority","优先级"),
          new FileFieldMapping ("StartDate","开始日期"),
          new FileFieldMapping ("EndDate","完成日期"),
          new FileFieldMapping ("ProgressStatus","进度状态"),
          new FileFieldMapping ("ProgressDescription","进度描述"),
          new FileFieldMapping ("OrderPerson","执行人"),
          new FileFieldMapping ("CheckPerson","审核人"),
          new FileFieldMapping ("Remark","备注")
          
        };
        //public List<WorkTaskManageModel>FindRecordBy(QueryWorkTaskManageDto  dto)
        //{
        //    _workTaskManageModelsList = CrudFactory.WorkTaskManageCrud.FindBy(dto);
        //    return _workTaskManageModelsList;
        }
       //public OpResult StoreWorkTaskManage(WorkTaskManageModel model)
       // {
       //    // return CrudFactory.WorkTaskManageCrud.Store(model);
       // }
        

    }

    internal class WorkTaskManageCrud
    {
        //public WorkTaskManageCrud()
        //    :base(new work)
    }

