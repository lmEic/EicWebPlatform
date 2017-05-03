using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.RmaMange
{
    public class RmaManager
    {

        /// <summary>
        /// 添加创建Ram表单管理
        /// </summary>
        public RmaReport RmaReport
        {
            get { return OBulider.BuildInstance<RmaReport>(); }
        }

        public BusinessHandleReport BusinessHandle
        {
            get { return OBulider.BuildInstance<BusinessHandleReport>(); }
        }



        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="rmaId"></param>
        /// <returns></returns>
        public List<RmaBussesDescriptionModel> GetBussesDescriptiondatas(string rmaId)
        {
            return RmaCurdFactory.RmaBussesDescription.GetRmaBussesDescriptionDatas(rmaId);
        }
    }


}
