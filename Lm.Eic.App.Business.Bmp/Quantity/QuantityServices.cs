using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.App.Business.Bmp.Quantity.SampleManager;

namespace Lm.Eic.App.Business.Bmp.Quantity
{
    public static   class QuantityServices
    {
       public static SampleManger  SampleManger
     {
         get { return OBulider.BuildInstance<SampleManger>(); }
     }
    }
}
