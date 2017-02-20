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

        #region PurQualifiedSupplier
        /// <summary>
        /// 供应商证书管理
        /// </summary>
        /// <returns></returns>
        public ActionResult SupplierCertificateManage()
        {
            return View();
        }
        /// <summary>
        /// 获取合格供应商列表
        /// </summary>
        /// <param name="yearStr">年份</param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetPurQualifiedSupplierListBy(string yearMonth)
        {
            var datas = PurchaseService.PurSupplierManager.SupplierCertificateManager.GetQualifiedSupplierList(yearMonth);
            return DateJsonResult(datas);
        }
        /// <summary>
        /// 导出合格供应商EXCEl表清册
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult CreateQualifiedSupplierInfoList()
        {
            var ds = PurchaseService.PurSupplierManager.SupplierCertificateManager.BuildQualifiedSupplierInfoList();
            return this.ExportToExcel(ds, "合格供应商清单", "合格供应商");
        }
        /// <summary>
        /// 获取合格供应商信息
        /// </summary>
        /// <param name="supplierId">供应商编号</param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetErpSuppplierInfoBy(string supplierId)
        {
            var datas = PurchaseService.PurSupplierManager.SupplierCertificateManager.GetSuppplierInfoBy(supplierId);
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
        /// 获取采购供应商用户数据列表
        /// </summary>
        /// <param name="supplierId"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetPurSupplierDataList(string supplierId, string dataType)
        {
            var datas = 0;
            return Json(datas, JsonRequestBehavior.AllowGet);
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
                    string fileName = Path.Combine(this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.PurSupplierCertificate, year), file.FileName);
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
            var opResult = PurchaseService.PurSupplierManager.SupplierCertificateManager.SavaEditSpplierCertificate(certificateDatas);
            return Json(opResult);
        }

        /// <summary>
        /// 获供应商合格的证书列表
        /// </summary>
        /// <param name="supplierId">供应商Id</param>
        /// <returns></returns>
        [NoAuthenCheck]

        public ContentResult GetSupplierQualifiedCertificateListBy(string supplierId)
        {
            var datas = PurchaseService.PurSupplierManager.SupplierCertificateManager.GetSupplierQualifiedCertificateListBy(supplierId);
            return DateJsonResult(datas);
        }
     
        /// <summary>
        /// 删除供应商证书文件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult DelPurSupplierCertificateFile(SupplierQualifiedCertificateModel entity)
        {
        
            var rootPath = HttpContext.Request.PhysicalApplicationPath;

            var datas = PurchaseService.PurSupplierManager.SupplierCertificateManager.DelEditSpplierCertificate(entity, rootPath);

            

            return Json(datas);
        }
        #endregion

        #region SupplierEvaluationManage
        public ActionResult SupplierEvaluationManage()
        {
            return View();
        }
        /// <summary>
        /// 获取要考核的供应商列表
        /// </summary>
        /// <param name="season"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetAuditSupplierList(string yearSeason)
        {
            var datas = PurchaseService.PurSupplierManager.SupplierAuditManager.GetSeasonSupplierList(yearSeason);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 保存供应商季度考核数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        ///SaveAuditSupplierInfo
        public JsonResult SaveAuditSupplierInfo(SupplierSeasonAuditModel entity)
        {
            var datas = PurchaseService.PurSupplierManager.SupplierAuditManager.SaveAuditSupplierInfo(entity);

            return Json(datas);
        }
        #endregion

        #region SupplierToturManage
        public ActionResult SupplierToturManage()
        {
            return View();
        }
        /// <summary>
        /// 供应商辅导计划
        /// </summary>
        /// <param name="yearQuarter"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ActionResult GetWaittingTourSupplier(string yearQuarter)
        {
            var datas = PurchaseService.PurSupplierManager.SupplierTutorManger.GetWaittingTourSupplier(yearQuarter);

            return Json(datas, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 编辑供应商辅导信息模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public ActionResult EditPurSupplierToturViewTpl()
        {
            return View();
        }
        /// <summary>
        /// 保存供应商辅导信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult SavePurSupTourInfo(SupplierSeasonTutorModel entity)
        {
            var opResult = PurchaseService.PurSupplierManager.SupplierTutorManger.SaveSupplierTutorModel(entity);
            return Json(opResult);
        }
        #endregion

        #region SupplierAuditToGrade
        public ActionResult SupplierAuditToGrade()
        {
            return View();
        }
        /// <summary>
        /// 编辑供应商考评分数模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public ActionResult EditPurSupAuditToGradeTpl()
        {
            return View();
        }

        /// <summary>
        /// 获取要评分的供应商信息列表
        /// </summary>
        /// <param name="yearQuarter"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetPurSupGradeInfo(string yearQuarter)
        {
            string year = yearQuarter.Substring(0, yearQuarter.Length - 2);
            var datas = PurchaseService.PurSupplierManager.SuppliersGradeManager.GetPurSupGradeInfoBy(year);
        
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 保存供应商评分数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult SavePurSupGradeData(SupplierGradeInfoModel entity)
        {
            var opResult = PurchaseService.PurSupplierManager.SuppliersGradeManager.SavePurSupGradeData(entity);
            return Json(opResult);
        }
        #endregion
    }
}
