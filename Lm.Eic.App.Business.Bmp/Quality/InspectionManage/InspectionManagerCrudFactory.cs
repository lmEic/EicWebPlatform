using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
        internal static InspectionModeConfigCrud InspectionModeConfigCrud
        {
            get { return OBulider.BuildInstance<InspectionModeConfigCrud>(); }
        }

        /// <summary>
        /// 检验方式配置CRUD
        /// </summary>
        internal static InspectionModeSwithConfigCrud InspectionModeSwithConfigCrud
        {
            get { return OBulider.BuildInstance<InspectionModeSwithConfigCrud>(); }
        }

        #region IQC Crud
        /// <summary>
        /// IQC物料检验配置CRUD
        /// </summary>
        internal static InspectionIqcItemConfigCrud IqcItemConfigCrud
        {
            get { return OBulider.BuildInstance<InspectionIqcItemConfigCrud>(); }
        }
        /// <summary>
        /// 物料检验项次CRUD
        /// </summary>
        internal static InspectionIqcMasterCrud IqcMasterCrud
        {
            get { return OBulider.BuildInstance<InspectionIqcMasterCrud>(); }
        }
        /// <summary>
        ///  物料检验项次数据CRUD
        /// </summary>
        internal static InspectionIqcDetailCrud IqcDetailCrud
        {
            get { return OBulider.BuildInstance<InspectionIqcDetailCrud>(); }
        }

        #endregion


        #region  FQC CRUD


        internal static InspectionFqcItemConfigCrud FqcItemConfigCrud
        {
            get { return OBulider.BuildInstance<InspectionFqcItemConfigCrud>(); }
        }

        internal static InspectionFqcDatailCrud FqcDetailCrud
        {
            get { return OBulider.BuildInstance<InspectionFqcDatailCrud>(); }
        }

        internal static InspectionFqcMasterCrud FqcMasterCrud
        {
            get { return OBulider.BuildInstance<InspectionFqcMasterCrud>(); }
        }
        internal static OrtMaterialConfigCrud OrtMaterialConfigCrud
        {
            get { return OBulider.BuildInstance<OrtMaterialConfigCrud>(); }
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
                OpResult opResult = OpResult.SetErrorResult("未执行任何操作！");
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
                return OpResult.SetErrorResult("未执行任何操作！");
                throw new Exception(ex.InnerException.Message);
            }

        }
    }

}
