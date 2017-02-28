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

    internal class IqcInspectionManagerCrudFactory
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
    /// <summary>
    /// 检验方式配置
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
            OpResult opResult = OpResult.SetResult(OpContext);
            opResult = irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
            opResult.Attach = model;
            return opResult;
        }

        private OpResult EidtInspectionModeConfig(InspectionModeConfigModel model)
        {
            OpResult opResult = OpResult.SetResult(OpContext);
            opResult= irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
            opResult.Attach = model;
            return opResult;
           
        }

        private OpResult AddInspectionModeConfig(InspectionModeConfigModel model)
        {

            OpResult opResult = OpResult.SetResult(OpContext);
            opResult = irep.Insert(model).ToOpResult_Add(OpContext);
            opResult.Attach = model;
            return opResult;
       
        }
    }
}
