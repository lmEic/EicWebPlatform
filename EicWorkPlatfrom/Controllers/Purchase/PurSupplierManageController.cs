using Lm.Eic.App.Business.Bmp.Purchase;
using Lm.Eic.App.DomainModel.Bpm.Purchase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EicWorkPlatfrom.Controllers.Purchase
{
    public class PurSupplierManageController : EicBaseController
    {
        //
        // GET: /PurSupplierManage/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 供应商录入
        /// </summary>
        /// <returns></returns>
        public ActionResult PurSupplierInput()
        {
            return View();
        }

        /// <summary>
        /// 建立合格供应商清册
        /// </summary>
        /// <returns></returns>
        public ActionResult BuildQualifiedSupplierInventory()
        {
            return View();
        }


        /// <summary>
        /// 获取合格供应商列表
        /// </summary>
        /// <param name="yearStr">年份</param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetPurQualifiedSupplierListBy(string yearStr)
        {
            var datas = PurchaseService.PurSupplierManager.FindQualifiedSupplierList(yearStr);
            return DateJsonResult(datas);
        }

        /// <summary>
        /// 获取合格供应商信息
        /// </summary>
        /// <param name="supplierId">供应商编号</param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetErpSuppplierInfoBy(string supplierId)
        {
            var datas = PurchaseService.PurSupplierManager.GetSuppplierInfoBy(supplierId);
            return DateJsonResult(datas);
        }

        /// <summary>
        /// 编辑供应商证书模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public ActionResult EditPurSupplierCertificateViewTpl()
        {
            return View();
        }
        /// <summary>
        /// 上传采购供应商证书文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult UploadPurSupplierCertificateFile(HttpPostedFileBase file)
        {
            var result = 0;
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    string year = DateTime.Now.Year.ToString();///按年份进行存储
                    ///待加入验证文件名称逻辑:
                    string fileName = Path.Combine(this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.PurSupplierCertificate,year), file.FileName);
                    file.SaveAs(fileName);
                    result = 1;
                }
            }
            return Json(result);
        }
        /// <summary>
        /// 保存供应商证书信息
        /// </summary>
        /// <param name="certificateDatas"></param>
        /// <returns></returns>
        [HttpPost]
        [NoAuthenCheck]
        public JsonResult StorePurSupplierCertificateInfo(List<InPutSupplieCertificateInfoModel> certificateDatas)
        {
            var opResult = PurchaseService.PurSupplierManager.SavaEditSpplierCertificate(certificateDatas);
            return Json(opResult);
        }
    }
}
