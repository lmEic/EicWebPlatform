using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.Erp.Bussiness.QmsManage;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{

    /// <summary>
    /// IQC 进料检验项目的配置  管理器
    /// </summary>
    public class InspectionIqcItemConfigManager
    {
        /// <summary>
        /// 物料号查询检验项目
        /// （添加测试条件）
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
      
        //public OpResult checkIqcConfigItem(InspectionItemConfigCheckModel datas)
        //{
        //    /// OpSign ==edit     审核
        //    ///  OpSign ==add     变更
        //    /// ItemConfigVersion 在原来的基础上加1
        //    /// CheckStatus       已审核
        //    OpResult opResult = InspectionManagerCrudFactory.InspectionItemConfigCheckCrud.Store(datas);
        //    if (opResult.Result)
        //    {
        //        opResult= InspectionManagerCrudFactory.IqcItemConfigCrud.OpCheckInspectionItemConfigDates(datas);
              
        //    }
        //    return opResult;
        //}


        //public OpResult checkIqcConfigItem(InspectionConfigMasterVm datas, string inspectionStatus)
        //{
        //    OpResult opResult = OpResult.SetSuccessResult("", false);
        //    if (datas != null)
        //    {
        //        string isActivate = (inspectionStatus == "变更中") ? "false" : "True";
        //        datas.MaterialIqcInspetionItem.ForEach(e =>
        //        {
        //            e.CheckPerson = datas.OpPerson;
        //            e.CheckStatus = inspectionStatus;
        //            e.CheckDate = DateTime.Now.Date;
        //            e.OpSign = OpMode.Edit;
        //            e.IsActivate = isActivate;
        //        });
        //        opResult = StoreIqcInspectionItemConfig(datas.MaterialIqcInspetionItem);
        //        opResult.Entity = datas;
        //    }
        //    return opResult;
        //}
        ///<summary>
        /// 物料号查询检验项目
        /// （添加测试条件）
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<InspectionIqcItemConfigModel> GetIqcspectionItemConfigDatasBy(string materialId)
        {
            return InspectionManagerCrudFactory.IqcItemConfigCrud.FindAllIqcInspectionItemConfigDatasBy(materialId);
        }
        /// <summary>
        ///  在数据库中是否存在此料号
        /// </summary>
        /// <param name="materailId"></param>
        /// <returns></returns>
        public OpResult IsExistInspectionConfigMaterailId(string materailId)
        {
            bool isexixt = InspectionManagerCrudFactory.IqcItemConfigCrud.IsExistInspectionConfigmaterailId(materailId);
            OpResult opResult = OpResult.SetSuccessResult("", false);
            if (isexixt) opResult = OpResult.SetSuccessResult("此物料料号已经存在", true);
            return opResult;
        }
        /// <summary>
        /// 增删改 IQC检验项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreIqcInspectionItemConfig(InspectionIqcItemConfigModel model)
        {
            return InspectionManagerCrudFactory.IqcItemConfigCrud.Store(model, true);
        }

        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult StoreIqcInspectionItemConfig(List<InspectionIqcItemConfigModel> modelList)
        {
            OpResult opresult= InspectionManagerCrudFactory.IqcItemConfigCrud.StoreInspectionItemConfigDatas(modelList);
            if (opresult.Result)
            { InspectionManagerCrudFactory.InspectionItemConfigCheckCrud.initialStoreCheckModel<InspectionIqcItemConfigModel>(modelList,"IQC"); }

            return opresult;
        }



        /// <summary>
        /// 导入IQC 检验配置文件
        /// </summary>
        /// <param name="documentPatch">Excel文档路径</param>
        /// <returns></returns>
        public List<InspectionIqcItemConfigModel> ImportProductFlowListBy(string documentPatch)
        {
            return documentPatch.GetEntitiesFromExcel<InspectionIqcItemConfigModel>();
        }

        /// <summary>
        /// 获取工序Excel模板
        /// </summary>
        /// <param name="documentPath"></param>
        /// <returns></returns>
        public DownLoadFileModel GetIqcInspectionItemConfigTemplate(string documentPath, string fileDownLoadName)
        {
            DownLoadFileModel dlfm = new DownLoadFileModel();
            dlfm.FilePath = documentPath;
            dlfm.FileDownLoadName = fileDownLoadName;
            return dlfm;

        }
    }
}
