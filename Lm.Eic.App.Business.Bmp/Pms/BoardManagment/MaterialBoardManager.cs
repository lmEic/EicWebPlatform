using Lm.Eic.App.DomainModel.Bpm.Pms.BoardManagment;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.Erp.Bussiness.MocManage;
using Lm.Eic.App.Erp.Domain.MocManageModel.OrderManageModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Lm.Eic.App.Business.Bmp.Pms.BoardManagment
{
    public class MaterialBoardManager
    {

        List<BomMaterialModel> _bomMaterialList = new List<BomMaterialModel>();

        #region Find

        /// <summary>
        /// 获取物料规格看板
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Image GetMaterialSpecBoardBy(string orderId)
        {
            //TODO ：根据工单号获取产品品号 =》依据产品品号查找看板 =》根据看板的线材品号 在工单的物料BOM中查找 =>只有存在该线材才能通过

            //根据工单号获取Erp中的工单信息
            var orderDetails = MocService.OrderManage.GetOrderDetails(orderId);
            var orderMaterialList = MocService.OrderManage.GetOrderMaterialList(orderId);

            //依据产品品号查找看板 
            var materialBoard = BorardCrudFactory.MaterialBoardCrud.FindMaterialSpecBoardBy(orderDetails.ProductID);
            if (materialBoard == null)
                return null;

            //得到工单中的所有料号
            var orderMaterialIdList = new List<string>();
            orderMaterialList.ForEach(e => { orderMaterialIdList.Add(e.MaterialId); });
            
            //看板的所有料号是否都能找到
            if (ContainsMaterialId(orderMaterialIdList, materialBoard.MaterialID))
                return BuildImage(materialBoard.DocumentPath.Replace("/", @"\"), string.Format("工单单号:{0}  批量：{1}", orderDetails.OrderId, orderDetails.Count));

            return null;
        }
        /// <summary>
        /// 获取待审核的看板列表
        /// </summary>
        /// <returns></returns>
        public List<MaterialSpecBoardModel> GetWaittingAuditBoardList()
        {
            return BorardCrudFactory.MaterialBoardCrud.GetWaittingAuditBoardList();
        }
        /// <summary>
        /// 物料是否存在
        /// </summary>
        /// <param name="productId">产品品号</param>
        /// <param name="materialId">物料编号 多个物料请用逗号分隔</param>
        /// <returns></returns>
        public OpResult CheckMaterialIdMatchProductId(string materialId ,string productId)
        {
            if (!ContainsProductId(productId))
                return OpResult.SetResult("未找到输入的产品品号！");
            if (!ContainsMaterialId(materialId))
                return OpResult.SetResult("未在BOM中找到料号");

            return OpResult.SetResult("", true);
        }

        #endregion


        #region Insert

        /// <summary>
        /// 添加一个物料规格看板
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult AddMaterialSpecBoard(MaterialSpecBoardModel model)
        {
            model.State = "待审核";
            var viefyResult = CheckMaterialIdMatchProductId(model.ProductID, model.MaterialID);
            return viefyResult.Result ? BorardCrudFactory.MaterialBoardCrud.Store(model) : viefyResult;
        }
        /// <summary>
        /// 审核看板
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult AffirmMaterialBoard(MaterialSpecBoardModel model)
        {
            model.OpSign = OpMode.Edit;
            model.State = "已审核";
            return BorardCrudFactory.MaterialBoardCrud.Store(model);
        }

        #endregion


        #region private Methods
      
        public Image BuildImage(string strPatch,string context)
        {
            try
            {
                Image myImage = Image.FromFile(strPatch);
                ////创建一个画布
                //int mapWidth = myImage.Width;
                //int mapHeight = myImage.Height + 30;
                //Bitmap map = new Bitmap(mapWidth, mapHeight);

                ////GDI+ 绘图 将文字与图片合成
                //Graphics graphics = Graphics.FromImage(map);
                //graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                //graphics.Clear(Color.White);
                //graphics.DrawString(context,
                //  new Font("宋体", 15),
                //  new SolidBrush(Color.Black),
                //  new PointF(0, 5));
                //graphics.DrawImage(myImage, new PointF(0, 30));

                ////释放缓存 
                //graphics.Dispose();
                //myImage.Dispose();
                //return tem;
                return myImage;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Erp中是否存在产品品号 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        private bool ContainsProductId(string productId)
        {
            _bomMaterialList = MocService.BomManage.GetBomMaterialList(productId);
            return _bomMaterialList != null && _bomMaterialList.Count >= 1;
        }
        /// <summary>
        /// BOM表中是否存在物料编号
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        private bool ContainsMaterialId(string materialId)
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
     
        #endregion

    }
}
