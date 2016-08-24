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

        List<Erp.Domain.MocManageModel.OrderManageModel.MaterialModel> _bomMaterialList = new List<Erp.Domain.MocManageModel.OrderManageModel.MaterialModel>();

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

            //得到工单中的所以料号
            var orderMaterialIdList = new List<string>();
            orderMaterialList.ForEach(e => { orderMaterialIdList.Add(e.MaterialId); });

            //看板的所有物号 是否在Bom的料号中 能找到
            if (ContainsMaterialId(orderMaterialIdList, materialBoard.MaterialID))
            {
                return materialBoard;
            }
            return null;
        }

        /// <summary>
        /// 添加一个物料规格看板
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult AddMaterialSpecBoard(MaterialSpecBoardModel model)
        {
            //产品品号是否存在
            if (!ContainsProductId(model.ProductID))
                return OpResult.SetResult("未在Erp中找到此产品");

            //物料是否存在与BOM
            if (!ContainsMaterialId(model.MaterialID))
                return OpResult.SetResult("未在物料列表中找到指定的物料");
            else
                return BorardCrudFactory.MaterialBoardCrud.Store(model);
        }

        /// <summary>
        /// Erp中是否存在产品品号 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool ContainsProductId(string productId)
        {
            MocService.BomManage.SetProductId(productId);
            _bomMaterialList = MocService.BomManage.GetBomMaterialList();
            return _bomMaterialList != null && _bomMaterialList.Count >= 1;
        }

        /// <summary>
        /// BOM表中是否存在物料编号
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public bool ContainsMaterialId(string materialId)
        {
            //得到Bom中的所有料号
            var bomMaterialIdList = new List<string>();
            _bomMaterialList.ForEach(e => { bomMaterialIdList.Add(e.MaterialId); });

            //看板的所有物号 是否在Bom的料号中  
            return ContainsMaterialId(bomMaterialIdList, materialId);
        }

        /// <summary>
        /// 确定物料是否在物料列表中 如果都在 true 否则 false
        /// </summary>
        /// <param name="orderMaterialIdList"></param>
        /// <param name="materialID"></param>
        /// <returns></returns>
        private bool ContainsMaterialId(List<string> orderMaterialIdList, string materialID)
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

    }
}
