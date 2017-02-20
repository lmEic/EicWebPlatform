using Lm.Eic.Uti.Common.YleeObjectBuilder;

namespace Lm.Eic.App.Business.Bmp.Quantity.SampleManager
{
    public  class SampleManger
    {
        /// <summary>
        /// 单号抽样项目打印记录管理
        /// </summary>
        public   SampleItemsIqcRecordManager SampleItemsIqcRecordManager
       { 
          get { return OBulider.BuildInstance<SampleItemsIqcRecordManager>(); }
       }  
        /// <summary>
        /// 单号物料抽样记录管理
        /// </summary>
       public  SampleIqcRecordManager SampleIqcRecordManager
       {
           get { return OBulider.BuildInstance<SampleIqcRecordManager>(); }
       }
        /// <summary>
        /// 物料数量抽样规则管理
        /// </summary>
        public   SampleRuleTableManger  SampleRuleTableManger
       { 
            get { return OBulider.BuildInstance<SampleRuleTableManger>(); } 
        }
        /// <summary>
        ///  物料抽样项目管理
        /// </summary>
        public  MaterialSampleItemsManager MaterialSampleItemsManager
        {
            get { return OBulider.BuildInstance<MaterialSampleItemsManager>(); }
        }
        /// <summary>
        /// 抽样方法法规则管理
        /// </summary>
        public  SampleWayLawManger SampleItermLawManger
        {
            get { return OBulider.BuildInstance<SampleWayLawManger>(); }
        }

     
        public IPQCSampleItemsManager  IPQCSampleItemsManager
        {
            get { return OBulider.BuildInstance<IPQCSampleItemsManager>(); }
        }
    }
}
