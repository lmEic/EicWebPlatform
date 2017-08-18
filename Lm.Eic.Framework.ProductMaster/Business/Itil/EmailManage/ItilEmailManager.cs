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

        // List<ItilEmailManageModel> _ItilEmailManageModelList = new List<ItilEmailManageModel>();
        public List<ItilEmailManageModel> GetEmails(ItilEmailManageModelDto dto)
        {
            return ItilEmailFactory.ItilEmailManageCrud.FindBy(dto);

        }
        /// <summary>
        /// 存储邮箱列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreEmailManage(ItilEmailManageModel model)
        {
            try
            {
                return ItilEmailFactory.ItilEmailManageCrud.Store(model);
            }
            catch (Exception ex)
            {
                return ex.ExOpResult();
            }
        }


        //public OpResult SendEmail(ItilEmailManageModel model)
        //{

        //}
    }
}
