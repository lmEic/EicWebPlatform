using Lm.Eic.App.Business.Bmp.Dev.DesignDevelop;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Dev
{
  public  class DevelopSerivce
    {
        public static ProductsDesignDevelopManger DesignDevManager
        {
            get { return OBulider.BuildInstance<ProductsDesignDevelopManger>(); }
        }

    }
}
