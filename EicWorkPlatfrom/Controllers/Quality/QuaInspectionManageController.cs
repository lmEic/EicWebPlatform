using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.Business.Bmp.Quality.InspectionManage;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using System.IO;

namespace EicWorkPlatfrom.Controllers
{
    public class QuaInspectionManageController : EicBaseController
    {
        //
        // GET: /QuaInspectionManage/

        public ActionResult Index()
        {
            return View();
        }

        #region IQC
        /// <summary>
        /// IQC检验项目配置
        /// </summary>
        /// <returns></returns>
        public ActionResult IqcInspectionItemConfiguration()
        {
            return View();
        }
        [NoAuthenCheck]
        public ActionResult InspectionDataGatheringOfIQC()
        {
            return View();
        }
        [NoAuthenCheck]

        public JsonResult GetMaterialDatas(string materialId)
        {
            var datas = InspectionService.InspectionItemConfigurator.GetIqcspectionItemConfigBy(materialId);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult DeleteMaterialDatas(IqcInspectionItemConfigModel entity) 
        {
            var opResult = InspectionService.InspectionItemConfigurator.SaveIqcInspectionItemConfig(entity);
           return Json(opResult);
        }

        // GetInspectionIndex
        [NoAuthenCheck]
        public JsonResult GetInspectionIndex(string materialId)
        {
            var opResult = InspectionService.InspectionItemConfigurator.GetInspectionIndex(materialId);
            return Json(opResult);
        }
        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="dataSets"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult SaveAllMaterialDatas(List<IqcInspectionItemConfigModel> dataSets)
        {
            return null;
        }
        /// <summary>
        /// 导入EXCEL数据到IQC物料检验配置
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult ImportIqcInspectionItemConfigDatas(HttpPostedFileBase file)
        {
            IqcInspectionItemConfigShowModel datas = null;
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    ///待加入验证文件名称逻辑:
                    string fileName = Path.Combine(this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.Temp), file.FileName);
                    file.SaveAs(fileName);
                    var listDatas = InspectionService.InspectionItemConfigurator.ImportProductFlowListBy(fileName);
                    var opResult = InspectionService.InspectionItemConfigurator.SaveIqcInspectionItemConfigList(listDatas);
                    if (opResult.Result)
                    {
                        datas=  new IqcInspectionItemConfigShowModel() { InspectionItemConfigModelList = listDatas };
                    }
                    System.IO.File.Delete(fileName);
                }
            }
            return Json(datas, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 载入IQC物料检验配置模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult LoadIqcInspectionItemConfigFile()
        {
            string filePath = @"E:\各部门日报格式\IQC物料检验配置数据表.xls";
            MemoryStream ms = InspectionService.InspectionItemConfigurator.GetIqcInspectionItemConfigTemplate(filePath);
            return this.ExportToExcel(ms, "IQC物料检验配置模板", "IQC物料检验配置模板");
            //return null;
        }
        #endregion

    }
}
