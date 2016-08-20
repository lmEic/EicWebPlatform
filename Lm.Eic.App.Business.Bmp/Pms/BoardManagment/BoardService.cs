using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.Uti.Common.YleeObjectBuilder;

namespace Lm.Eic.App.Business.Bmp.Pms.BoardManagment
{
   public static  class BoardService
    {

       public  static MaterialBoardManager MaterialBoardManager
       {
           get { return OBulider.BuildInstance<MaterialBoardManager>(); }
       }
    }
}
