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
        /// 获取开发任务列表 
        /// </summary>
        /// <param name="progressSignList">进度标识列表</param>
        /// <returns></returns>
        public List<ItilDevelopModuleManageModel> GetDevelopModuleManageListBy(List<string> progressSignList)
        {
            return ItilDevelopModuleManageCrudFactory.ItilDevelopModuleManageCrud.GetDevelopModuleManageListBy(progressSignList);
        }

        /// <summary>
        /// 仓储操作 model.OpSign = add/edit/delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(ItilDevelopModuleManageModel model)
        {
            return ItilDevelopModuleManageCrudFactory.ItilDevelopModuleManageCrud.Store(model);
        }

        /// <summary>
        /// 发送邮件通知
        /// </summary>
        /// <returns></returns>
        public OpResult SendMail()
        {
            return ItilDevelopModuleManageCrudFactory.ItilDevelopModuleManageCrud.SendMail();
        }
    }



}
