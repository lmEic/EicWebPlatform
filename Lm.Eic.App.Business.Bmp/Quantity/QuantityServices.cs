using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.App.Business.Bmp.Quantity.SampleManager;

namespace Lm.Eic.App.Business.Bmp.Quantity
{
    /// <summary>
    ///质量管理系统接口门面
    /// </summary>
    public static   class QuantityServices
    {
       public static SampleManger  SampleManger
     {
         get { return OBulider.BuildInstance<SampleManger>(); }
     }
    }
}
