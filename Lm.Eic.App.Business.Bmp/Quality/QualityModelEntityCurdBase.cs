using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep;
using Lm.Eic.App.Erp.Domain.QuantityModel;
using Lm.Eic.App.Erp.Bussiness.QuantityManage;


namespace Lm.Eic.App.Business.Bmp.Quality
{
    public abstract class QualityModelEntityCurdBase<ModelEntity> : CrudBase<ModelEntity, IBpmRepositoryMdoelReository<ModelEntity>>
  where ModelEntity : class, new()
    {
        public QualityModelEntityCurdBase(string OpContext) : base(new BpmRepositoryMdoelReository<ModelEntity>(), OpContext)
        { }
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add, AddModel);
            this.AddOpItem(OpMode.UpDate, Update);
            this.AddOpItem(OpMode.Delete, DeleteModel);
        }
        protected abstract OpResult Update(ModelEntity arg);
        protected abstract OpResult AddModel(ModelEntity arg);
        protected abstract OpResult DeleteModel(ModelEntity arg);

    }
}
