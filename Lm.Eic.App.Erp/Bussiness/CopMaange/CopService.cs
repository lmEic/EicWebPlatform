using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
namespace Lm.Eic.App.Erp.Bussiness.CopMaange
{
   public  class CopService
    {

       public static OrderManage OrderManageManager
       {
           get { return OBulider.BuildInstance<OrderManage>(); }
       }
    }
}
