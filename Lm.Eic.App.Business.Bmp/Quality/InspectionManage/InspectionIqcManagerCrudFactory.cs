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

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
    /// <summary>
    /// IQC 检验管理工厂
    /// </summary>
    internal class InspectionIqcManagerCrudFactory
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

        public List<InspectionModeConfigModel> GetInspectionStartEndNumberBy(string inspectionMode, string inspectionLevel, string inspectionAQL)
        {
            if ((inspectionLevel == null || inspectionLevel==String.Empty )&&(inspectionAQL == null || inspectionAQL == string.Empty))
              return irep.Entities.Where(e => e.InspectionMode == inspectionMode).OrderBy(e => e.StartNumber).ToList();
            if (inspectionAQL == null || inspectionAQL==string.Empty)
                return irep.Entities.Where(e => e.InspectionMode == inspectionMode && e.InspectionLevel == inspectionLevel).OrderBy(e => e.StartNumber).ToList();
           else  return irep.Entities.Where(e => e.InspectionMode == inspectionMode && e.InspectionLevel == inspectionLevel && e.InspectionAQL == inspectionAQL).OrderBy(e => e.StartNumber).ToList();
        }

    }
    /// <summary>
    /// 检验方式转换配置CRUD
    /// </summary>

    internal class  InspectionModeSwithConfigCrud:CrudBase<InspectionModeSwitchConfigModel, IInspectionModeSwitchConfigRepository>
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
            return irep.Entities.Where(e => e.SwitchCategory == swithCategory && e.CurrentStatus == currentStatus ).ToList();
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
        /// <summary>
        /// 保存数库
        /// </summary>
        /// <param name="ModelList"></param>
        /// <returns></returns>
        internal OpResult StoreModeSwithConfigModelList(string inspectionModeType, List<InspectionModeSwitchConfigModel>modelList)
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
            if (i == modelList.Count)
            {
                opResult.Entity = modelList;
               
            }
            return opResult;
        }
    }

    #region  IQC  IQC物料检验配置 Crud
    /// <summary>
    /// IQC物料检验配置
    /// </summary>
   internal class InspectionIqcItemConfigCrud : CrudBase<InspectionIqCItemConfigModel, IIqcInspectionItemConfigRepository>
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
        private OpResult DeleteInspectionItemConfig(InspectionIqCItemConfigModel model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult EidtInspectionItemConfig(InspectionIqCItemConfigModel model)
        {

            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OpResult AddInspectionItemConfig(InspectionIqCItemConfigModel model)
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
        public List<InspectionIqCItemConfigModel> FindIqcInspectionItemConfigDatasBy(string materialId)
        {
            return irep.Entities.Where(e => e.MaterialId == materialId).OrderBy(e => e.InspectionItemIndex).ToList();
        }

      
        /// <summary>
        /// 批量保存 IQC检验项目数据
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        internal OpResult StoreInspectionItemConfiList(List<InspectionIqCItemConfigModel> modelList)
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
        internal InspectionIqcMasterModel  GetIqcInspectionMasterModelListBy(string orderId, string materialId)
        {
            return irep.Entities.FirstOrDefault(e => e.OrderId == orderId && e.MaterialId == materialId);
        }
        internal bool IsExistOrderIdAndMaterailId(string orderId, string materialId)
        {
            return irep.IsExist(e => e.OrderId == orderId && e.MaterialId == materialId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inspectionStatus"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>

        internal List<InspectionIqcMasterModel> GetIqcInspectionMasterModelListBy(string inspectionStatus, DateTime  startTime,DateTime endTime)
        {
            return irep.Entities.Where(e => e.InspectionStatus == inspectionStatus && e.MaterialInDate >= startTime && e.MaterialInDate <= endTime).ToList();
        }
        internal List<InspectionIqcMasterModel> GetIqcInspectionMasterModelListBy( DateTime startTime, DateTime endTime)
        {
            DateTime starttime = startTime.ToDate();
            DateTime endtime = startTime.ToDate();
            return irep.Entities.Where(e => e.MaterialInDate >= starttime && e.MaterialInDate <= endtime).ToList();
        }
    } 
    /// <summary>
    ///进料检验单（ERP） 物料检验项次录入数据
    /// </summary>
    internal class InspectionIqcDetailCrud : CrudBase<InspectionIqcDetailModel, IIqcInspectionDetailRepository>
    {
       public InspectionIqcDetailCrud() : base(new IqcInspectionDetailRepository(), "物料检验项次数据")
        {}

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
        internal List< InspectionIqcDetailModel> GetIqcInspectionDetailModelBy(string orderid, string materialId)
        {
            return irep.Entities.Where(e => e.OrderId == orderid && e.MaterialId == materialId ).ToList() ;
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
            var inspectionItemsRecords = irep.Entities.Where(e => e.MaterialId == materialId).Distinct();
            //如果第一次打印 
            if (inspectionItemsRecords == null | inspectionItemsRecords.Count() <= 0) return true;

            // 进料日期后退30天 抽测打印记录
            var inspectionItemsMonthRecord = (from t in inspectionItemsRecords
                                              where t.MaterialInDate >= (materialInDate.AddDays(-30))
                                                    & t.MaterialInDate <= materialInDate
                                              select t.MaterialId).Distinct();
            //没有 测
            if (inspectionItemsMonthRecord == null | inspectionItemsMonthRecord.Count() <= 0) return true;
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
        internal  bool JudgeMaterialTwoYearIsRecord(string sampleMaterial)
        {
            var nn = irep.Entities.Where(e => e.MaterialInDate >= DateTime.Now.AddYears(-2));
            if (nn != null || nn.Count() > 0)
                return true;
            else return false;
        }
    }

    #endregion
 
}
