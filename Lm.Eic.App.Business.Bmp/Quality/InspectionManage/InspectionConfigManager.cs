using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.Erp.Bussiness.QmsManage;
using Lm.Eic.App.Erp.Domain.QuantityModel;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
    /// <summary>
    /// 检验配置管理器
    /// </summary>
    public class InspectionConfigurationManager
    {
        /// <summary>
        ///检验方式配置管理器
        /// </summary>
        public InspectionModeConfigManager ModeConfigManager
        {
            get { return OBulider.BuildInstance<InspectionModeConfigManager>(); }
        }
        /// <summary>
        /// 检验方式转换配置管理器
        /// </summary>
        public InspectionModeSwithConfigManager ModeSwithConfigManager
        {
            get { return OBulider.BuildInstance<InspectionModeSwithConfigManager>(); }
        }
        /// <summary>
        ///Iqc检验项目配置管理器
        /// </summary>
        public InspectionIqcItemConfigManager IqcItemConfigManager
        {
            get { return OBulider.BuildInstance<InspectionIqcItemConfigManager>(); }
        }
    }

    /// <summary>
    ///  检验方式的配置管理器
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
            return InspectionIqcManagerCrudFactory.InspectionModeConfigCrud.Store(model, true);
        }
        public List<InspectionModeConfigModel> GetInInspectionModeConfigModelList(string inspectionMode, string inspectionLevel, string inspectionAQL)
        {
            return InspectionIqcManagerCrudFactory.InspectionModeConfigCrud.GetInspectionStartEndNumberBy(inspectionMode,  inspectionLevel,  inspectionAQL);
        }
    }


    public class InspectionModeSwithConfigManager
    {
       public OpResult  StroeInspectionModeSwithConfig(List<InspectionModeSwithConfigModel>  modelList)

        {
            return InspectionIqcManagerCrudFactory.InspectionModeSwithConfigCrud.StoreModeSwithConfigModelList(modelList);
        }
        private List<InspectionModeSwithConfigModel> SummaryrModelToStoreModel(InspectionModeSwithConfigSummaryModel model)
        {
           return model == null? new List<InspectionModeSwithConfigModel>() :
               new List<InspectionModeSwithConfigModel>()
            {
                new InspectionModeSwithConfigModel()
                {
                    SwithCategory = model.SwithCategory,
                    IsEnable=model.SumIsIsEnable.ToString(),
                    SwithVaule = model.BroadenToNormalSampleNumber,
                    SwithMemo = "宽到正常抽取批量",
                    CurrentStatus = "放宽",
                    SwithSatus ="正常",
                    SwithProperty= "SampleNumber"
                },
                new InspectionModeSwithConfigModel()
                {
                       SwithCategory = model.SwithCategory,
                       IsEnable = model.SumIsIsEnable.ToString(),
                       SwithVaule = model.BroadenToNormalAcceptNumber,
                       SwithMemo = "宽到正常接受数",
                       CurrentStatus = "放宽",
                       SwithSatus = "正常",
                       SwithProperty = "AcceptNumber"
                },
                 new InspectionModeSwithConfigModel()
                {
                    SwithCategory = model.SwithCategory,
                    IsEnable=model.SumIsIsEnable.ToString(),
                    SwithVaule = model.NormalToRestrictSampleNumber,
                    SwithMemo = "正常到加严抽取批量",
                    CurrentStatus = "正常",
                    SwithSatus ="加严",
                    SwithProperty= "SampleNumber"
                },
                new InspectionModeSwithConfigModel()
                {
                       SwithCategory = model.SwithCategory,
                       IsEnable = model.SumIsIsEnable.ToString(),
                       SwithVaule = model.NormalToRestrictAcceptNumber,
                       SwithMemo = "正常到加严接受数",
                       CurrentStatus = "正常",
                       SwithSatus = "加严",
                       SwithProperty = "AcceptNumber"
                },

                 new InspectionModeSwithConfigModel()
                {
                    SwithCategory = model.SwithCategory,
                    IsEnable=model.SumIsIsEnable.ToString(),
                    SwithVaule = model.RestrictToNormalSampleNumber,
                    SwithMemo = "加严到正常抽取批量",
                    CurrentStatus = "加严",
                    SwithSatus ="正常",
                    SwithProperty= "SampleNumber"
                },
                new InspectionModeSwithConfigModel()
                {
                       SwithCategory = model.SwithCategory,
                       IsEnable = model.SumIsIsEnable.ToString(),
                       SwithVaule = model.RestrictToNormalAcceptNumber,
                       SwithMemo = "加严到正常抽取接受量",
                       CurrentStatus = "加严",
                       SwithSatus ="正常",
                       SwithProperty = "AcceptNumber"
                },
                 new InspectionModeSwithConfigModel()
                {
                    SwithCategory = model.SwithCategory,
                    IsEnable=model.SumIsIsEnable.ToString(),
                    SwithVaule = model.NormalToBroadenSampleNumber,
                    SwithMemo = "正常到放宽抽取批量",
                    CurrentStatus = "正常",
                    SwithSatus ="放宽",
                    SwithProperty= "SampleNumber"
                },
                new InspectionModeSwithConfigModel()
                {
                       SwithCategory = model.SwithCategory,
                       IsEnable = model.SumIsIsEnable.ToString(),
                       SwithVaule = model.NormalToBroadenAcceptNumber,
                       SwithMemo = "正常到放宽接受数",
                        CurrentStatus = "正常",
                       SwithSatus ="放宽",
                       SwithProperty = "AcceptNumber"
                },
            };


           
        }

        private InspectionModeSwithConfigSummaryModel StoreModelToShowModel(List<InspectionModeSwithConfigModel> modelList)
        {
            return null;
        }
    }
}
