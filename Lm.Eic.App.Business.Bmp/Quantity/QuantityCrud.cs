using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeExtension.Validation;
using Lm.Eic.App.DbAccess.Bpm.Mapping.QuantityMapping;
using Lm.Eic.App.DbAccess.Bpm.Repository.QuantityRep;
using Lm.Eic.App.DomainModel.Bpm.Quanity;

namespace Lm.Eic.App.Business.Bmp.Quantity
{
  internal  class QuantityCrudFactory
  {
      public static   SampleIpqcItemsCrud SampleIpqcItemsCrud
      {
          get { return OBulider.BuildInstance<SampleIpqcItemsCrud>(); }
      }
  }
    internal class  SampleIpqcItemsCrud:CrudBase <SampleItemsIpqcDataModel,ISampleItemsIpqcDataReosity>
    {
        public  SampleIpqcItemsCrud() :base (new SampleItemsIpqcDataReosity())
        { }

        public OpResult  store(SampleItemsIpqcDataModel model)
        {
            return this.StoreEntity(model, mdl =>
            {
              
                var result = this.PersistentDatas(model,
                    add =>
                    {
                        return AddSampleIpqcItemsData(model);
                    },
                    updata =>
                    {
                        return UpdataSampleIpqcItemsData(model);
                    }
                  );
                return result;
            });
        }

        private OpResult AddSampleIpqcItemsData(SampleItemsIpqcDataModel model)
        {

            return irep.Insert(model).ToOpResult_Add("数据增加完成");
        }
        private OpResult UpdataSampleIpqcItemsData(SampleItemsIpqcDataModel model)
        {
            return irep.Update(e => e.Id_key == model.Id_key, model).ToOpResult("编辑数据");
        }
        private   List<SampleItemsIpqcDataModel> FindBy()

    }

}
                                                               