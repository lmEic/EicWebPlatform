using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.RmaMange
{
    public class RmaReportManager
    {
        //生成RmaId编号
        public string GetNewRmaID()
        {
            return RmaCurdFactory.RmaReportInitiate.GetNewRmaID();
        }
        /// <summary>
        /// 存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public OpResult StoreRamReortInitiate(RmaReportInitiateModel model)
        {
            return null;
        }
    }
}
