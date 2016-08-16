using Lm.Eic.App.DomainModel.Bpm.Pms.BoardManager;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.BoardManagment
{
   public  class MaterialBoardManager
    {
        /// <summary>
        /// 获取物料规格看板
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public MaterialSpecBoardModel GetMaterialSpecBoardBy(string orderId)
        {
            MaterialSpecBoardModel model = new MaterialSpecBoardModel();
            
            //TODO ：根据工单号获取产品品号 =》依据产品品号查找看板 =》根据看板的线材品号 在工单的物料BOM中查找 =>只有存在该线材才能通过
            return model;
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
