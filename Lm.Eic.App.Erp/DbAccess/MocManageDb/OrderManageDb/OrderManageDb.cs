using Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Erp.DbAccess.MocManageDb.OrderManageDb
{
    public class OrderDetailsDb
    {
        /// <summary>
        /// 获取工单详情
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public OrderModel GetOrderDetailsBy(string orderID)
        {
            return null;
        }
    }

    public class OrderMaterialDb
    {
        /// <summary>
        /// 获取工单物料列表
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<MaterialModel> GetOrderMaterialListBy(string orderId)
        {
            return null;
        }

    }
}
