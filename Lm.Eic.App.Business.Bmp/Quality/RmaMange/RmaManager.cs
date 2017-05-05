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
        /// 创建Ram表单管理
        /// </summary>
        public RmaReport RmaReport
        {
            get { return OBulider.BuildInstance<RmaReport>(); }
        }
        /// <summary>
        /// 业务处理表单
        /// </summary>
        public BussesDescription BussesManage
        {
            get { return OBulider.BuildInstance<BussesDescription>(); }
        }

        /// <summary>
        /// 品保结案处理
        /// </summary>
        public InspecitonManage InspecitonManage
        {
            get { return OBulider.BuildInstance<InspecitonManage>(); }
        }


    }


}
