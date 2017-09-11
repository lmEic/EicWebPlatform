using System;
using System.Web.Mvc;
using System.Collections.Generic;
using Lm.Eic.App.DomainModel.Bpm.Pms.NewDailyReport;
using Lm.Eic.App.Business.Bmp.Pms.NewDailyReport;
using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.App.Business.Bmp.Pms.DailyReport;
using System.IO;
using Lm.Eic.App.Business.Bmp.Hrm.Archives;
using System.Web;
using Lm.Eic.App.Erp.Bussiness.QuantityManage;

namespace EicWorkPlatfrom.Controllers.Product
{
    public class ProDailyReportController : EicBaseController
    {
        //
        // GET: /DailyReport/

        public ActionResult Index()
        {
            return View();
        }

        #region report hour set method  生产工时工艺录入
        public ActionResult DReportHoursSet()
        {
            return View();
        }

        /// <summary>
        /// 获取产品工艺流程列表
        /// </summary>
        /// <param name="department"></param>
        /// <param name="productName"></param>
        /// <returns></returns>
        [HttpGet]
        [NoAuthenCheck]
        public JsonResult GetProductionFlowList(string department, string productName, string orderId, int searchMode)
        {
            //工单没有用到
            //用品名得到多处数据 把数据转化为 ProductsFlowOverModel
            var result = DailyProductionReportService.ProductionConfigManager.ProductionFlowSet.GetProductFlowInfoBy(new QueryDailyProductReportDto()
            {
                Department = department,
                ProductName = productName,
                OrderId = orderId,
                SearchMode = searchMode
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查找包含品名数据
        /// </summary>
        /// <param name="department"></param>
        /// <param name="likeProductName">包函的品名</param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetProductFlowListBy(string department, string likeProductName)
        {
            var productFlowOverviews = DailyProductionReportService.ProductionConfigManager.ProductionFlowSet.GetFlowShowSummaryInfosBy(department, likeProductName);
            return Json(productFlowOverviews, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存产品工艺流程数据
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        [HttpPost]
        [NoAuthenCheck]
        public JsonResult StoreProductFlowDatas(List<StandardProductionFlowModel> entities)
        {
            var datas = DailyProductionReportService.ProductionConfigManager.ProductionFlowSet.StoreModelList(entities);
            return Json(datas);
        }
        [HttpPost]
        [NoAuthenCheck]
        public JsonResult StoreFlowData(StandardProductionFlowModel entity)
        {
            var datas = DailyProductionReportService.ProductionConfigManager.ProductionFlowSet.StoreProductFlow(entity);
            return Json(datas);
        }
        /// <summary>
        /// 获取产品工艺初始化数据
        /// searchMode:0查询全部；1按名称模糊查询
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetProductionFlowOverview(string department, string productName, int searchMode)
        {

            if (searchMode == 0)
            {
                var ProductFlowdatas = DailyProductionReportService.ProductionConfigManager.ProductionFlowSet.GetFlowShowSummaryInfosBy(department);


                return Json(ProductFlowdatas, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var ProductFlowdatas = DailyProductionReportService.ProductionConfigManager.ProductionFlowSet.GetFlowShowSummaryInfosBy(department, productName);
                return Json(ProductFlowdatas, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 载入产品工艺流程模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult LoadProductFlowTemplateFile()
        {
            ///路径下载
            string filePath = @"E:\各部门日报格式\日报数据表.xls";
            var dlfm = DailyReportService.ConfigManager.ProductFlowSetter.GetProductFlowTemplate(filePath);
            return this.DownLoadFile(dlfm);

        }

        public JsonResult ImportProductFlowDatas(HttpPostedFileBase file)
        {
            List<ProductFlowModel> datas = null;
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    ///待加入验证文件名称逻辑:
                    string fileName = Path.Combine(this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.Temp), file.FileName);
                    file.SaveAs(fileName);
                    datas = DailyReportService.ConfigManager.ProductFlowSetter.ImportProductFlowListBy(fileName);
                    System.IO.File.Delete(fileName);
                }
            }

            return Json(datas, JsonRequestBehavior.AllowGet);

        }
        #endregion



        #region DRProductDispatching method 生产订单分派
        /// <summary>
        /// 生产工单管理
        /// </summary>
        /// <returns></returns>
        public ActionResult DRProductOrderDispatching()
        {
            return View();
        }
        /// <summary>
        /// 得到工单分配信息
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetOrderDispatchInfoDatas(string department)
        {
            ///ERP在制生产订单
            var erpInProductiondatas = QualityDBManager.OrderIdInpectionDb.GetProductionOrderIdInfoBy(department, "在制");
            ///今日生产已确认分配的订单
            var todayHaveDispatchProductionOrderDatas = "";
            var datas = new { erpInProductiondatas, todayHaveDispatchProductionOrderDatas };
            return DateJsonResult(datas);
        }
        #endregion



        #region daily report  input method 日报录入
        /// <summary>
        /// 日报录入
        /// </summary>
        /// <returns></returns>
        public ActionResult DReportInput()
        {
            return View();
        }
        /// <summary>
        ///  得到在制生产工单
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetInProductionOrderDatas(string department)
        {
            var datas = QualityDBManager.OrderIdInpectionDb.GetProductionOrderIdInfoBy(department, "在制");
            return DateJsonResult(datas);
        }

        #endregion
    }
}