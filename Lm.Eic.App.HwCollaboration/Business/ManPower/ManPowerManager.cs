using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.HwCollaboration.Business;
using Lm.Eic.App.HwCollaboration.Model;

namespace Lm.Eic.App.HwCollaboration.Business.ManPower
{
    /// <summary>
    /// 人力管理器
    /// </summary>
    public class ManPowerManager : HwCollaborationBase<ManPowerDto>
    {
        protected override void SetApiUrlAndDto()
        {
            this.apiUrl = "https://api-beta.huawei.com:443/service/esupplier/importManpower/1.0.0";
            this.dto = new ManPowerDto()
            {
                manpowerMainList = new List<SccManpowerMain>() {
                    new SccManpowerMain()
                    {
                       hrLeavePercent = 0.5,
                       manpowerAddQuantity = 10,
                       manpowerGapQuantity = 5,
                       manpowerTotalQuantity = 1000,
                       vendorFactoryCode = "421072",
                       keyDeptDataList = new List<SccManpowerLine>() {
                       new SccManpowerLine() { hrAddQuantity=3, hrLeavePercent=0.3, hrGapQuantity=4, hrQequestQuantity=5, keyDeptName="EIC" },
                    }
                    }
                }
            };
        }
    }
}
