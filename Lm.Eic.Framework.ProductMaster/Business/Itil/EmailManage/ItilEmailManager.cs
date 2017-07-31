using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Business.Itil
{
   public class ItilEmailManager
    {

       
        public List<ItilEmailManageModel>GetMails(ItilEmailManageModelDto dto)
        {

            return ItilEmailFactory.ItilEmailManageCrud.FindBy(dto);
         
        }
        public OpResult StoreMails(ItilEmailManageModel model)
        {
            try
            {
                return ItilEmailFactory.ItilEmailManageCrud.Store(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
       
    }
}
