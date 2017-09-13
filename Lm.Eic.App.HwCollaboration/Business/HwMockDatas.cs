using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.HwCollaboration.Model;
using Lm.Eic.App.HwCollaboration.DbAccess;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeOOMapper;

namespace Lm.Eic.App.HwCollaboration.Business
{
    /// <summary>
    /// 测试数据
    /// </summary>
    public class HwMockDatas
    {
        public static ManPowerDto ManPowerDto
        {
            get
            {
                return new ManPowerDto()
                {
                    manpowerMainList = new List<SccManpowerMain>() {
                           new SccManpowerMain()
                           {
                             hrLeavePercent = 0.5,
                             manpowerAddQuantity = 10,
                             manpowerGapQuantity = 5,
                             manpowerTotalQuantity = 1000,
                             vendorFactoryCode = "421072-001",
                             keyDeptDataList = new List<SccManpowerLine>() {
                             new SccManpowerLine() {
                                 hrAddQuantity =3,
                                 hrLeavePercent =0.3,
                                 hrGapQuantity =4,
                                 hrRequestQuantity =5,
                                 keyDeptName ="EIC"
                             }
                           }
                        }
                    }
                };
            }
        }
        /// <summary>
        /// 数据传输日志
        /// </summary>
        public static HwDataTransferLog OpLog
        {
            get
            {
                return new HwDataTransferLog()
                {
                    OpModule = "人力信息",
                    OpPerson = "杨垒",
                    OpSign = "add"
                };
            }
        }
    }
}
