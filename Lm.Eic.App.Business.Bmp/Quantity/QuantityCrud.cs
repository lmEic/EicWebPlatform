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

  }
    internal class   SampleIpqcItemsCrud:CrudBase <SampleItemsIpqcDataModel,ISampleItemsIpqcDataReosity>
    {
        public  SampleIpqcItemsCrud() :base (new SampleItemsIpqcDataReosity())
        {

        }
   
    }
}
                                                               