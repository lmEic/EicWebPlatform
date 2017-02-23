using Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep;
using Lm.Eic.App.DomainModel.Bpm.Qms;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Qms.InspectionManage
{
 public    class IqcInspectionManagerCrudFactory
    {
    }


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
            throw new NotImplementedException();
        }

        private OpResult EidtInspectionItemConfig(IqcInspectionItemConfigModel model)
        {
            throw new NotImplementedException();
        }

        private OpResult AddInspectionItemConfig(IqcInspectionItemConfigModel model)
        {
            throw new NotImplementedException();
        }
    }



    public class InspectionModeConfigCrud : CrudBase<InspectionModeConfigModel, IInspectionModeConfigRepository>
    {
        public InspectionModeConfigCrud() : base( new InspectionModeConfigRepository(), "检验方式配置")
        {
        }

        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddInspectionModeConfig);
            this.AddOpItem(OpMode.Edit, EidtInspectionModeConfig);
            this.AddOpItem(OpMode.Delete, DeleteInspectionModeConfig);
        }

        private OpResult DeleteInspectionModeConfig(InspectionModeConfigModel arg)
        {
            throw new NotImplementedException();
        }

        private OpResult EidtInspectionModeConfig(InspectionModeConfigModel arg)
        {
            throw new NotImplementedException();
        }

        private OpResult AddInspectionModeConfig(InspectionModeConfigModel arg)
        {
            throw new NotImplementedException();
        }
    }
}
