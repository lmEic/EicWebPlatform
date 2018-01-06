using Lm.Eic.App.DomainModel.Bpm.Pms.LeaveAsk;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.LeaveAsk
{
   public class LeaveAskService
    {

        /// <summary>
        /// 请假管理服务
        /// </summary>
        public static LeaveAskManager LeaveAskManager
        {
            get { return OBulider.BuildInstance<LeaveAskManager>(); }
        }


    }
}
