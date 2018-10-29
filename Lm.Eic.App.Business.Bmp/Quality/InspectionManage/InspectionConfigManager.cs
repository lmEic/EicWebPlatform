﻿using Lm.Eic.App.DomainModel.Bpm.Quanity;
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


        public  InspectionConfigCheckManager ConfigCheckManager
        {
            get { return OBulider.BuildInstance<InspectionConfigCheckManager>(); }
        }


        /// <summary>
        ///Iqc检验项目配置管理器
        /// </summary>
        public InspectionIqcItemConfigManager IqcItemConfigManager
        {
            get { return OBulider.BuildInstance<InspectionIqcItemConfigManager>(); }
        }
        /// <summary>
        /// FQC检验项目配置管理器
        /// </summary>
        public InspectionFqcItemConfigManager FqcItemConfigManager
        {
            get { return OBulider.BuildInstance<InspectionFqcItemConfigManager>(); }
        }

        /// <summary>
        /// Ipqc检验项目配置管理器
        /// </summary>
        public InspectionIpqcItemConfigManager IpqcItemConfigManager
        {
            get { return OBulider.BuildInstance<InspectionIpqcItemConfigManager>(); }
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
            return InspectionManagerCrudFactory.InspectionModeConfigCrud.Store(model, true);
        }
        public List<InspectionModeConfigModel> GetInInspectionModeConfigModelList(string inspectionMode, string inspectionLevel, string inspectionAQL)
        {
            return InspectionManagerCrudFactory.InspectionModeConfigCrud.GetInspectionStartEndNumberBy(inspectionMode, inspectionLevel, inspectionAQL);
        }

        /// <summary>
        /// 导入配置列表
        /// </summary>
        /// <param name="documentPatch">Excel文档路径</param>
        /// <returns></returns>
        public List<InspectionModeConfigModel> ImportInspectionModeConfigBy(string documentPatch)
        {
            return documentPatch.GetEntitiesFromExcel<InspectionModeConfigModel>();
        }



        public List<string> GetInspectionModeConfigStrList(string inspectionMode, string inspectionLevel)
        {
            var listDatas = InspectionManagerCrudFactory.InspectionModeConfigCrud.GetInspectionStartEndNumberBy(inspectionMode, inspectionLevel);


            return listDatas.Select(e => e.InspectionAQL).Distinct().ToList();
        }
        public List<string> GetInspectionModeConfigStrList(string inspectionMode)
        {
            var listDatas = InspectionManagerCrudFactory.InspectionModeConfigCrud.GetInspectionStartEndNumberBy(inspectionMode);
            return listDatas.Select(e => e.InspectionLevel).Distinct().ToList();
        }
    }


    public class InspectionModeSwithConfigManager
    {
        /// <summary>
        /// 存储检验方式配置
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult StroeInspectionModeSwithConfig(string inspectionModeType, List<InspectionModeSwitchConfigModel> modelList)
        {
            return InspectionManagerCrudFactory.InspectionModeSwithConfigCrud.StoreModeSwithConfigModelList(inspectionModeType, modelList);
        }
        /// <summary>
        /// 得到转换数据
        /// </summary>
        /// <param name="swithCategory"></param>
        /// <returns></returns>
        public List<InspectionModeSwitchConfigModel> GetInspectionModeSwithConfig(string swithCategory)
        {
            return InspectionManagerCrudFactory.InspectionModeSwithConfigCrud.GetInspectionModeSwithConfiglistBy(swithCategory);
        }
        //public OpResult StroeInspectionSwithConfig(InspectionModeSwithConfigSummaryModel model)
        //{
        //    return StroeInspectionModeSwithConfig(SummaryrModelToStoreModel(model));
        //}
        //private List<InspectionModeSwithConfigModel> SummaryrModelToStoreModel(InspectionModeSwithConfigSummaryModel model)
        //{
        //   return model == null? new List<InspectionModeSwithConfigModel>() :
        //       new List<InspectionModeSwithConfigModel>()
        //    {
        //        new InspectionModeSwithConfigModel()
        //        {
        //            SwithCategory = model.SwithCategory,
        //            IsEnable=model.SumIsIsEnable.ToString(),
        //            SwithVaule = model.BroadenToNormalSampleNumber,
        //            SwithMemo = "宽到正常抽取批量",
        //            CurrentStatus = "放宽",
        //            SwithSatus ="正常",
        //            SwithProperty= "SampleNumber"
        //        },
        //        new InspectionModeSwithConfigModel()
        //        {
        //               SwithCategory = model.SwithCategory,
        //               IsEnable = model.SumIsIsEnable.ToString(),
        //               SwithVaule = model.BroadenToNormalAcceptNumber,
        //               SwithMemo = "宽到正常接受数",
        //               CurrentStatus = "放宽",
        //               SwithSatus = "正常",
        //               SwithProperty = "AcceptNumber"
        //        },
        //         new InspectionModeSwithConfigModel()
        //        {
        //            SwithCategory = model.SwithCategory,
        //            IsEnable=model.SumIsIsEnable.ToString(),
        //            SwithVaule = model.NormalToRestrictSampleNumber,
        //            SwithMemo = "正常到加严抽取批量",
        //            CurrentStatus = "正常",
        //            SwithSatus ="加严",
        //            SwithProperty= "SampleNumber"
        //        },
        //        new InspectionModeSwithConfigModel()
        //        {
        //               SwithCategory = model.SwithCategory,
        //               IsEnable = model.SumIsIsEnable.ToString(),
        //               SwithVaule = model.NormalToRestrictAcceptNumber,
        //               SwithMemo = "正常到加严接受数",
        //               CurrentStatus = "正常",
        //               SwithSatus = "加严",
        //               SwithProperty = "AcceptNumber"
        //        },

        //         new InspectionModeSwithConfigModel()
        //        {
        //            SwithCategory = model.SwithCategory,
        //            IsEnable=model.SumIsIsEnable.ToString(),
        //            SwithVaule = model.RestrictToNormalSampleNumber,
        //            SwithMemo = "加严到正常抽取批量",
        //            CurrentStatus = "加严",
        //            SwithSatus ="正常",
        //            SwithProperty= "SampleNumber"
        //        },
        //        new InspectionModeSwithConfigModel()
        //        {
        //               SwithCategory = model.SwithCategory,
        //               IsEnable = model.SumIsIsEnable.ToString(),
        //               SwithVaule = model.RestrictToNormalAcceptNumber,
        //               SwithMemo = "加严到正常抽取接受量",
        //               CurrentStatus = "加严",
        //               SwithSatus ="正常",
        //               SwithProperty = "AcceptNumber"
        //        },
        //         new InspectionModeSwithConfigModel()
        //        {
        //            SwithCategory = model.SwithCategory,
        //            IsEnable=model.SumIsIsEnable.ToString(),
        //            SwithVaule = model.NormalToBroadenSampleNumber,
        //            SwithMemo = "正常到放宽抽取批量",
        //            CurrentStatus = "正常",
        //            SwithSatus ="放宽",
        //            SwithProperty= "SampleNumber"
        //        },
        //        new InspectionModeSwithConfigModel()
        //        {
        //               SwithCategory = model.SwithCategory,
        //               IsEnable = model.SumIsIsEnable.ToString(),
        //               SwithVaule = model.NormalToBroadenAcceptNumber,
        //               SwithMemo = "正常到放宽接受数",
        //                CurrentStatus = "正常",
        //               SwithSatus ="放宽",
        //               SwithProperty = "AcceptNumber"
        //        },
        //    };



        //}

        //private InspectionModeSwithConfigSummaryModel StoreModelToShowModel(List<InspectionModeSwithConfigModel> modelList)
        //{
        //    return (modelList == null || modelList.Count <= 0) ? new InspectionModeSwithConfigSummaryModel() :
        //    new InspectionModeSwithConfigSummaryModel()
        //    {
        //        SwithCategory = modelList.FirstOrDefault().SwithProperty,
        //        BroadenToNormalAcceptNumber = modelList.FirstOrDefault(e => e.CurrentStatus == "放宽" && e.SwithSatus == "正常" && e.SwithProperty == "AcceptNumber").SwithVaule,
        //        BroadenToNormalSampleNumber = modelList.FirstOrDefault(e => e.CurrentStatus == "放宽" && e.SwithSatus == "正常" && e.SwithProperty == "SampleNumber").SwithVaule,
        //        NormalToRestrictAcceptNumber = modelList.FirstOrDefault(e => e.CurrentStatus == "正常" && e.SwithSatus == "加严" && e.SwithProperty == "AcceptNumber").SwithVaule,
        //        NormalToRestrictSampleNumber = modelList.FirstOrDefault(e => e.CurrentStatus == "正常" && e.SwithSatus == "加严" && e.SwithProperty == "SampleNumber").SwithVaule,
        //        RestrictToNormalAcceptNumber = modelList.FirstOrDefault(e => e.CurrentStatus == "加严" && e.SwithSatus == "正常" && e.SwithProperty == "AcceptNumber").SwithVaule,
        //        RestrictToNormalSampleNumber = modelList.FirstOrDefault(e => e.CurrentStatus == "加严" && e.SwithSatus == "正常" && e.SwithProperty == "SampleNumber").SwithVaule,
        //        NormalToBroadenAcceptNumber = modelList.FirstOrDefault(e => e.CurrentStatus == "正常" && e.SwithSatus == "放宽" && e.SwithProperty == "AcceptNumber").SwithVaule,
        //        NormalToBroadenSampleNumber = modelList.FirstOrDefault(e => e.CurrentStatus == "正常" && e.SwithSatus == "放宽" && e.SwithProperty == "SampleNumber").SwithVaule,
        //        SumIsIsEnable = Convert.ToBoolean(modelList.FirstOrDefault().IsEnable)

        //    };

        //}
    }

    public class InspectionConfigCheckManager
    {
        public List<InspectionItemConfigCheckModel> GetIqcspectionItemConfigDatasBy(string checkStatus, string department, DateTime dateFrom, DateTime dateTo)
        {

            return InspectionManagerCrudFactory.InspectionItemConfigCheckCrud.GetItemConfigCheckDates(checkStatus, department, dateFrom, dateTo);
        }
        public InspectionItemConfigCheckModel GetItemConfigCheckDataBy(string checkStatus)
        {
           var datas = InspectionManagerCrudFactory.InspectionItemConfigCheckCrud.GetItemConfigCheckDates(checkStatus);
           return  (datas!=null&&datas.Count>0)?datas.FirstOrDefault():null;
        }
        public OpResult checkIqcConfigItem(InspectionItemConfigCheckModel datas,string opProperty)
        {
            /// OpSign ==edit     审核
            ///  OpSign ==add     变更
            /// ItemConfigVersion 在原来的基础上加1
            /// CheckStatus       已审核
            OpResult opResult = InspectionManagerCrudFactory.InspectionItemConfigCheckCrud.UpdateCheckStatusk(datas);
            if (opResult.Result)
            {
               switch(opProperty)
                {
                    case "IQC":
                        opResult = InspectionManagerCrudFactory.IqcItemConfigCrud.OpCheckInspectionItemConfigDates(datas);
                        break;
                    case "FQC":
                        opResult = InspectionManagerCrudFactory.FqcItemConfigCrud.OpCheckInspectionItemConfigDates(datas);
                        break;
                    case "IPQC":
                       
                        break;
                    default:
                        opResult = OpResult.SetErrorResult("数据没有更新");
                        break;

                }
            }
            return opResult;
        }
    }
}
