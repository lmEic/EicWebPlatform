using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using System.Collections.Generic;
using Lm.Eic.Uti.Common.YleeOOMapper;

namespace Lm.Eic.Framework.ProductMaster.Business.Itil
{
    public class ItilDevelopModuleManager
    {
        /// <summary>
        /// 获取开发任务列表  1.依据状态列表查询 2.依据函数名称查询 
        /// </summary>
        /// <param name="progressSignList">进度标识列表</param>
        /// <returns></returns>
        public List<ItilDevelopModuleManageModel> GetDevelopModuleManageListBy(ItilDto dto)
        {
            return ItilCrudFactory.ItilDevelopModuleManageCrud.GetDevelopModuleManageListBy(dto);
        }
        /// <summary>
        /// 仓储操作 model.OpSign = add/edit/delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(ItilDevelopModuleManageModel model)
        {
            return ItilCrudFactory.ItilDevelopModuleManageCrud.Store(model);
        }
        /// <summary>
        /// 修改开发进度
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        public OpResult ChangeProgressStatus(ItilDevelopModuleManageModel model)
        {
            return ItilCrudFactory.ItilDevelopModuleManageCrud.ChangeProgressStatus(model);
        }
        /// <summary>
        /// 获取开发任务进度变更明细
        /// </summary>
        /// <param name="model">开发任务</param>
        /// <returns></returns>
        public List<ItilDevelopModuleManageChangeRecordModel> GetChangeRecordListBy(ItilDevelopModuleManageModel model)
        {
            return ItilCrudFactory.ItilDevelopModuleManageCrud.GetChangeRecordListBy(model);
        }
        /// <summary>
        /// 发送邮件通知 
        /// </summary>
        /// <param name="receiveUserList">接收人列表</param>
        /// <returns></returns>
        public OpResult SendMail()
        {
            return ItilCrudFactory.ItilDevelopModuleManageCrud.SendMail();
        }
    }



}
