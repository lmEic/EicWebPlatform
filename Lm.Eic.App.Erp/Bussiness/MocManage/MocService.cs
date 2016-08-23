using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.Erp.DbAccess.MocManageDb.OrderManageDb;
using Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel;
using Lm.Eic.Uti.Common.YleeObjectBuilder;


namespace Lm.Eic.App.Erp.Bussiness.MocManage
{
    public static class MocService
    {
        /// <summary>
        /// 工单管理器
        /// </summary>
        public static OrderManage OrderManage
        {
            get { return OBulider.BuildInstance<OrderManage>(); }
        }

        public static BomManage BomManage
        {
            get { return OBulider.BuildInstance<BomManage>(); }
        }
    }
}
