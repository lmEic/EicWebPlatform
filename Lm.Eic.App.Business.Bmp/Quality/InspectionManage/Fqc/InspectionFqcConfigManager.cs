using Lm.Eic.App.DomainModel.Bpm.Quanity;
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
        public List<InspectionFqcItemConfigModel> GetFqcspectionItemConfigDatasBy(string materialId)
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
            bool isexixt = InspectionManagerCrudFactory.FqcItemConfigCrud.IsExistFqcConfigmaterailId(materailId);
            OpResult opResult = OpResult.SetSuccessResult("存在", false);
            if (isexixt) opResult = OpResult.SetSuccessResult("此物料料号已经存在", true);
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

        public OpResult StoreFqcInspectionItemConfig(List<InspectionFqcItemConfigModel> modelList)
        {
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
    }
}
