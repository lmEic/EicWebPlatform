using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.Business.Bmp.Pms.BoardManagment;
using Lm.Eic.App.DomainModel.Bpm.Pms.BoardManagment;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;

namespace EicWorkPlatfrom.Controllers.Product
{
    /// <summary>
    /// 生产看板管理器
    /// </summary>
    public class ProBoardController : EicBaseController
    {
        //
        // GET: /ProBoard/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 线材看板控制器
        /// </summary>
        /// <returns></returns>
        public ActionResult JumperWireBoard()
        {
            return View();
        }
        /// <summary>
        /// 上传看板文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult UploadMaterialBoardFile(HttpPostedFileBase file)
        {
            var result = 0;
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    ///待加入验证文件名称逻辑:
                    string fileName = Path.Combine(this.CombinedFilePath("FileLibrary", "TwoMaterialBoard"), file.FileName);
                    file.SaveAs(fileName);
                    result = 1;
                }
            }
            return Json(result);
        }
        /// <summary>
        /// 检测物料料号是否与产品料号相匹配
        /// </summary>
        /// <param name="materialId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult CheckMaterialIdMatchProductId(string materialId, string productId)
        {
            var result = BoardService.MaterialBoardManager.CheckMaterialIdMatchProductId(productId,materialId);
            return Json(result);
        }
        /// <summary>
        /// 添加物料看板规格记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult AddMaterialSpecBoardRecord(MaterialSpecBoardModel model)
        {
            var result = BoardService.MaterialBoardManager.AddMaterialSpecBoard(model);
            return Json(result);
        }

        [NoAuthenCheck]
        public ActionResult GetMaterialSpecBoardBy(string orderId)
        {
            var image = BoardService.MaterialBoardManager.BuildImage(@"E:\sss.jpg", "出货数量");
            return this.ImageResult(image);
        }
    }
}
