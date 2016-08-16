using Lm.Eic.App.DomainModel.Bpm.Pms.BoardManager;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.Erp.Bussiness.MocManage;

namespace Lm.Eic.App.Business.Bmp.Pms.BoardManagment
{
    public class MaterialBoardManager
    {
        /// <summary>
        /// 获取物料规格看板
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public MaterialSpecBoardModel GetMaterialSpecBoardBy(string orderId)
        {
              MaterialSpecBoardModel MaterialSpecBoardModel=new MaterialSpecBoardModel  ();
            //初始化
            MocService.OrderManage.SetOrderId(orderId);
            //  工单信息
            var orderInfo = MocService.OrderManage.GetOrderDetails();
            // 工单对应的物料信息
            var orderManterilInfo = MocService.OrderManage.GetOrderMaterialList();

            //   依据产品品号查找看板 
            var  BoardCrudinfo=  BorardCrudFactory.BoardCrud.FindMaterialSpecBoardBy(orderInfo.ProductID).FirstOrDefault();








            //TODO ：根据工单号获取产品品号 =》依据产品品号查找看板 =》根据看板的线材品号 在工单的物料BOM中查找 =>只有存在该线材才能通过
           return MaterialSpecBoardModel;
        }

        /// <summary>
        /// 仓储操作 model.OpSign = add/edit/delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(MaterialSpecBoardModel model)
        {
            //TODO ：依据产品品号为唯一值进行看板的存储 线材料号可以以 “，” 分隔的形式存储多个线材料号 实现CRUD
            return null;
        }
    }

}
