using System;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeObjectBuilder;

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

    internal class RmaReportInitiateCurd : QualityModelEntityCurdBase<RmaReportInitiateModel>
    {
        public RmaReportInitiateCurd() : base("创建表单")
        { }
        protected override OpResult AddModel(RmaReportInitiateModel arg)
        {
            throw new NotImplementedException();
        }

        protected override OpResult DeleteModel(RmaReportInitiateModel arg)
        {
            throw new NotImplementedException();
        }

        protected override OpResult Update(RmaReportInitiateModel arg)
        {
            throw new NotImplementedException();
        }
    }



    internal class RmaBussesDescriptionCurd : QualityModelEntityCurdBase<RmaBussesDescriptionModel>
    {
        public RmaBussesDescriptionCurd() : base("记录登记表单")
        {
        }

        protected override OpResult AddModel(RmaBussesDescriptionModel arg)
        {
            throw new NotImplementedException();
        }

        protected override OpResult DeleteModel(RmaBussesDescriptionModel arg)
        {
            throw new NotImplementedException();
        }

        protected override OpResult Update(RmaBussesDescriptionModel arg)
        {
            throw new NotImplementedException();
        }
    }


    internal class RmaInspectionManageCurd : QualityModelEntityCurdBase<RmaInspectionManageModel>
    {
        public RmaInspectionManageCurd() : base("Ram检验处理")
        {
        }

        protected override OpResult AddModel(RmaInspectionManageModel arg)
        {
            throw new NotImplementedException();
        }

        protected override OpResult DeleteModel(RmaInspectionManageModel arg)
        {
            throw new NotImplementedException();
        }

        protected override OpResult Update(RmaInspectionManageModel arg)
        {
            throw new NotImplementedException();
        }
    }

}
