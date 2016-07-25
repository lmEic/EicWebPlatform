using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.DbAccess.Bpm.Repository.QuantityRep;
using Lm.Eic.App.Erp.Bussiness.QuantityManage;
using Lm.Eic.App.Erp.Domain.QuantityModel;
using Lm.Eic.Uti.Common.YleeOOMapper;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.App.Business.Bmp.Quantity;
namespace Lm.Eic.App.Business.Bmp.Quantity.SampleManger.SampleItermRulesManger
{
    /// <summary>
    /// 取样放宽加严规则
    /// </summary>
    public  class SampleWayLawManger
    {
        ISampleWayLawReosity irep = null;
        public SampleWayLawManger ()
        { irep = new SampleWayLawReosity(); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="classification">IQC/FQC/IPQC</param>
        /// <returns></returns>
        public SampleWayLawModel GeLimitParameterBy(string classification)
        {
            return irep.Entities.Where(e => e.Classification == classification).ToList().FirstOrDefault();
        }
         /// <summary>
         ///  得到放宽加严重规划
         /// </summary>
         /// <param name="sampleMaterial">抽检的料号</param>
         /// <param name="theclass">类别</param>
         /// <returns></returns>
        public string GetCheckWayBy(string sampleMaterial, string theclass)
        {

            Dictionary<string, string> Paramter = GetAllParamterDictionaryBy(theclass);
            string JudgeWay = Paramter["JudgeWay"].Trim();
            //如果没有设置限制
            if (JudgeWay == "False") return "正常";


            int AB = Paramter["AB"].ToInt ();
            int AC = Paramter["AC"].ToInt ();
            int BA = Paramter["BA"].ToInt ();
            int CA = Paramter["CA"].ToInt ();
            int ABI = Paramter["ABI"].ToInt ();
            int ACI = Paramter["ACI"].ToInt ();
            int BAI = Paramter["BAI"].ToInt ();
            int CAI = Paramter["CAI"].ToInt ();
            string OldCheckWay = string.Empty ;

            var SampleRecord = QuantitySampleService.SampleRecordManager.GetIQCSampleRecordModelsBy(sampleMaterial).OrderByDescending (e=>e.Id_key);
            var chekWay = from r in SampleRecord.Take(1)
                          select r.CheckWay;
            foreach (var r in chekWay)
            {
                OldCheckWay = (r != null ? r.ToString() : string .Empty );
            }

            if (OldCheckWay == string .Empty ) { return "正常"; }
            if (OldCheckWay == "正常")
            {
                int A = (AB > AC) ? AB : AC; // 得到最大的检验批次
                int B = (AB <= AC) ? AB : AC;// 得到最小的检验批次
                int C = (ABI > ACI) ? ACI : ABI; // 按理说ABI必须小于ACI
                int D = (ABI <= ACI) ? ACI : ABI; // 取最大值
                var mm = SampleRecord.Take(A);
                if (mm.Count() < B | mm == null) return "正常";//实得到实体数小于最小的抽样批次
                //下面是 实得到实体数大于等于 最小的缺抽样批次
                var n = from r in mm
                        where r.SampleResult == "FAIL"
                        orderby r.FinishDate descending
                        select r;
                if (n.Count() <= C | n == null) return "放宽";//实得到FAIL实体数小于最小的
                //下面是 实体数FAIL数 大于等于 最小的
                if (n.Count() >= D) return "加严";  // 排除 FAIL数 大于等于 最大数时
                var mmm = mm.Take(B);
                var nn = from r in mmm
                         where r.SampleResult == "FAIL"
                         orderby r.FinishDate descending
                         select r;
                if (nn.Count() <= C | n == null) return "放宽";
                else return "正常";
            }
            if (OldCheckWay == "放宽")
            {
                var mm = SampleRecord.Take(BA);
                var n = from r in mm
                        where r.SampleResult == "FAIL"
                        orderby r.FinishDate descending
                        select r;
                return (n.Count() <= BAI | mm == null) ? "放宽" : "正常";
            }
            if (OldCheckWay == "加严")
            {
                var mm = SampleRecord.Take(CA);
                var n = from r in mm
                        where r.SampleResult == "FAIL"
                        orderby r.FinishDate descending
                        select r;
                return (n.Count() > CAI | mm == null) ? "加严" : "正常";
            }
            return "正常";

        }

         /// <summary>
         /// 
         /// </summary>
         /// <param name="theClass"> 类别IQC/IPQC/FQC</param>
         /// <returns></returns>
        private Dictionary<string, string> GetAllParamterDictionaryBy(string theClass)
        {
            Dictionary<string, string> AllParamter = new Dictionary<string, string>();
           var  mdl = irep.Entities.Where(e => e.Classification == theClass).ToList ().FirstOrDefault ();
            if (mdl != null)
            {
                 AllParamter.Add("JudgeWay", mdl.JudgeWay.Trim());
                 AllParamter.Add("AB", mdl.AB.Trim());
                 AllParamter.Add("AC", mdl.AC.Trim());
                 AllParamter.Add("BA", mdl.BA.Trim());
                 AllParamter.Add("CA", mdl.CA.Trim());
                 AllParamter.Add("ABI", mdl.ABI.Trim());
                 AllParamter.Add("ACI", mdl.ACI.Trim());
                 AllParamter.Add("BAI", mdl.BAI.Trim());
                 AllParamter.Add("CAI", mdl.CAI.Trim());
            }
            return AllParamter;
        }

    }
    /// <summary>
    ///  物料数量抽样规则     （抽样数量/拒受数量 /接受数量） 
    /// </summary>
    public  class SampleRuleTableManger
    {
        ISampleRuleTableReposity  irep =null ;
        public  SampleRuleTableManger()
        {
            irep = new SampleRuleTableReposity();
        }
        /// <summary>
        /// 获取取样数量/拒受数量 /接受数量
        /// </summary>
        /// <param name="checkWay">放宽/加严/正常</param>
        /// <param name="checkLevel">水平</param>
        /// <param name="grade">档次</param>
        /// <param name="number">来料数量</param>
        /// <returns></returns>
        public SampleRuleTableModel getSampleNumberBy(string  checkWay ,string  checkLevel,string grade,Int64 number)
        {
            List<string> Maxs = new List<string>();
            List<string> Mins = new List<string>();
            string MaxNumber = string .Empty ;
            string MinNumber = string .Empty ;
            var Models = irep.Entities.Where(e => e.CheckWay == checkWay & e.CheckLevel == checkLevel & e.Grade == grade).ToList();
            Models.ForEach(e =>
            {
                Maxs.Add(e.EndNumber);
                Mins.Add(e.StartNumber);
            });
            if (Maxs.Count > 0)
            { MaxNumber = GetMaxNumber(Maxs, number); }
            if (Mins.Count > 0)
            { MinNumber = GetMinNumber(Mins, number); }
            return  Models.Find(e => e.StartNumber == MinNumber && e.EndNumber == MaxNumber);
      
        }

        private string GetMaxNumber(List<string> maxNumbers, Int64 number)
        {
            List<Double> IntMaxNumbers = new List<Double>();
            foreach (string max in maxNumbers)
            {
                if (max != "*")
                {
                    Double MaxNumber = max.ToDouble();
                    if (MaxNumber >= number)
                    {
                        IntMaxNumbers.Add(MaxNumber);
                    }
                }
               
            }
            if (IntMaxNumbers.Count > 0)
            { return IntMaxNumbers.Min().ToString(); }
            else  return "*";
        }
        private string GetMinNumber(List<string> minNumbers, Int64 mumber)
        {
            List<Double> IntMinNumbers = new List<Double>();
            foreach (string min in minNumbers)
            {
                if (min != "")
                {
                    Double MinNumber = min.ToDouble ();
                    if (MinNumber <= mumber)
                    {
                        IntMinNumbers.Add(MinNumber);
                    }
                }
                else return string.Empty;
            }
            return IntMinNumbers.Max().ToString();
        }
      
    }

   /// <summary>
   /// 物料抽样项目
   /// </summary>
    public class MaterialSampleItemManager
    {
        IMaterialSampleItemReposity irep = null;
        public MaterialSampleItemManager()
        {
            irep = new MaterialSampleItemReposity();
        }
        /// <summary>
        ///   由料号得到抽样项次
        /// </summary>
        /// <param name="sampleMaterial">料号</param>
        /// <returns></returns>
        public List<MaterialSampleItemModel> GetMaterilalSampleItemBy(string sampleMaterial)
        {
            return irep.Entities.Where(e => e.SampleMaterial == sampleMaterial || e.SampleMaterial == "ToAllMaterial").OrderByDescending(e => e.PriorityLevel).ToList();
        }

    }
}

