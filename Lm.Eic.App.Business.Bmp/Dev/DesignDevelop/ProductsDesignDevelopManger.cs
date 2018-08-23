using Lm.Eic.App.DomainModel.Bpm.Dev;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Dev.DesignDevelop
{
 public  class ProductsDesignDevelopManger
    {
        public OpResult StoreDesignModel(DesignDevelopInputModel model)
        {
            return DevelopManagerCrudFactory.DesignModeCrud.Store(model, true);
        }
    }
}
