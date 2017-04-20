using System;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;

namespace Lm.Eic.App.Business.Bmp.Quality.RmaMange
{
    internal class RmaCurdFactory
    {
        internal static RmaReportInitiateCurd RmaReportInitiate
        {
            get { return OBulider.BuildInstance<RmaReportInitiateCurd>(); }
        }

        internal static RmaBussesDescriptionCurd RmaBussesDescription
        {
            get { return OBulider.BuildInstance<RmaBussesDescriptionCurd>(); }
        }


        internal static RmaInspectionManageCurd RmaInspectionManage
        {
            get { return OBulider.BuildInstance<RmaInspectionManageCurd>(); }
        }


    }

    internal class RmaReportInitiateCurd : CrudBase<RmaReportInitiateModel, IRmaReportInitiateRepository>
    {
        public RmaReportInitiateCurd() : base(new RmaReportInitiateRepository(), "创建表单")
        { }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddModel);
            this.AddOpItem(OpMode.UpDate, Update);
            this.AddOpItem(OpMode.Delete, DeleteModel);
        }

        protected OpResult AddModel(RmaReportInitiateModel model)
        {
            throw new NotImplementedException();
        }

        protected OpResult DeleteModel(RmaReportInitiateModel model)
        {
            throw new NotImplementedException();
        }

        protected OpResult Update(RmaReportInitiateModel model)
        {
            throw new NotImplementedException();
        }
    }



    internal class RmaBussesDescriptionCurd : CrudBase<RmaBussesDescriptionModel, IRmaBussesDescriptionRepository>
    {
        public RmaBussesDescriptionCurd() : base(new RmaBussesDescriptionRepository(), "记录登记表单")
        {
        }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddModel);
            this.AddOpItem(OpMode.UpDate, Update);
            this.AddOpItem(OpMode.Delete, DeleteModel);
        }

        protected OpResult AddModel(RmaBussesDescriptionModel arg)
        {
            throw new NotImplementedException();
        }

        protected OpResult DeleteModel(RmaBussesDescriptionModel arg)
        {
            throw new NotImplementedException();
        }

        protected OpResult Update(RmaBussesDescriptionModel arg)
        {
            throw new NotImplementedException();
        }
    }


    internal class RmaInspectionManageCurd : CrudBase<RmaInspectionManageModel, IRmaInspectionManageRepository>
    {
        public RmaInspectionManageCurd() : base(new RmaInspectionManageRepository(), "Ram检验处理")
        {
        }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddModel);
            this.AddOpItem(OpMode.UpDate, Update);
            this.AddOpItem(OpMode.Delete, DeleteModel);
        }

        protected OpResult AddModel(RmaInspectionManageModel arg)
        {
            throw new NotImplementedException();
        }

        protected OpResult DeleteModel(RmaInspectionManageModel arg)
        {
            throw new NotImplementedException();
        }

        protected OpResult Update(RmaInspectionManageModel arg)
        {
            throw new NotImplementedException();
        }
    }

}
