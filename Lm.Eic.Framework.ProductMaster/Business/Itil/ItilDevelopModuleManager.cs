using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Framework.ProductMaster.DbAccess.Repository;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;

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
        public OpResult SendMail(List<string> receiveUserList)
        {
            return ItilCrudFactory.ItilDevelopModuleManageCrud.SendMail(receiveUserList);
        }
    }



}
