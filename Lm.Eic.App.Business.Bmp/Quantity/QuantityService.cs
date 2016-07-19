using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.App.Business.Bmp.Quantity.SampleItermLaw;



namespace Lm.Eic.App.Business.Bmp.Quantity
{
    public static class QuantityService
    {
        /// <summary>
        /// 单号抽样项目打印记录管理
        /// </summary>
       public static  IQCSampleItemsRecordManager IQCSampleItemsRecordManager
       { 
          get { return OBulider.BuildInstance<IQCSampleItemsRecordManager>(); }
       }
       
        
        /// <summary>
         /// 单号物料抽样记录管理
         /// </summary>
       public static SampleRecordManager SampleRecordManager
       {
           get { return OBulider.BuildInstance<SampleRecordManager>(); }
       }
        
        
        
        /// <summary>
        /// 物料数量抽样规则管理
        /// </summary>
        public static  SamplePlanTableManger  SamplePlanTableManger
       { 
            get { return OBulider.BuildInstance<SamplePlanTableManger>(); } 
        }
        /// <summary>
        ///  物料抽样项目管理
        /// </summary>
        public static MaterialSampleItemManager MaterialSampleItemManager
        {
            get { return OBulider.BuildInstance<MaterialSampleItemManager>(); }
        }
        /// <summary>
        /// 放宽加严重规规则管理
        /// </summary>
        public static SampleWayLawManger SampleItermLawManger
        {
            get { return OBulider.BuildInstance<SampleWayLawManger>(); }
        }
    }
}
