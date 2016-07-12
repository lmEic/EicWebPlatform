using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.Uti.Common.YleeObjectBuilder;

namespace Lm.Eic.App.Business.Bmp.Quantity
{
    public static class QuantityService
    {
      public static  IQCSampleItemsRecordManager IQCSampleItemsRecordManager
      {
           
          get { return OBulider.BuildInstance<IQCSampleItemsRecordManager>(); }
      }

        public  static MaterialSampleItemManager MaterialSampleItemManager
      {
          get { return OBulider.BuildInstance<MaterialSampleItemManager>(); }
      }
    }
}
