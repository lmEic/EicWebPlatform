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
            MaterialSpecBoardModel MaterialSpecBoardModel=new MaterialSpecBoardModel  ();
            //初始化
            MocService.OrderManage.SetOrderId(orderId);
            //  工单信息
            var orderInfo = MocService.OrderManage.GetOrderDetails();
            // 工单对应的物料信息
            var orderManterilInfo = MocService.OrderManage.GetOrderMaterialList();
             //得到Bom表中的所以料号
            List<string> materialBom = new List<string>();
            orderManterilInfo.ForEach(e => { materialBom.Add(e.MaterialId); });

            //   依据产品品号查找看板 
            var  BoardCrudinfo=  BorardCrudFactory.BoardCrud.FindMaterialSpecBoardBy(orderInfo.ProductID).FirstOrDefault();
            //    看板的所有物号 是否在Bom的料号中 能找到
            if (IsHaveBoard(materialBom, BoardCrudinfo.MaterialID))
            {
                MaterialSpecBoardModel = BoardCrudinfo;
            }
           return MaterialSpecBoardModel;
        }


        private bool IsHaveBoard(List<string> materialBom,string materialID)
        {
            bool retrurnBoll = true ;
            if (materialID.Contains(","))
            {
                string[] materials = materialID.Split(',');
                if (materials.Count() > 0)
                {
                    foreach (string i in materials)
                    {
                        // 在Bom的料号中 是否都包含看板料号 只要有-项不包含 返回false
                        if (!materialBom.Contains(i))
                        {
                            retrurnBoll = false;
                            break;
                        }
                    }
                    return retrurnBoll;
                }
                else return false;
            }
            else
            {
                if (materialBom.Contains(materialID))
                    return retrurnBoll;
                else return false;
            }

        }
        /// <summary>
        /// 仓储操作 model.OpSign = add/edit/delete
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult Store(MaterialSpecBoardModel model)
        {
            //TODO ：依据产品品号为唯一值进行看板的存储 线材料号可以以 “，” 分隔的形式存储多个线材料号 实现CRUD
            return BorardCrudFactory.BoardCrud.Store (model);
        }
    }

}
