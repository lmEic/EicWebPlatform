using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.Erp.Bussiness.QmsManage;
using Lm.Eic.App.Erp.Bussiness.QuantityManage;
using Lm.Eic.App.Erp.Domain.QuantityModel;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
    /// <summary>
    /// 进料检验数据采集器
    /// </summary>
    public class InspectionIqcDataGather : InspectionDateGatherManageBase
    {

        #region LoadClass
        public IqcMasterDatasGather MasterDatasGather
        {
            get { return OBulider.BuildInstance<IqcMasterDatasGather>(); }
        }
        public IqcDetailDatasGather DetailDatasGather
        {
            get { return OBulider.BuildInstance<IqcDetailDatasGather>(); }
        }
        public InspectionItemCondition ItemCondition
        {
            get { return OBulider.BuildInstance<InspectionItemCondition>(); }
        }
        #endregion
        public OpResult StoreInspectionIqcGatherDatas(InspectionItemDataSummaryVM model)
        {
            var opReulst = OpResult.SetSuccessResult("数据为空，保存失败", false);
            if (model == null) return opReulst;
            ///输入的数据不为空值
            if (model.InspectionItemDatas == null || model.InspectionItemDatas == string.Empty) return opReulst;
            opReulst = DetailDatasGather.StoreInspectionIqcDetailModelForm(model, model.SiteRootPath);
            if (opReulst.Result)
                opReulst = MasterDatasGather.StoreInspectionIqcMasterModelForm(model);
            return opReulst;
        }
        /// <summary>
        /// 得到下载路经
        /// </summary>
        /// <param name="siteRootPath"></param>
        /// <param name="orderId"></param>
        /// <param name="materialId"></param>
        /// <param name="inspectionItem"></param>
        /// <returns></returns>
        public DownLoadFileModel GetIqcDatasDownLoadFileModel(string siteRootPath, string orderId, string materialId, string inspectionItem)
        {
            DownLoadFileModel dlfm = new DownLoadFileModel();
            var model = InspectionManagerCrudFactory.IqcDetailCrud.GetIqcInspectionDetailModelBy(orderId, materialId, inspectionItem);
            if (model == null || model.FileName == null || model.DocumentPath == null)
                return dlfm.Default();
            return dlfm.CreateInstance
                (siteRootPath.GetDownLoadFilePath(model.DocumentPath),
                model.FileName.GetDownLoadContentType(),
                 model.FileName);
        }
        /// <summary>
        /// 生成IQC检验项目所有的信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<InspectionItemDataSummaryVM> BuildingIqcInspectionItemDataSummaryLabelListBy(string orderId, string materialId)
        {
            try
            {
                var orderMaterialInfo = GetPuroductSupplierInfo(orderId).FirstOrDefault(e => e.ProductID == materialId);
                if (orderMaterialInfo == null) return new List<InspectionItemDataSummaryVM>();
                var iqcNeedInspectionsItemdatas = ItemCondition.getIqcNeedInspectionItemDatas(orderId, materialId, orderMaterialInfo.ProduceInDate);
                if (iqcNeedInspectionsItemdatas == null || iqcNeedInspectionsItemdatas.Count <= 0) return new List<InspectionItemDataSummaryVM>();
                //保存单头数据
                return HandleInspectionSummayDatas(orderMaterialInfo, iqcNeedInspectionsItemdatas);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// 处理数据总表
        /// </summary>
        /// <param name="orderMaterialInfo"></param>
        /// <param name="iqcNeedInspectionsItemdatas"></param>
        /// <returns></returns>
        private List<InspectionItemDataSummaryVM> HandleInspectionSummayDatas(MaterialModel orderMaterialInfo, List<InspectionIqcItemConfigModel> iqcNeedInspectionsItemdatas)
        {
            List<InspectionItemDataSummaryVM> returnList = new List<InspectionItemDataSummaryVM>();
            InspectionItemDataSummaryVM model = null;
            iqcNeedInspectionsItemdatas.ForEach(m =>
            {
                var inspectionMode = GetJudgeInspectionMode("IQC", m.MaterialId, m.InspectionItem);
                ///得到检验方法数据
                var inspectionModeConfigModelData = this.GetInspectionModeConfigDataBy(m.InspectionLevel, m.InspectionAQL, orderMaterialInfo.ProduceNumber, inspectionMode);
                ///得到已经检验的数据  
                var iqcHaveInspectionData = DetailDatasGather.GetIqcInspectionDetailModelBy(orderMaterialInfo.OrderID, orderMaterialInfo.ProductID, m.InspectionItem);
                ///初始化 综合模块 
                InitializeSummaryVM(out model, orderMaterialInfo, iqcNeedInspectionsItemdatas, m, inspectionMode);
                ///加载已经录入的数据
                SetHaveInspectionItemDataVaule(model, iqcHaveInspectionData);
                ///加载项目的抽样方案
                SetItemModeConfig(model, inspectionModeConfigModelData);
                returnList.Add(model);
                //产生测试项目先存入到数据库中(如果存在 直接反回)
                DetailDatasGather.InitializestoreInspectionDetial(model);
            });
            return returnList;
        }

        /// 查找IQC检验项目所有的信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<InspectionItemDataSummaryVM> FindIqcInspectionItemDataSummaryLabelListBy(string orderId, string materialId)
        {
            List<InspectionItemDataSummaryVM> returnList = new List<InspectionItemDataSummaryVM>();
            ///明细表中查找
            var iqcHaveInspectionData = DetailDatasGather.GetIqcInspectionDetailDatasBy(orderId, materialId);
            if (iqcHaveInspectionData == null || iqcHaveInspectionData.Count <= 0) return returnList;
            ///物料配置项中查找
            var iqcItemConfigdatas = InspectionManagerCrudFactory.IqcItemConfigCrud.FindIqcInspectionItemConfigDatasBy(materialId);
            if (iqcItemConfigdatas == null || iqcItemConfigdatas.Count <= 0) return returnList;
            ///每个一项添加相应的信息
            iqcHaveInspectionData.ForEach(m =>
           {
               ///初始化 综合模块
               var model = new InspectionItemDataSummaryVM()
               {
                   OrderId = orderId,
                   MaterialId = materialId,
                   InspectionItem = m.InspecitonItem,
                   EquipmentId = m.EquipmentId,
                   MaterialInDate = m.MaterialInDate,
                   MaterialInCount = m.MaterialCount,
                   SizeLSL = 0,
                   SizeUSL = 0,
                   InspectionItemStatus = m.InspectionItemStatus,
                   SizeMemo = string.Empty,
                   InspectionAQL = string.Empty,
                   InspectionLevel = string.Empty,
                   InspectionCount = Convert.ToInt16(m.InspectionCount),
                   AcceptCount = 0,
                   RefuseCount = 0,
                   InspectionItemDatas = m.InspectionItemDatas,
                   InsptecitonItemIsFinished = true,
                   NeedFinishDataNumber = Convert.ToInt16(m.InspectionCount),
                   HaveFinishDataNumber = this.GetHaveFinishDataNumber(m.InspectionItemDatas),
                   InspectionItemResult = m.InspectionItemResult,
                   FileName = m.FileName,
                   DocumentPath = m.DocumentPath,
                   Memo = m.Memo,
                   OpPerson = m.OpPerson,
                   InspectionMethod = string.Empty,
                   InspectionMode = m.InspectionMode,
                   Id_Key = m.Id_Key,
               };
               /// 找到对应的项目
               var iqcItemConfigdata = iqcItemConfigdatas.FirstOrDefault(e => e.InspectionItem == m.InspecitonItem);
               if (iqcItemConfigdata != null)
               {
                   model.SizeLSL = iqcItemConfigdata.SizeLSL;
                   model.SizeUSL = iqcItemConfigdata.SizeUSL;
                   model.SizeMemo = iqcItemConfigdata.SizeMemo;
                   model.InspectionAQL = iqcItemConfigdata.InspectionAQL;
                   model.InspectionMethod = iqcItemConfigdata.InspectionMethod;
                   model.InspectionLevel = iqcItemConfigdata.InspectionLevel;
                   model.EquipmentId = iqcItemConfigdata.EquipmentId;
                   //数据采集类型
                   model.InspectionDataGatherType = iqcItemConfigdata.InspectionDataGatherType;
               }
               //检验方式
               var inspectionMode = GetJudgeInspectionMode("IQC", m.MaterialId, m.InspecitonItem);
               if (model.InspectionMode == string.Empty)
               {
                   model.InspectionMode = inspectionMode;
               }

               var inspectionModeConfigModelData = this.GetInspectionModeConfigDataBy(model.InspectionLevel, model.InspectionAQL, m.MaterialCount, inspectionMode);

               if (inspectionModeConfigModelData != null)
               {
                   model.AcceptCount = inspectionModeConfigModelData.AcceptCount;
                   model.RefuseCount = inspectionModeConfigModelData.RefuseCount;

               }

               returnList.Add(model);
           });
            return returnList;
        }


        /// <summary>
        /// 设置项目的抽样方案
        /// </summary>
        /// <param name="model"></param>
        /// <param name="inspectionModeConfigModelData"></param>
        private void SetItemModeConfig(InspectionItemDataSummaryVM model, InspectionModeConfigModel inspectionModeConfigModelData)
        {
            if (inspectionModeConfigModelData != null)
            {
                if (model.InspectionCount == 0)
                    model.InspectionCount = inspectionModeConfigModelData.InspectionCount;
                if (model.AcceptCount == 0)
                    model.AcceptCount = inspectionModeConfigModelData.AcceptCount;
                if (model.RefuseCount == 0)
                    model.RefuseCount = inspectionModeConfigModelData.RefuseCount;
                if (model.NeedFinishDataNumber == 0)
                    //需要录入的数据个数 暂时为抽样的数量
                    model.NeedFinishDataNumber = inspectionModeConfigModelData.InspectionCount;
            }
        }

        /// <summary>
        /// 初始化 综合模块 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="orderMaterialInfo"></param>
        /// <param name="iqcNeedInspectionsItemdatas"></param>
        /// <param name="m"></param>
        /// <param name="inspectionMode"></param>
        private void InitializeSummaryVM(
           out InspectionItemDataSummaryVM model,
            MaterialModel orderMaterialInfo,
            List<InspectionIqcItemConfigModel> iqcNeedInspectionsItemdatas,
            InspectionIqcItemConfigModel m,
            string inspectionMode)
        {

            model = new InspectionItemDataSummaryVM()
            {
                OrderId = orderMaterialInfo.OrderID,
                MaterialId = orderMaterialInfo.ProductID,
                InspectionItem = m.InspectionItem,
                EquipmentId = m.EquipmentId,
                MaterialInDate = orderMaterialInfo.ProduceInDate,
                MaterialDrawId = orderMaterialInfo.ProductDrawID,
                MaterialName = orderMaterialInfo.ProductName,
                MaterialSpec = orderMaterialInfo.ProductStandard,
                MaterialSupplier = orderMaterialInfo.ProductSupplier,
                MaterialInCount = orderMaterialInfo.ProduceNumber,
                InspectionItemSumCount = iqcNeedInspectionsItemdatas.Count,
                InspectionItemStatus = "Doing",
                ///检验方法
                InspectionMethod = m.InspectionMethod,
                ///数据采集类型
                InspectionDataGatherType = m.InspectionDataGatherType,
                SizeLSL = m.SizeLSL,
                SizeUSL = m.SizeUSL,
                SizeMemo = m.SizeMemo,
                InspectionAQL = m.InspectionAQL,
                ///检验方式
                InspectionMode = inspectionMode,
                InspectionLevel = m.InspectionLevel,
                Memo = string.Empty,
                InspectionCount = 0,
                AcceptCount = 0,
                RefuseCount = 0,
                InspectionItemDatas = string.Empty,
                InsptecitonItemIsFinished = false,
                NeedFinishDataNumber = 0,
                HaveFinishDataNumber = 0,
                InspectionItemResult = string.Empty
            };
        }
        /// <summary>
        /// 加载已经录入的数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="iqcHaveInspectionData"></param>
        private void SetHaveInspectionItemDataVaule(InspectionItemDataSummaryVM model, InspectionIqcDetailModel iqcHaveInspectionData)
        {
            if (iqcHaveInspectionData != null)
            {
                model.InspectionItemDatas = iqcHaveInspectionData.InspectionItemDatas;
                model.InspectionItemResult = iqcHaveInspectionData.InspectionItemResult;
                model.EquipmentId = iqcHaveInspectionData.EquipmentId;
                model.InspectionItemStatus = iqcHaveInspectionData.InspectionItemStatus;
                model.Id_Key = iqcHaveInspectionData.Id_Key;
                model.Memo = iqcHaveInspectionData.Memo;
                model.InspectionMode = iqcHaveInspectionData.InspectionMode;
                model.FileName = iqcHaveInspectionData.FileName;
                model.DocumentPath = iqcHaveInspectionData.DocumentPath;
                model.InspectionCount = iqcHaveInspectionData.InspectionCount;
                model.AcceptCount = iqcHaveInspectionData.InspectionAcceptCount;
                model.RefuseCount = iqcHaveInspectionData.InspectionRefuseCount;
                model.InsptecitonItemIsFinished = true;
                model.NeedFinishDataNumber = (int)iqcHaveInspectionData.InspectionCount;
                model.HaveFinishDataNumber = DoHaveFinishDataNumber(iqcHaveInspectionData.InspectionItemResult, iqcHaveInspectionData.InspectionItemDatas, model.NeedFinishDataNumber);
            }
        }
        /// <summary>
        /// 判断是否按提正常还
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        private string GetJudgeInspectionMode(string InspectionClass, string materialId, string InspecitonItem)
        {
            try
            {
                ///1,通过料号 和 抽检验项目  得到当前的最后一次抽检的状态
                ///3，比较 对比
                ///4，返回一个 转换的状态
                string retrunstirng = "正常";
                var DetailModeDatas = DetailDatasGather.GetIqcMasterModeDatasBy(materialId);
                if (DetailModeDatas == null) return retrunstirng;
                var DetailModeData = DetailModeDatas.OrderByDescending(e => e.MaterialInDate).Take(100).ToList();
                if (DetailModeData == null || DetailModeData.Count <= 0) return retrunstirng;
                var currentStatus = DetailModeData.Last().InspectionMode;
                ///2，通当前状态 得到抽样规则 抽样批量  拒受数
                var modeSwithParameterList = InspectionManagerCrudFactory.InspectionModeSwithConfigCrud.GetInspectionModeSwithConfiglistBy(InspectionClass, currentStatus);
                if (modeSwithParameterList == null || modeSwithParameterList.Count <= 0) return retrunstirng;
                int sampleNumberVauleMin = modeSwithParameterList.FindAll(e => e.SwitchProperty == "SampleNumber").Select(e => e.SwitchVaule).Min();
                int AcceptNumberVauleMax = modeSwithParameterList.FindAll(e => e.SwitchProperty == "AcceptNumber").Select(e => e.SwitchVaule).Max();
                int sampleNumberVauleMax = modeSwithParameterList.FindAll(e => e.SwitchProperty == "SampleNumber").Select(e => e.SwitchVaule).Max();
                int AcceptNumberVauleMin = modeSwithParameterList.FindAll(e => e.SwitchProperty == "AcceptNumber").Select(e => e.SwitchVaule).Min();
                var getNumber = DetailModeData.Take(sampleNumberVauleMax).Count(e => e.InspectionResult == "FAIL");
                switch (currentStatus)
                {
                    case "加严":
                        retrunstirng = (getNumber >= AcceptNumberVauleMin) ? "正常" : currentStatus;
                        break;
                    case "放宽":
                        retrunstirng = (getNumber <= AcceptNumberVauleMin) ? "正常" : currentStatus;
                        break;
                    case "正常":
                        if (getNumber <= AcceptNumberVauleMin) retrunstirng = "放宽";
                        else
                        {
                            ///加严的数量
                            int getTheNumber = DetailModeData.Take(sampleNumberVauleMin).Count(e => e.InspectionResult == "FAIL");
                            retrunstirng = (getTheNumber >= AcceptNumberVauleMax) ? "加严" : currentStatus;
                        }
                        break;
                    default:
                        break;
                }
                return retrunstirng;
            }
            catch (Exception es)
            {
                throw new Exception(es.InnerException.Message);
            }

        }



        public List<InspectionIqcDetailModel> GetIqcInspectionDetailDatasBy(string orderId)
        {
            return DetailDatasGather.GetIqcInspectionDetailDatasBy(orderId);
        }
    }




}
