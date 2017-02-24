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
           
            throw new NotImplementedException();
        }

        private OpResult EidtInspectionItemConfig(IqcInspectionItemConfigModel model)
        {
            throw new NotImplementedException();
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
            return irep.Entities.Where(e => e.MaterialId == materialId).ToList();
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

        private OpResult DeleteIqcInspectionMaster(IqcInspectionMasterModel arg)
        {
            throw new NotImplementedException();
        }

        private OpResult EidtIqcInspectionMaster(IqcInspectionMasterModel arg)
        {
            throw new NotImplementedException();
        }

        private OpResult AddIqcInspectionMaster(IqcInspectionMasterModel arg)
        {
            throw new NotImplementedException();
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

        private OpResult DeleteIqcInspectionDetail(IqcInspectionDetailModel arg)
        {
            throw new NotImplementedException();
        }

        private OpResult EidtIqcInspectionDetail(IqcInspectionDetailModel arg)
        {
            throw new NotImplementedException();
        }

        private OpResult AddIqcInspectionDetail(IqcInspectionDetailModel arg)
        {
            throw new NotImplementedException();
        }
    }

    #endregion


}
