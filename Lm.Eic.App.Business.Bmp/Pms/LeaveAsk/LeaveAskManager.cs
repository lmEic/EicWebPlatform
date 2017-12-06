using Lm.Eic.App.DomainModel.Bpm.Pms.LeaveAsk;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.LeaveAsk
{
   public class LeaveAskManager
    {

        /// <summary>
        /// 保存请假数据
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public OpResult StoreLeaveAskDatas(LeaveAskModels models)
        {
            try
            {
                return LeaveAskFactory.LeaveAskCrud.Store(models);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


    }
}
