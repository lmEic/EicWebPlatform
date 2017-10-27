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

        public static FactoryInventoryDto InventoryDto
        {
            get
            {
                return new FactoryInventoryDto()
                {
                    factoryInventoryList = new List<SccFactoryInventory>() {
                        new SccFactoryInventory() {
                              customerCode="157",
                              faultQty=0,
                              goodQuantity =1000,
                              inspectQty =0,
                              stockTime=DateTime.Now.ToDateStr(),
                              vendorFactoryCode="421072-001",
                              vendorItemCode="349213C0350P0RT",
                              vendorItemRevision="",
                              vendorLocation="",
                              vendorStock=""
                        },
                    }
                };
            }
        }

        public static VendorItemRelationDto VendorItems
        {
            get
            {
                return new VendorItemRelationDto
                {
                    vendorItemList = new List<SccVendorItemRelationVO>() {
                        new SccVendorItemRelationVO() {
                             itemCategory= "BOSA Pigtail",
                             vendorItemCode= "349213C0350P0RT",
                             customerVendorCode= "157",
                             leadTime= 0,
                             vendorItemDesc= "APD",
                             unitOfMeasure= "PCS",
                             customerItemCode= "NA",
                             vendorProductModel= "",
                             customerProductModel= "",
                             inventoryType= "FG",
                             goodPercent= 1,
                             lifeCycleStatus= "MP"
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
