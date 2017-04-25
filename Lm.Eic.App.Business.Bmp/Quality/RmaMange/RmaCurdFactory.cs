using System;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using System.Collections.Generic;
using System.Linq;

namespace Lm.Eic.App.Business.Bmp.Quality.RmaMange
{
    internal class RmaCurdFactory
    {
        internal static RmaReportInitiateCrud RmaReportInitiate
        {
            get { return OBulider.BuildInstance<RmaReportInitiateCrud>(); }
        }

        internal static RmaBussesDescriptionCrud RmaBussesDescription
        {
            get { return OBulider.BuildInstance<RmaBussesDescriptionCrud>(); }
        }


        internal static RmaInspectionManageCrud RmaInspectionManage
        {
            get { return OBulider.BuildInstance<RmaInspectionManageCrud>(); }
        }


    }

    internal class RmaReportInitiateCrud : CrudBase<RmaReportInitiateModel, IRmaReportInitiateRepository>
    {
        public RmaReportInitiateCrud() : base(new RmaReportInitiateRepository(), "创建表单")
        { }
        #region  CRUD
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddModel);
            this.AddOpItem(OpMode.UpDate, Update);
        }

        OpResult AddModel(RmaReportInitiateModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
        OpResult Update(RmaReportInitiateModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        #endregion

        #region  Find
        internal string CreateNewRmaID()
        {
            ///以R开头 年份 月份  再加序序号000
            string nowYaer = DateTime.Now.ToString("yy");
            string nowMonth = DateTime.Now.ToString("MM");
            var count = irep.Entities.Count(e => e.RmaYear == nowYaer && e.RmaMonth == nowMonth) + 1;
            return "R" + nowYaer + nowMonth + count.ToString("000");

        }


        internal RmaReportInitiateModel GetInitiateData(string rmaId)
        {
            return irep.Entities.FirstOrDefault(e => e.RmaId == rmaId);
        }


        internal bool IsExist(string rmaId)
        {

            return irep.IsExist(e => e.RmaId == rmaId);
        }
        #endregion

    }



    internal class RmaBussesDescriptionCrud : CrudBase<RmaBussesDescriptionModel, IRmaBussesDescriptionRepository>
    {
        public RmaBussesDescriptionCrud() : base(new RmaBussesDescriptionRepository(), "记录登记表单")
        {
        }
        #region  CRUD
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddModel);
            this.AddOpItem(OpMode.UpDate, Update);
            this.AddOpItem(OpMode.Delete, DeleteModel);
        }

        OpResult AddModel(RmaBussesDescriptionModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }

        OpResult DeleteModel(RmaBussesDescriptionModel model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }

        OpResult Update(RmaBussesDescriptionModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        public List<RmaBussesDescriptionModel> GetRmaBussesDescriptionDatas(string rmaId)
        {
            return irep.Entities.Where(e => e.RmaId == rmaId).ToList();
        }
        #endregion



    }


    internal class RmaInspectionManageCrud : CrudBase<RmaInspectionManageModel, IRmaInspectionManageRepository>
    {
        public RmaInspectionManageCrud() : base(new RmaInspectionManageRepository(), "Ram检验处理")
        {
        }
        #region  CRUD
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddModel);
            this.AddOpItem(OpMode.UpDate, Update);
            this.AddOpItem(OpMode.Delete, DeleteModel);
        }

        OpResult AddModel(RmaInspectionManageModel model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }

        OpResult DeleteModel(RmaInspectionManageModel model)
        {
            return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
        }

        OpResult Update(RmaInspectionManageModel model)
        {
            return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }
        #endregion





    }

}
