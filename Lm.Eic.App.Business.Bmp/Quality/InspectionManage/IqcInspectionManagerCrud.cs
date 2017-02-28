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
    #region  IQC  IQC物料检验配置
    /// <summary>
    /// IQC物料检验配置
    /// </summary>
    public class InspectionItemConfigCrud : CrudBase<IqcInspectionItemConfigModel, IIqcInspectionItemConfigRepository>
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
            OpResult opResult = OpResult.SetResult(OpContext);
            opResult= irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
            opResult.Attach = model;
            return opResult;
        }

        private OpResult EidtInspectionItemConfig(IqcInspectionItemConfigModel model)
        {
            OpResult opResult = OpResult.SetResult(OpContext);
            opResult= irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
            opResult.Attach = model;
            return opResult;
        }

        private OpResult AddInspectionItemConfig(IqcInspectionItemConfigModel model)
        {
            OpResult opResult = OpResult.SetResult(OpContext);
            opResult= irep.Insert(model).ToOpResult_Add(OpContext);
            opResult.Attach = model;
            return opResult;
        }




        public bool IsExistInspectionConfigItem(string materialId, string inspectionItem)
        {
            return this.irep.IsExist(e => e.MaterialId == materialId && e.InspectionItem == inspectionItem);
        }
        public List<IqcInspectionItemConfigModel> FindIqcInspectionItemConfigsBy(string materialId)
        {
            return irep.Entities.Where(e => e.MaterialId == materialId).OrderBy(e => e.InspectionItemIndex).ToList();
        }
        /// <summary>
        /// 批量保存 IQC检验项目数据
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult StoreInspectionItemConfiList(List<IqcInspectionItemConfigModel> modelList)
        {
            OpResult opResult = OpResult.SetResult("未执行任何操作！");
            SetFixFieldValue(modelList, OpMode.Add);
            int i = 0;
            //如果存在 就修改   
            modelList.ForEach(m =>
            {
                if (IsExistInspectionConfigItem(m.MaterialId, m.InspectionItem))
                { m.OpSign = "edit";}
                opResult = this.Store(m);
                if (opResult.Result)
                i =i + opResult.RecordCount ;
            });
            opResult = i.ToOpResult(OpContext);
            if (i == modelList.Count)   opResult.Attach = modelList;
            return opResult;

           
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
