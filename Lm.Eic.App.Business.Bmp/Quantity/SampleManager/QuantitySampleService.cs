using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.Uti.Common.YleeObjectBuilder;




namespace Lm.Eic.App.Business.Bmp.Quantity.SampleManger
{
    public static class QuantitySampleService
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
        public static  SampleRuleTableManger  SamplePlanTableManger
       { 
            get { return OBulider.BuildInstance<SampleRuleTableManger>(); } 
        }
        /// <summary>
        ///  物料抽样项目管理
        /// </summary>
        public static MaterialSampleItemManager MaterialSampleItemManager
        {
            get { return OBulider.BuildInstance<MaterialSampleItemManager>(); }
        }
        /// <summary>
        /// 抽样方法法规则管理
        /// </summary>
        public static SampleWayLawManger SampleItermLawManger
        {
            get { return OBulider.BuildInstance<SampleWayLawManger>(); }
        }
    }
}
