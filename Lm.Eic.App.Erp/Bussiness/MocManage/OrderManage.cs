
using Lm.Eic.App.Erp.DbAccess.MocManageDb.OrderManageDb;
using Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Erp.Bussiness.MocManage.OrderManage
{
    /// <summary>
    /// 工单管理Crud工厂
    /// </summary>
    internal class OrderCrudFactory
    {
        /// <summary>
        /// 工单详情Crud
        /// </summary>
        public static OrderDetailsDb OrderDetailsDb
        {
            get { return OBulider.BuildInstance<OrderDetailsDb>(); }
        }

        /// <summary>
        /// 工单物料Crud
        /// </summary>
        public static OrderMaterialDb OrderMaterialDb
        {
            get { return OBulider.BuildInstance<OrderMaterialDb>(); }
        }
    }

    /// <summary>
    /// 工单管理
    /// </summary>
    public class OrderManage
    {
        /// <summary>
        /// 初始化一个工单
        /// </summary>
        /// <param name="orderId"></param>
        public OrderManage(string orderId)
        {
            this._orderId = orderId;
        }
        /// <summary>
        /// 全局变量 OrderID
        /// </summary>
        string _orderId = string.Empty;

        /// <summary>
        /// 获取工单详情
        /// </summary>
        /// <returns></returns>
        public OrderModel GetOrderDetails()
        {
            return OrderCrudFactory.OrderDetailsDb.GetOrderDetailsBy(_orderId);
        }
        /// <summary>
        /// 获取工单物料列表
        /// </summary>
        /// <returns></returns>
        public List<MaterialModel> GetOrderMaterialList()
        {
            return OrderCrudFactory.OrderMaterialDb.GetOrderMaterialListBy(_orderId);
        }
    }


    public class test
    {
        OrderManage order = new OrderManage("512-1607067");

        public void gte()
        {
            var tttt = order.GetOrderDetails();
            var ttttList = order.GetOrderMaterialList();
        }
}
}
