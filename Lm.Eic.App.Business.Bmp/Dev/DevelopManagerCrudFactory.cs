using Lm.Eic.App.DbAccess.Bpm.Repository.DevRep;
using Lm.Eic.App.DomainModel.Bpm.Dev;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Dev
{
  public   class DevelopManagerCrudFactory
    {
        /// <summary>
        /// 检验方式配置CRUD
        /// </summary>
        internal static ProudctDesignModeCrud DesignModeCrud
        {
            get { return OBulider.BuildInstance<ProudctDesignModeCrud>(); }
        }
        internal class ProudctDesignModeCrud : CrudBase<DesignDevelopInputModel, IDesignDevelopInputRepository>
        {
            public ProudctDesignModeCrud() : base(new DesignDevelopInputRepository(), "设计模板输入")
            {
            }
            protected override void AddCrudOpItems()
            {
                this.AddOpItem(OpMode.Add, AddModel);
                this.AddOpItem(OpMode.Edit, EidtModel);
                this.AddOpItem(OpMode.Delete, DeleteModel);
            }
            private OpResult AddModel(DesignDevelopInputModel model)
            {
                return irep.Insert(model).ToOpResult_Add(OpContext);
            }
            private OpResult EidtModel(DesignDevelopInputModel model)
            {
                return irep.Update(e => e.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
            }
            private OpResult DeleteModel(DesignDevelopInputModel model)
            {
                return irep.Delete(e => e.Id_Key == model.Id_Key).ToOpResult_Delete(OpContext);
            }
        }
    }
}
