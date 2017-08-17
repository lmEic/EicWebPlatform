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
    public class InspectionFqcItemConfigManager
    {
        /// <summary>
        /// 获取FQC检验项配置数据
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<InspectionFqcItemConfigModel> GetFqcInspectionItemConfigDatasBy(string materialId)
        {
            return InspectionManagerCrudFactory.FqcItemConfigCrud.FindFqcInspectionItemConfigDatasBy(materialId);
        }
        /// <summary>
        ///  在数据库中是否存在此料号
        /// </summary>
        /// <param name="materailId"></param>
        /// <returns></returns>
        public OpResult IsExistFqcConfigMaterailId(string materailId)
        {
            OpResult opResult = OpResult.SetSuccessResult("成功", true);
            if (InspectionManagerCrudFactory.FqcItemConfigCrud.IsExistFqcConfigmaterailId(materailId))
            {
                opResult = OpResult.SetSuccessResult("此物料已经存在", false);
            }
            var productMaterailModels = QmsDbManager.MaterialInfoDb.GetProductInfoBy(materailId);
            if (productMaterailModels != null && productMaterailModels.Count > 0)
                opResult.Entity = productMaterailModels.FirstOrDefault();
            else return OpResult.SetSuccessResult("料号不正确定", false);
            return opResult;
        }
        /// <summary>
        /// 增删改 FQC检验项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreFqcInspectionItemConfig(InspectionFqcItemConfigModel model)
        {
            return InspectionManagerCrudFactory.FqcItemConfigCrud.Store(model, true);
        }
        /// <summary>
        /// 批量存储FQC配置
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult StoreFqcInspectionItemConfig(List<InspectionFqcItemConfigModel> modelList, string isNeedOrt)
        {
            if (modelList != null && modelList.Count > 0)
            {
                if (InspectionManagerCrudFactory.OrtMaterialConfigCrud.ChangeMaterialIsValid(modelList.FirstOrDefault().MaterialId, isNeedOrt).Result)
                    modelList.ForEach(e => e.IsNeedORT = isNeedOrt);
            }
            return InspectionManagerCrudFactory.FqcItemConfigCrud.StoreFqcItemConfigList(modelList);

        }
        /// <summary>
        /// 导入FQC 检验配置文件
        /// </summary>
        /// <param name="documentPatch">Excel文档路径</param>
        /// <returns></returns>
        public List<InspectionFqcItemConfigModel> ImportProductFlowListBy(string documentPatch)
        {
            return documentPatch.GetEntitiesFromExcel<InspectionFqcItemConfigModel>();
        }
        /// <summary>
        /// 获得ORT物料配置
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public MaterialOrtConfigModel GetMaterialORTConfigBy(string materialId)
        {
            var data = InspectionManagerCrudFactory.OrtMaterialConfigCrud.FindOrtMaterialDatasBy(materialId);
            if (data == null) return new MaterialOrtConfigModel() { MaterialId = materialId, IsValid = "False" };
            return data;
        }
        public OpResult SaveOrtData(MaterialOrtConfigModel ortModel)
        {
            return InspectionManagerCrudFactory.OrtMaterialConfigCrud.StoreMaterialOrtConfigModel(ortModel);
        }
    }
}
