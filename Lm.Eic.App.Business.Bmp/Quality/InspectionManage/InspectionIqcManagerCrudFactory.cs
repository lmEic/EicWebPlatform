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
        /// IQC物料检验配置CRUD
        /// </summary>
        public static InspectionIqcItemConfigCrud InspectionIqcItemConfigCrud
        {
            get { return OBulider.BuildInstance<InspectionIqcItemConfigCrud>(); }
        }
        /// <summary>
        /// 物料检验项次CRUD
        /// </summary>
        public static IqcInspectionMasterCrud IqcInspectionMasterCrud
        {
            get { return OBulider.BuildInstance<IqcInspectionMasterCrud>(); }
        }
        /// <summary>
        ///  物料检验项次数据CRUD
        /// </summary>
        public static IqcInspectionDetailCrud IqcInspectionDetailCrud
        {
            get { return OBulider.BuildInstance<IqcInspectionDetailCrud>(); }
        }
    }

    #region  IQC  IQC物料检验配置 Crud
    /// <summary>
    /// IQC物料检验配置
    /// </summary>
    public class InspectionIqcItemConfigCrud : CrudBase<InspectionIqCItemConfigModel, IIqcInspectionItemConfigRepository>
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

        public bool IsExistInspectionConfigmaterailId(string materailId)
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
        public OpResult StoreInspectionItemConfiList(List<InspectionIqCItemConfigModel> modelList)
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
    #endregion

    /// <summary>
    /// 进料检验单（ERP）  物料检验项次
    /// </summary>
    internal class IqcInspectionMasterCrud : CrudBase<InspectionIqcMasterModel, IIqcInspectionMasterRepository>
    {
        public IqcInspectionMasterCrud() : base(new IqcInspectionMasterRepository(), "物料检验")
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
        public List<InspectionIqcMasterModel> GetIqcInspectionMasterModelList(string orderId, string materialId)
        {


            return irep.Entities.Where(e => e.OrderId == orderId && e.MaterialId == materialId).ToList();
        }
    }
    /// <summary>
    ///进料检验单（ERP） 物料检验项次录入数据
    /// </summary>
    internal class IqcInspectionDetailCrud : CrudBase<InspectionIqcDetailModel, IIqcInspectionDetailRepository>
    {
        public IqcInspectionDetailCrud() : base(new IqcInspectionDetailRepository(), "物料检验项次数据")
        {
        }

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
        public List<InspectionIqcDetailModel> GetIqcInspectionDetailModelListBy(string orderid, string materialId)
        {
            return irep.Entities.Where(e => e.OrderId == orderid && e.MaterialId == materialId).ToList(); ;
        }

        public InspectionIqcDetailModel GetIqcInspectionDetailModelBy(string orderid, string materialId, string inspectionItem)
        {
            return irep.Entities.Where(e => e.OrderId == orderid && e.MaterialId == materialId && e.InspecitonItem == inspectionItem).ToList().FirstOrDefault(); ;
        }
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
        public List<InspectionModeConfigModel> GetInspectionStartEndNumberBy(string inspectionMode,string inspectionLevel,string inspectionAQL)
        {
            return irep.Entities.Where(e => e.InspectionMode == inspectionMode && e.InspectionLevel == inspectionLevel && e.InspectionAQL == inspectionAQL).OrderBy (e=>e.StartNumber).ToList();
        }
        
    }
}
