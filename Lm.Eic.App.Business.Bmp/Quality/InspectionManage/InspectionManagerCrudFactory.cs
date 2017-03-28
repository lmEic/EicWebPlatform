using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
    /// <summary>
    /// IQC 检验管理工厂
    /// </summary>
    internal class InspectionManagerCrudFactory
    {
        /// <summary>
        /// 检验方式配置CRUD
        /// </summary>
        public static InspectionModeConfigCrud InspectionModeConfigCrud
        {
            get { return OBulider.BuildInstance<InspectionModeConfigCrud>(); }
        }

        /// <summary>
        /// 检验方式配置CRUD
        /// </summary>
        public static InspectionModeSwithConfigCrud InspectionModeSwithConfigCrud
        {
            get { return OBulider.BuildInstance<InspectionModeSwithConfigCrud>(); }
        }

        #region IQC Crud
        /// <summary>
        /// IQC物料检验配置CRUD
        /// </summary>
        public static InspectionIqcItemConfigCrud IqcItemConfigCrud
        {
            get { return OBulider.BuildInstance<InspectionIqcItemConfigCrud>(); }
        }
        /// <summary>
        /// 物料检验项次CRUD
        /// </summary>
        public static InspectionIqcMasterCrud IqcMasterCrud
        {
            get { return OBulider.BuildInstance<InspectionIqcMasterCrud>(); }
        }
        /// <summary>
        ///  物料检验项次数据CRUD
        /// </summary>
        public static InspectionIqcDetailCrud IqcDetailCrud
        {
            get { return OBulider.BuildInstance<InspectionIqcDetailCrud>(); }
        }
        #endregion


        #region  FQC CRUD


        public static InspectionFqcItemConfigCrud FqcItemConfigCrud
        {
            get { return OBulider.BuildInstance<InspectionFqcItemConfigCrud>(); }
        }

        public static InspectionFqcDatailCrud FqcDetailCrud
        {
            get { return OBulider.BuildInstance <InspectionFqcDatailCrud>(); }
        }

        public static InspectionFqcMasterCrud FqcMasterCrud
        {
            get { return OBulider.BuildInstance<InspectionFqcMasterCrud>(); }
        }

        #endregion
    }
    /// <summary>
    /// 检验方式配置CRUD
    /// </summary>
    internal class InspectionModeConfigCrud : CrudBase<InspectionModeConfigModel, IInspectionModeConfigRepository>
    {
        public InspectionModeConfigCrud() : base(new InspectionModeConfigRepository(), "检验方式配置")
        {
        }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddInspectionModeConfig);
            this.AddOpItem(OpMode.Edit, EidtInspectionModeConfig);
            this.AddOpItem(OpMode.Delete, DeleteInspectionModeConfig);
        }

        private OpResult DeleteInspectionModeConfig(InspectionModeConfigModel model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }

        private OpResult EidtInspectionModeConfig(InspectionModeConfigModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }

        private OpResult AddInspectionModeConfig(InspectionModeConfigModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }

        public List<InspectionModeConfigModel> GetInspectionStartEndNumberBy(string inspectionMode, string inspectionLevel = null, string inspectionAQL = null)
        {
            if ((inspectionMode != null && inspectionMode != string.Empty) && (inspectionLevel != null && inspectionLevel != String.Empty) && (inspectionAQL != null && inspectionAQL != string.Empty))

                return irep.Entities.Where(e => e.InspectionMode == inspectionMode && e.InspectionLevel == inspectionLevel && e.InspectionAQL == inspectionAQL).OrderBy(e => e.StartNumber).ToList();
            if ((inspectionMode != null && inspectionMode != string.Empty) && (inspectionLevel != null && inspectionLevel != String.Empty))
                return irep.Entities.Where(e => e.InspectionMode == inspectionMode && e.InspectionLevel == inspectionLevel).OrderBy(e => e.InspectionAQL).ToList();
            else return irep.Entities.Where(e => e.InspectionMode == inspectionMode).OrderBy(e => e.InspectionLevel).ToList();

        }

    }
    /// <summary>
    /// 检验方式转换配置CRUD
    /// </summary>

    internal class InspectionModeSwithConfigCrud : CrudBase<InspectionModeSwitchConfigModel, IInspectionModeSwitchConfigRepository>
    {
        public InspectionModeSwithConfigCrud() : base(new InspectionModeSwitchConfigRepository(), "检验方式转换")
        { }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddInspectionModeSwithConfig);
            this.AddOpItem(OpMode.Edit, EidtInspectionModeSwithConfig);
            this.AddOpItem(OpMode.Delete, DeleteInspectionModeSwithConfig);
        }

        private OpResult DeleteInspectionModeSwithConfig(InspectionModeSwitchConfigModel model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }

        private OpResult EidtInspectionModeSwithConfig(InspectionModeSwitchConfigModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }

        private OpResult AddInspectionModeSwithConfig(InspectionModeSwitchConfigModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
        /// <summary>
        /// 得到转换参数
        /// </summary>
        /// <param name="swithCategory"></param>
        /// <param name="currentStatus"></param>
        /// <returns></returns>

        internal List<InspectionModeSwitchConfigModel> GetInspectionModeSwithConfiglistBy(string swithCategory, string currentStatus)
        {
            return irep.Entities.Where(e => e.SwitchCategory == swithCategory && e.CurrentStatus == currentStatus).ToList();
        }


        /// <summary>
        /// 得到转换的参数
        /// </summary>
        /// <param name="swithCategory"></param>
        /// <returns></returns>
        internal List<InspectionModeSwitchConfigModel> GetInspectionModeSwithConfiglistBy(string swithCategory)
        {
            return irep.Entities.Where(e => e.SwitchCategory == swithCategory).ToList();
        }
        internal bool IsExistInspectionModeType(string inspectionModeType)
        {
            return irep.IsExist(e => e.SwitchCategory == inspectionModeType);
        }
        /// <summary>
        /// 保存数库
        /// </summary>
        /// <param name="ModelList"></param>
        /// <returns></returns>
        internal OpResult StoreModeSwithConfigModelList(string isEnable, List<InspectionModeSwitchConfigModel> modelList)
        {
            try
            {
                OpResult opResult = OpResult.SetResult("未执行任何操作！");
                if ((modelList == null || modelList.Count != 8) || !IsExistInspectionModeType(modelList.FirstOrDefault().SwitchCategory))
                    return opResult;
                SetFixFieldValue(modelList, OpMode.Add);
                int i = 0;
                //如果存在 就修改   
                modelList.ForEach(m =>
                {
                    if (this.irep.IsExist(e => e.Id_Key == m.Id_Key))
                    { m.OpSign = "edit"; }
                    m.IsEnable = isEnable;
                    opResult = this.Store(m);
                    if (opResult.Result)
                        i = i + opResult.RecordCount;
                });
                opResult = i.ToOpResult_Eidt(OpContext);
                if (i == modelList.Count) opResult.Entity = modelList;
                return opResult;
            }
            catch (Exception ex)
            {
                return OpResult.SetResult("未执行任何操作！");
                throw new Exception(ex.InnerException.Message); 
            }
         
        }
    }

    #region  IQC  IQC物料检验配置 Crud
    /// <summary>
    /// IQC物料检验配置
    /// </summary>
    internal class InspectionIqcItemConfigCrud : CrudBase<InspectionIqcItemConfigModel, IIqcInspectionItemConfigRepository>
    {
        public InspectionIqcItemConfigCrud() : base(new IqcInspectionItemConfigRepository(), "IQC物料检验配置")
        { }
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddInspectionItemConfig);
            this.AddOpItem(OpMode.Edit, EidtInspectionItemConfig);
            this.AddOpItem(OpMode.Delete, DeleteInspectionItemConfig);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult DeleteInspectionItemConfig(InspectionIqcItemConfigModel model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult EidtInspectionItemConfig(InspectionIqcItemConfigModel model)
        {

            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult AddInspectionItemConfig(InspectionIqcItemConfigModel model)
        {

            return irep.Insert(model).ToOpResult_Add(OpContext);
        }


        /// <summary>
        /// 在数据库中是否存在此料号
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>

        internal bool IsExistInspectionConfigmaterailId(string materailId)
        {
            return this.irep.IsExist(e => e.MaterialId == materailId);
        }
        /// <summary>
        /// 查询IQC物料检验配置数据
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<InspectionIqcItemConfigModel> FindIqcInspectionItemConfigDatasBy(string materialId)
        {
            return irep.Entities.Where(e => e.MaterialId == materialId).OrderBy(e => e.InspectionItemIndex).ToList();
        }


        /// <summary>
        /// 批量保存 IQC检验项目数据
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        internal OpResult StoreInspectionItemConfiList(List<InspectionIqcItemConfigModel> modelList)
        {
            OpResult opResult = OpResult.SetResult("未执行任何操作！");
            SetFixFieldValue(modelList, OpMode.Add);
            int i = 0;
            //如果存在 就修改   
            modelList.ForEach(m =>
            {
                if (this.irep.IsExist(e => e.Id_Key == m.Id_Key))
                { m.OpSign = "edit"; }


                opResult = this.Store(m);
                if (opResult.Result)
                    i = i + opResult.RecordCount;
            });
            opResult = i.ToOpResult(OpContext);
            if (i == modelList.Count) opResult.Entity = modelList;
            return opResult;


        }
    }


    /// <summary>
    /// 进料检验单（ERP）  物料检验项次
    /// </summary>
    internal class InspectionIqcMasterCrud : CrudBase<InspectionIqcMasterModel, IIqcInspectionMasterRepository>
    {
        public InspectionIqcMasterCrud() : base(new IqcInspectionMasterRepository(), "物料检验")
        {
        }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddIqcInspectionMaster);
            this.AddOpItem(OpMode.Edit, EidtIqcInspectionMaster);
            this.AddOpItem(OpMode.Delete, DeleteIqcInspectionMaster);
        }
        private OpResult DeleteIqcInspectionMaster(InspectionIqcMasterModel model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }

        private OpResult EidtIqcInspectionMaster(InspectionIqcMasterModel model)
        {
           
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        /// <summary>
        /// 更新详细列表
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materialId"></param>
        /// <param name="inspectionStatus"></param>
        /// <returns></returns>
        internal OpResult UpAuditDetailData(string orderId,string  materialId, string inspectionStatus)
        {
            return irep.UpAuditDetailData(orderId, materialId, inspectionStatus).ToOpResult_Eidt(OpContext);
        }
        /// <summary>
        /// 更新主列表
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materialId"></param>
        /// <param name="inspectionStatus"></param>
        /// <returns></returns>
        internal OpResult UpAuditMaterData(string orderId, string materialId, string inspectionStatus)
        {
            return irep.UpAuditMaterData(orderId, materialId, inspectionStatus).ToOpResult_Eidt(OpContext);
        }

        private OpResult AddIqcInspectionMaster(InspectionIqcMasterModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        internal InspectionIqcMasterModel GetIqcInspectionMasterModelListBy(string orderId, string materialId)
        {
            return irep.Entities.FirstOrDefault(e => e.OrderId == orderId && e.MaterialId == materialId);
        }
        internal bool IsExistOrderIdAndMaterailId(string orderId, string materialId, string InspectionIqcDetas = null)
        {
            bool returnBool = irep.IsExist(e => e.OrderId == orderId && e.MaterialId == materialId);
            if (InspectionIqcDetas != null && InspectionIqcDetas != string.Empty)
                return irep.IsExist(e => e.OrderId == orderId && e.MaterialId == materialId && e.InspectionStatus.Contains(InspectionIqcDetas));
            else return returnBool;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inspectionStatus"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>

        internal List<InspectionIqcMasterModel> GetIqcInspectionMasterModelListBy(string inspectionStatus, DateTime startTime, DateTime endTime, string inspectionResult =null )
        {
            if (inspectionResult == null)
                return irep.Entities.Where(e => e.InspectionStatus == inspectionStatus && e.MaterialInDate >= startTime && e.MaterialInDate <= endTime).ToList();
            else return irep.Entities.Where(e => e.InspectionStatus == inspectionStatus && e.MaterialInDate >= startTime && e.MaterialInDate <= endTime && e.InspectionResult == inspectionResult).ToList();
        }
        internal List<InspectionIqcMasterModel> GetIqcInspectionMasterModelListBy(DateTime startTime, DateTime endTime)
        {
            DateTime starttime = startTime.ToDate();
            DateTime endtime = endTime.ToDate();
            return irep.Entities.Where(e => e.MaterialInDate >= starttime && e.MaterialInDate <= endtime).ToList();
        }
    }
    /// <summary>
    ///进料检验单（ERP） 物料检验项次录入数据
    /// </summary>
    internal class InspectionIqcDetailCrud : CrudBase<InspectionIqcDetailModel, IIqcInspectionDetailRepository>
    {
        public InspectionIqcDetailCrud() : base(new IqcInspectionDetailRepository(), "物料检验项次数据")
        { }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddIqcInspectionDetail);
            this.AddOpItem(OpMode.Edit, EidtIqcInspectionDetail);
            this.AddOpItem(OpMode.Delete, DeleteIqcInspectionDetail);
        }

        private OpResult DeleteIqcInspectionDetail(InspectionIqcDetailModel model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }

        private OpResult EidtIqcInspectionDetail(InspectionIqcDetailModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }

        private OpResult AddIqcInspectionDetail(InspectionIqcDetailModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
        /// <summary>
        /// 由单号和料号得到所有检验项目的数据
        /// </summary>
        /// <param name="orderid">单号</param>
        /// <param name="materialId">料号</param>
        /// <returns></returns>
        internal List<InspectionIqcDetailModel> GetIqcInspectionDetailModelListBy(string materialId, string inspectionItem)
        {
            return irep.Entities.Where(e => e.InspecitonItem == inspectionItem && e.MaterialId == materialId).ToList(); ;
        }

        internal InspectionIqcDetailModel GetIqcInspectionDetailModelBy(string orderid, string materialId, string inspectionItem)
        {
            return irep.Entities.Where(e => e.OrderId == orderid && e.MaterialId == materialId && e.InspecitonItem == inspectionItem).ToList().FirstOrDefault(); ;
        }
        internal List<InspectionIqcDetailModel> GetIqcInspectionDetailModelBy(string orderid, string materialId)
        {
            return irep.Entities.Where(e => e.OrderId == orderid && e.MaterialId == materialId).ToList();
        }

        /// <summary>
        ///  判定是否需要测试 盐雾测试
        /// </summary>
        /// <param name="materialId">物料料号</param>
        /// <param name="materialInDate">当前物料进料日期</param>
        /// <returns></returns>
        internal bool JudgeYwTest(string materialId, DateTime materialInDate)
        {
            bool ratuenValue = true;
            //调出此物料所有打印记录项
            var inspectionItemsRecords = irep.Entities.Where(e => e.MaterialId == materialId).Distinct().ToList();
            //如果第一次打印 
            if (inspectionItemsRecords == null | inspectionItemsRecords.Count() <= 0) return true;

            // 进料日期后退30天 抽测打印记录
            var inspectionItemsMonthRecord = (from t in inspectionItemsRecords
                                              where t.MaterialInDate >= (materialInDate.AddDays(-30))
                                                    & t.MaterialInDate <= materialInDate
                                              select t.InspecitonItem).Distinct<string>().ToList();
            //没有 测
            if (inspectionItemsMonthRecord == null) return true;
            // 有  每项中是否有测过  盐雾测试
            foreach (var n in inspectionItemsMonthRecord)
            {
                if (n.Contains("盐雾")) { ratuenValue = false; break; }
            }
            return ratuenValue;


        }


        /// <summary>
        ///  判定些物料在二年内是否有录入记录 
        /// </summary>
        /// <param name="sampleMaterial">物料料号</param>
        /// <returns></returns>
        internal bool JudgeMaterialTwoYearIsRecord(string sampleMaterial)
        {
            var nn = irep.Entities.Where(e => e.MaterialInDate >= DateTime.Now.AddYears(-2));
            if (nn != null && nn.Count() > 0)
                return true;
            else return false;
        }
    }

    #endregion


    #region  FQC检验配置管理 Crud

    internal class InspectionFqcItemConfigCrud : CrudBase<InspectionFqcItemConfigModel, IFqcInspectionItemConfigRepository>
    {
        public InspectionFqcItemConfigCrud() : base(new FqcInspectionItemConfigRepository(), "Fqc料物配置")
        { }
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddFqcInspection);
            this.AddOpItem(OpMode.Edit, EidtFqcInspection);
            this.AddOpItem(OpMode.Delete, DeleteFqcInspection);
        }

        private OpResult DeleteFqcInspection(InspectionFqcItemConfigModel model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }

        private OpResult EidtFqcInspection(InspectionFqcItemConfigModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }

        private OpResult AddFqcInspection(InspectionFqcItemConfigModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }

        /// <summary>
        /// 查询FQC物料检验配置数据
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<InspectionFqcItemConfigModel> FindFqcInspectionItemConfigDatasBy(string materialId)
        {
            return irep.Entities.Where(e => e.MaterialId == materialId).OrderBy(e => e.InspectionItemIndex).ToList();
        }
        public bool IsExistFqcConfigmaterailId(string materailId)
        {
            return irep.IsExist(e => e.MaterialId == materailId);
        }
        public OpResult StoreFqcItemConfiList(List<InspectionFqcItemConfigModel> modelList)
        {
            OpResult opResult = OpResult.SetResult("未执行任何操作！");
            SetFixFieldValue(modelList, OpMode.Add);
            int i = 0;
            //如果存在 就修改   
            modelList.ForEach(m =>
            {
                if (this.irep.IsExist(e => e.Id_Key == m.Id_Key))
                { m.OpSign = "edit"; }


                opResult = this.Store(m);
                if (opResult.Result)
                    i = i + opResult.RecordCount;
            });
            opResult = i.ToOpResult(OpContext);
            if (i == modelList.Count) opResult.Entity = modelList;
            return opResult;
        }

    }


    internal class InspectionFqcDatailCrud : CrudBase<InspectionFqcDetailModel,IFqcInspectionDatailRepository>
    {
        public InspectionFqcDatailCrud():base(new FqcInspectionDatailRepository(),"FQC详细表单")
        { }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddFqcInspectionDetail);
            this.AddOpItem(OpMode.Edit, EidtFqcInspectionDetail);
            this.AddOpItem(OpMode.Delete, DeleteFqcInspectionDetail);
            this.AddOpItem(OpMode.UploadFile, UploadFileFqcInspectionDetail);
        }

        private OpResult DeleteFqcInspectionDetail(InspectionFqcDetailModel model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }

        private OpResult EidtFqcInspectionDetail(InspectionFqcDetailModel model)
        {
        
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }

        private OpResult AddFqcInspectionDetail(InspectionFqcDetailModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
        private OpResult UploadFileFqcInspectionDetail(InspectionFqcDetailModel model)
        {

            var oldmodel = InspectionManagerCrudFactory.FqcDetailCrud.GetFqcDetailModelBy(model.OrderId, model.OrderIdNumber, model.InspectionItem);
            if (oldmodel != null && model.OpSign == OpMode.UploadFile)
            {
                model.Id_Key = oldmodel.Id_Key;
                string oldPath = oldmodel.DocumentPath;
                string NewPath = model.DocumentPath;
                if (oldPath != null && oldPath != string.Empty && oldPath != NewPath)
                    GetDcumentPath(oldPath).DeleteFileDocumentation();
                model.OpSign = OpMode.Edit;
            }
            //if (model != null && model.DocumentPath != null && model.DocumentPath != string.Empty)


            //    GetDcumentPath( model.DocumentPath).DeleteFileDocumentation();

            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext); 
        }

        private string GetDcumentPath(string putinDcumentPath)
        {
            string dcumentPath = putinDcumentPath;
            if (dcumentPath.Contains("/"))
            { dcumentPath = dcumentPath.Replace("/", @"\"); }
            if (!dcumentPath.Contains(@"E:\Repositorys\EicWebPlatform\EicWorkPlatfrom\"))
            {
                dcumentPath = @"E:\Repositorys\EicWebPlatform\EicWorkPlatfrom\" + dcumentPath;
            }
            return dcumentPath;
        }

        public List<InspectionFqcDetailModel> GetFqcInspectionDetailModelListBy(string orderId)
        {
            return irep.Entities.Where(e => e.OrderId == orderId).ToList();
        }

        public List<InspectionFqcDetailModel> GetFqcInspectionDetailModelListBy(string orderId, int orderIdNumber)
        {
            return irep.Entities.Where(e => e.OrderId == orderId && e.OrderIdNumber == orderIdNumber).ToList();
        }
        public bool DetailDataIsexistBy(string orderId, int orderIdNumber, string inspecitonItem)
        {
            try
            {
                return irep.IsExist(e => e.OrderId == orderId && e.OrderIdNumber == orderIdNumber && e.InspectionItem == inspecitonItem);
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.InnerException.Message);
            }
           
        }

        public InspectionFqcDetailModel GetFqcDetailModelBy(string orderId, int orderIdNumber, string inspecitonItem)
        {
            try
            {
                return irep.Entities.FirstOrDefault(e => e.OrderId == orderId && e.OrderIdNumber == orderIdNumber && e.InspectionItem == inspecitonItem);
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception(ex.InnerException.Message);
            }

        }


        public InspectionFqcDetailModel GetFqcDetailModelBy(decimal id_key)
        {
            try
            {
                return irep.Entities.FirstOrDefault(e => e.Id_Key ==id_key );
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception(ex.InnerException.Message);
            }

        }

    }



    internal class InspectionFqcMasterCrud : CrudBase<InspectionFqcMasterModel, IFqcInspectionMasterRepository>
    {
        public InspectionFqcMasterCrud() : base(new FqcInspectionMasterRepository(), "FQC总表信息")
        { }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddFqcInspectionMaster);
            this.AddOpItem(OpMode.Edit, EidtFqcInspectionMaster);
            this.AddOpItem(OpMode.Delete, DeleteFqcInspectionMaster);
        }

        private OpResult DeleteFqcInspectionMaster(InspectionFqcMasterModel model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }

        private OpResult EidtFqcInspectionMaster(InspectionFqcMasterModel model)
        {
            
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }

        public InspectionFqcMasterModel ExistModel(string orderId, int orderIdNumber, string materialId)
        {
            try
            {
                return irep.Entities.FirstOrDefault(e => e.OrderId == orderId && e.OrderIdNumber == orderIdNumber && e.MaterialId == materialId);
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        private OpResult AddFqcInspectionMaster(InspectionFqcMasterModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
        public List<InspectionFqcMasterModel> GetFqcInspectionMasterModelListBy(string orderId)
        {
            return irep.Entities.Where(e => e.OrderId == orderId).ToList();
        }

        public List<InspectionFqcMasterModel> GetFqcInspectionMasterModelListBy(string formStatus, DateTime dateFrom, DateTime dateTo)
        {
            return irep.Entities.Where(e => e.InspectionStatus == formStatus && e.MaterialInDate >= dateFrom && e.MaterialInDate <= dateTo).ToList();
        }
        public List<InspectionFqcMasterModel> GetFqcInspectionMasterListBy(string materialId)
        {
            return irep.Entities.Where(e => e.MaterialId == materialId).ToList();
        }

        public OpResult UpAuditDetailData(string orderId, int orderIdNumber, string Updatestring)
        {
            return irep.UpAuditDetailData(orderId, orderIdNumber, Updatestring).ToOpResult_Eidt(OpContext);
        }
    }
    #endregion

}
