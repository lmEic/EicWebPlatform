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
using System.IO;
using System.Drawing.Imaging;

namespace Lm.Eic.App.Business.Bmp.Pms.BoardManagment
{
    public class MaterialBoardManager
    {

        List<BomMaterialModel> _bomMaterialList = new List<BomMaterialModel>();

        #region Find
        /// <summary>
        /// 获取物料规格看板
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="orderId"></param>
        /// <param name="shipmentDate"></param>
        /// <param name="shipmentCount"></param>
        /// <returns></returns>
        public Image GetMaterialSpecBoardBy(string rootPath, string orderId, string shipmentDate, string shipmentCount)
        {

            //TODO ：根据工单号获取产品品号 =》依据产品品号查找看板 =》根据看板的线材品号 在工单的物料BOM中查找 =>只有存在该线材才能通过

            //根据工单号获取Erp中的工单信息
            var orderDetails = MocService.OrderManage.GetOrderDetails(orderId);
            var orderMaterialList = MocService.OrderManage.GetOrderMaterialList(orderId);

            if (orderDetails == null)
                return BuildImageErr("未找到此工单");
                            
            //依据产品品号查找看板 
            var materialBoard = BorardCrudFactory.MaterialBoardCrud.FindMaterialSpecBoardBy(orderDetails.ProductID);
            if (materialBoard == null)
                return BuildImageErr("未找到看板");

            if (materialBoard.State == "待审核")
                return BuildImageErr("看板未审核");

            //得到工单中的所有料号
            var orderMaterialIdList = new List<string>();
            orderMaterialList.ForEach(e => { orderMaterialIdList.Add(e.MaterialId); });

            //看板的所有料号是否都能找到
            if (!ContainsMaterialId(orderMaterialIdList, materialBoard.MaterialID))
                return BuildImageErr("物料不存在与工单");

            return BuildImage(string.Format(@"{0}{1}",rootPath, materialBoard.DocumentPath.Replace("/", @"\")),
                string.Format("工单单号:{0} 出货日期：{1}  批量：{2}", orderDetails.OrderId, shipmentDate, shipmentCount));
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
        public OpResult CheckMaterialIdMatchProductId(string materialId, string productId)
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
            //依据产品品号查找看板 
            var materialBoard = BorardCrudFactory.MaterialBoardCrud.FindMaterialSpecBoardBy(model.ProductID);
            model.OpSign = materialBoard == null ? OpMode.Add : OpMode.Edit;
            model.State = "待审核";
            var viefyResult = CheckMaterialIdMatchProductId(model.MaterialID, model.ProductID);
            return viefyResult.Result ? BorardCrudFactory.MaterialBoardCrud.Store(model) : viefyResult;
        }

        /// <summary>
        /// 审核看板
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult AuditMaterialBoard(MaterialSpecBoardModel model)
        {
            return BorardCrudFactory.MaterialBoardCrud.AuditMaterialBoard(model);
        }

        #endregion


        #region private Methods

        /// <summary>
        /// 生成看板图片
        /// </summary>
        /// <param name="strPatch">图片路径</param>
        /// <param name="context"></param>
        /// <returns></returns>
        private  Image BuildImage(string strPatch, string context)
        {
            try
            {
                Image myImage = Image.FromFile(strPatch);
                //创建一个画布
                int mapWidth = myImage.Width;
                int mapHeight = myImage.Height + 30;
                Bitmap map = new Bitmap(mapWidth, mapHeight);

                //GDI+ 绘图 将文字与图片合成
                Graphics graphics = Graphics.FromImage(map);
                graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                graphics.Clear(Color.White);
                graphics.DrawString(context,
                  new Font("宋体", 10),
                  new SolidBrush(Color.Black),
                  new PointF(0, 5));

                Point ulCorner = new Point(0, 30);
                Point urCorner = new Point(mapWidth, 30);
                Point llCorner = new Point(0, mapHeight);
                Point[] destPara = { ulCorner, urCorner, llCorner };
                graphics.DrawImage(myImage, destPara);

                //存储到流 进行格式转化
                MemoryStream bmpStream = new MemoryStream();
                map.Save(bmpStream, myImage.RawFormat);
                Image resultImage = Bitmap.FromStream(bmpStream);

                //释放缓存 
                graphics.Dispose();
                myImage.Dispose();
                return resultImage;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }


        /// <summary>
        /// 生成错误看板图片
        /// </summary>
        /// <param name="errMessage"></param>
        /// <returns></returns>
        private Image BuildImageErr(string errMessage)
        {
            //创建一个画布
            Bitmap map = new Bitmap(400, 300);
            //GDI+ 绘图 将文字与图片合成
            Graphics graphics = Graphics.FromImage(map);
            graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            graphics.Clear(Color.White);
            graphics.DrawString(errMessage,
              new Font("宋体", 18),
              new SolidBrush(Color.Black),
              new PointF(100, 140));

            //存储到流 进行格式转化
            MemoryStream bmpStream = new MemoryStream();
            map.Save(bmpStream, ImageFormat.Png);
            Image resultImage = Bitmap.FromStream(bmpStream);

            //释放缓存 
            graphics.Dispose();
            map.Dispose();
            return resultImage;
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
