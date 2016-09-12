
using Lm.Eic.App.Erp.DbAccess.MocManageDb.OrderManageDb;
using Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Erp.Bussiness.MocManage
{
    /// <summary>
    /// 工单管理
    /// </summary>
    public class OrderManage
    {
        public OrderManage() { }
       
        /// <summary>
        /// 获取工单详情
        /// </summary>
        /// <returns></returns>
        public OrderModel GetOrderDetails(string orderId)
        {
            return OrderCrudFactory.OrderDetailsDb.GetOrderDetailsBy(orderId);
        }
        /// <summary>
        /// 获取工单物料列表
        /// </summary>
        /// <returns></returns>
        public List<OrderMaterialModel> GetOrderMaterialList(string orderId)
        {
            return OrderCrudFactory.OrderMaterialDb.GetOrderMaterialListBy(orderId);
        }

    }
}
