using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.Erp.Bussiness.QmsManage;
using Lm.Eic.App.Erp.Domain.QuantityModel;
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
    ///  检验方式的配置 管理器
    /// </summary>
    public class InspectionModeConfigManager
    {
        /// <summary>
        /// 存储  检验方式配置数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreInspectionModeConfig(InspectionModeConfigModel model)
        {
            return IqcInspectionManagerCrudFactory.InspectionModeConfigCrud.Store(model, true);
        }
    }

    /// <summary>
    /// IQC 进料检验的配置  管理器
    /// </summary>
    public class IqcInspectionItemConfigManager
    {
        /// <summary>
        /// 物料号查询检验项目
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<IqcInspectionItemConfigModel> GetIqcspectionItemConfigDatasBy(string materialId)
        {
          return  IqcInspectionManagerCrudFactory.InspectionItemConfigCrud.FindIqcInspectionItemConfigDatasBy(materialId); 
        }
        /// <summary>
        ///  在数据库中是否存在此料号
        /// </summary>
        /// <param name="materailId"></param>
        /// <returns></returns>
        public OpResult IsExistInspectionConfigMaterId(string materailId)
        {
            bool  isexixt=   IqcInspectionManagerCrudFactory.InspectionItemConfigCrud.IsExistInspectionConfigmaterailId(materailId);
            OpResult opResult = OpResult.SetResult("", false );
            if (isexixt) opResult = OpResult.SetResult("此物料料号已经存在", true);
            return opResult;
        }
        /// <summary>
        /// 增删改 IQC检验项目
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult StoreIqcInspectionItemConfig(IqcInspectionItemConfigModel model)
        {
           return IqcInspectionManagerCrudFactory.InspectionItemConfigCrud.Store(model,true);
        }
        
        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult StoreIqcInspectionItemConfig(List<IqcInspectionItemConfigModel> modelList)
        {
            return IqcInspectionManagerCrudFactory.InspectionItemConfigCrud.StoreInspectionItemConfiList(modelList);
        }

      

        /// <summary>
        /// 导入IQC 检验配置文件
        /// </summary>
        /// <param name="documentPatch">Excel文档路径</param>
        /// <returns></returns>
        public List<IqcInspectionItemConfigModel> ImportProductFlowListBy(string documentPatch)
        {
            StringBuilder errorStr = new StringBuilder();
            var listEntity = ExcelHelper.ExcelToEntityList<IqcInspectionItemConfigModel>(documentPatch, out errorStr);
            string errorStoreFilePath = @"C:\ExcelToEntity\ErrorStr.txt";
            if (errorStr.ToString() != string.Empty)
            {
                errorStoreFilePath.CreateFile(errorStr.ToString());
            }
            return listEntity;
        }

        /// <summary>
        /// 获取工序Excel模板
        /// </summary>
        /// <param name="documentPath"></param>
        /// <returns></returns>
        public System.IO.MemoryStream GetIqcInspectionItemConfigTemplate(string documentPath)
        {
            return FileOperationExtension.GetMemoryStream(documentPath);
        }
    }



  
}
