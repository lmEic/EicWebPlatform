using Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
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
        public OpResult StoreFqcItemConfigList(List<InspectionFqcItemConfigModel> modelList)
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


    internal class InspectionFqcDatailCrud : CrudBase<InspectionFqcDetailModel, IFqcInspectionDatailRepository>
    {
        public InspectionFqcDatailCrud() : base(new FqcInspectionDatailRepository(), "FQC详细表单")
        { }
        #region Crud
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddFqcInspectionDetail);
        }



        private OpResult AddFqcInspectionDetail(InspectionFqcDetailModel model)
        {
            if (model == null) return new OpResult("保存文件不能为空", false);
            //如果存在 (Id_key 已经赋值） 
            if (IsExist(model.OrderId, model.OrderIdNumber, model.InspectionItem))
            {
                if (model.Id_Key <= 0) return new OpResult("Id_key 没有赋值", false);
                return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
            }
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
        internal OpResult UploadFileFqcInspectionDetail(InspectionFqcDetailModel model, string siteRootPath)
        {
            if (model == null) return new OpResult("采集数据模型不能为NULL", false);
            var oldmodel = this.GetFqcOldDetailModelBy(model);
            if (oldmodel == null)
                return this.AddFqcInspectionDetail(model);//若不存在则直接添加
            model.Id_Key = oldmodel.Id_Key;
            if (oldmodel.DocumentPath != model.DocumentPath && oldmodel.DocumentPath != string.Empty && oldmodel.DocumentPath != null)//比对新旧文件是否一样,若不一样，则删除旧的文件
            {
                if (siteRootPath != string.Empty && siteRootPath != null)
                {
                    string fileName = Path.Combine(siteRootPath, oldmodel.DocumentPath);
                    //if (fileName.Contains("/"))
                    //{
                    fileName = fileName.Replace("/", @"\");
                    if (File.Exists(fileName))
                        File.Delete(fileName);
                    //}
                }//删除旧的文件
            }
            this.SetFixFieldValue(model);
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);// 同时修改文件模型记录
        }
        #endregion


        #region find method
        public List<InspectionFqcDetailModel> GetFqcInspectionDetailDatasBy(string orderId)
        {
            return irep.Entities.Where(e => e.OrderId == orderId).ToList();
        }

        public List<InspectionFqcDetailModel> GetFqcInspectionDetailDatasBy(string orderId, int orderIdNumber)
        {
            return irep.Entities.Where(e => e.OrderId == orderId && e.OrderIdNumber == orderIdNumber).ToList();
        }
        public bool IsExist(string orderId, int orderIdNumber, string inspecitonItem)
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
        public InspectionFqcDetailModel GetFqcOldDetailModelBy(InspectionFqcDetailModel newmodel)
        {
            try
            {
                if (newmodel == null) return null;
                return irep.Entities.FirstOrDefault(e => e.OrderId == newmodel.OrderId && e.OrderIdNumber == newmodel.OrderIdNumber && e.InspectionItem == newmodel.InspectionItem);
            }
            catch (Exception ex)
            {
                return null;
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
                return irep.Entities.FirstOrDefault(e => e.Id_Key == id_key);
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception(ex.InnerException.Message);
            }

        }
        #endregion
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
        internal bool IsExsitStoreModel(InspectionFqcMasterModel newModel)
        {
            if (newModel == null) return false;
            return irep.IsExist(e => e.OrderId == newModel.OrderId && e.OrderIdNumber == newModel.OrderIdNumber && e.MaterialId == newModel.MaterialId);
        }

        internal InspectionFqcMasterModel GetStroeOldModel(InspectionFqcMasterModel newModel)
        {
            try
            {
                if (!IsExsitStoreModel(newModel)) return null;
                return irep.Entities.FirstOrDefault(e => e.OrderId == newModel.OrderId && e.OrderIdNumber == newModel.OrderIdNumber && e.MaterialId == newModel.MaterialId);
            }
            catch (Exception ex)
            {
                return null;
                throw new Exception(ex.InnerException.Message);
            }
        }
        private OpResult AddFqcInspectionMaster(InspectionFqcMasterModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
        internal List<InspectionFqcMasterModel> GetFqcInspectionMasterModelListBy(string orderId)
        {
            return irep.Entities.Where(e => e.OrderId == orderId).ToList();
        }

        internal List<InspectionFqcMasterModel> GetFqcInspectionMasterModelListBy(string formStatus, DateTime dateFrom, DateTime dateTo)
        {
            return irep.Entities.Where(e => e.InspectionStatus == formStatus && e.MaterialInDate >= dateFrom && e.MaterialInDate <= dateTo).ToList();
        }
        internal List<InspectionFqcMasterModel> GetFqcInspectionMasterListBy(string materialId)
        {
            return irep.Entities.Where(e => e.MaterialId == materialId).ToList();
        }

        internal OpResult UpAuditDetailData(string orderId, int orderIdNumber, string Updatestring)
        {
            return irep.UpAuditDetailData(orderId, orderIdNumber, Updatestring).ToOpResult_Eidt(OpContext);
        }
    }
    #endregion
}