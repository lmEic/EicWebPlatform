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
       public static  SampleItemsIqcRecordManager SampleItemsIqcRecordManager
       { 
          get { return OBulider.BuildInstance<SampleItemsIqcRecordManager>(); }
       }
       
        
        /// <summary>
        /// 单号物料抽样记录管理
        /// </summary>
       public static SampleIqcRecordManager SampleIqcRecordManager
       {
           get { return OBulider.BuildInstance<SampleIqcRecordManager>(); }
       }
        /// <summary>
        /// 物料数量抽样规则管理
        /// </summary>
        public static  SampleRuleTableManger  SampleRuleTableManger
       { 
            get { return OBulider.BuildInstance<SampleRuleTableManger>(); } 
        }
        /// <summary>
        ///  物料抽样项目管理
        /// </summary>
        public static MaterialSampleItemsManager MaterialSampleItemsManager
        {
            get { return OBulider.BuildInstance<MaterialSampleItemsManager>(); }
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
