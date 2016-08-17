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
            //TODO ：根据工单号获取产品品号 =》依据产品品号查找看板 =》根据看板的线材品号 在工单的物料BOM中查找 =>只有存在该线材才能通过

            //根据工单号获取Erp中的工单信息
            MocService.OrderManage.SetOrderId(orderId);
            var orderDetails = MocService.OrderManage.GetOrderDetails();
            var orderMaterialList = MocService.OrderManage.GetOrderMaterialList();

            //依据产品品号查找看板 
            var materialBoard = BorardCrudFactory.MaterialBoardCrud.FindMaterialSpecBoardBy(orderDetails.ProductID);

            //得到Bom表中的所以料号
            var orderMaterialIdList = new List<string>();
            orderMaterialList.ForEach(e => { orderMaterialIdList.Add(e.MaterialId); });

            //看板的所有物号 是否在Bom的料号中 能找到
            if (IsContainsMaterialId(orderMaterialIdList, materialBoard.MaterialID))
            {
                return materialBoard;
            }
            return null;
        }
        /// <summary>
        /// 工单物料是否包含此物料
        /// </summary>
        /// <param name="orderMaterialIdList"></param>
        /// <param name="materialID"></param>
        /// <returns></returns>
        private bool IsContainsMaterialId(List<string> orderMaterialIdList, string materialID)
        {
            //如果只有一个料号
            if (!materialID.Contains(","))
                return orderMaterialIdList.Contains(materialID);

            //如果有多个料号
            string[] materials = materialID.Split(',');
            if (materials == null || materials.Count() < 1)
                return false;
            
            //物料是否都存在与工单物料中
            foreach (var material in materials)
            {
                if (!orderMaterialIdList.Contains(material))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 仓储操作 model.OpSign = add/edit/delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(MaterialSpecBoardModel model)
        {
            //TODO ：依据产品品号为唯一值进行看板的存储 线材料号可以以 “，” 分隔的形式存储多个线材料号 实现CRUD
            return BorardCrudFactory.MaterialBoardCrud.Store (model);
        }
    }

}
