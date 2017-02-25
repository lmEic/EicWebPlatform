using Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
 internal  class IqcInspectionManagerCrudFactory
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
        public static InspectionItemConfigCrud InspectionItemConfigCrud
        {
            get { return OBulider.BuildInstance<InspectionItemConfigCrud>(); }
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


    #region  IQC
    /// <summary>
    /// IQC物料检验配置
    /// </summary>
    public  class InspectionItemConfigCrud : CrudBase<IqcInspectionItemConfigModel, IIqcInspectionItemConfigRepository>
    {
        public InspectionItemConfigCrud():base(new IqcInspectionItemConfigRepository (),"IQC物料检验配置")
            { }
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddInspectionItemConfig);
            this.AddOpItem(OpMode.Edit, EidtInspectionItemConfig);
            this.AddOpItem(OpMode.Delete, DeleteInspectionItemConfig);
        }

        private OpResult DeleteInspectionItemConfig(IqcInspectionItemConfigModel model)
        {

            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }

        private OpResult EidtInspectionItemConfig(IqcInspectionItemConfigModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }

        private OpResult AddInspectionItemConfig(IqcInspectionItemConfigModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }

        public bool IsExistInspectionConfigItem(string materialId, string inspectionItem)
        {
            return irep.IsExist(e => e.MaterialId == materialId && e.InspectionItem == inspectionItem);
        }
        public List<IqcInspectionItemConfigModel> FindIqcInspectionItemConfigsBy(string materialId)
        {
            var listmodel = irep.Entities.Where(e => e.MaterialId == materialId).OrderBy(e => e.InspectionItemIndex).ToList();
          
            return listmodel;
        }
    }




    /// <summary>
    /// 物料检验项次
    /// </summary>
    internal class IqcInspectionMasterCrud : CrudBase<IqcInspectionMasterModel, IIqcInspectionMasterRepository>
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

        private OpResult DeleteIqcInspectionMaster(IqcInspectionMasterModel model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }

        private OpResult EidtIqcInspectionMaster(IqcInspectionMasterModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }

        private OpResult AddIqcInspectionMaster(IqcInspectionMasterModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
    }


    /// <summary>
    /// 物料检验项次数据
    /// </summary>
    internal class IqcInspectionDetailCrud : CrudBase<IqcInspectionDetailModel, IIqcInspectionDetailRepository>
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

        private OpResult DeleteIqcInspectionDetail(IqcInspectionDetailModel model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }

        private OpResult EidtIqcInspectionDetail(IqcInspectionDetailModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }

        private OpResult AddIqcInspectionDetail(IqcInspectionDetailModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
    }

    #endregion


}
