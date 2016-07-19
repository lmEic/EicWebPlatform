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
namespace Lm.Eic.App.Business.Bmp.Quantity.SampleItermLaw
{
    /// <summary>
    /// 取样放宽加严规则
    /// </summary>
    public  class SampleItermLawManger
    {
        ISampleContorlLimitReosity irep = null;
        public SampleItermLawManger ()
        { irep = new SampleContorlLimitReosity(); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="classification">IQC/FQC/IPQC</param>
        /// <returns></returns>
        public SampleContorlLimitModel GeLimitParameterBy(string classification)
        {
            return irep.Entities.Where(e => e.Classification == classification).ToList().FirstOrDefault();
        }


    }
    /// <summary>
    ///  取样 数量/拒受数量 /接受数量 规则
    /// </summary>
    public  class SamplePlanTableManger
    {
        ISamplePlanTableReposity  irep =null ;
        public  SamplePlanTableManger()
        {
            irep = new SamplePlanTableReposity();
        }
        /// <summary>
        /// 获取取样数量/拒受数量 /接受数量
        /// </summary>
        /// <param name="checkWay">放宽/加严/正常</param>
        /// <param name="checkLevel">水平</param>
        /// <param name="grade">档次</param>
        /// <param name="number">来料数量</param>
        /// <returns></returns>
        public SamplePlanTableModel getSampleNumber(string  checkWay ,string  checkLevel,string grade,Int64 number)
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
                    Double MaxNumber = Convert.ToDouble(max);
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
                    Double MinNumber = Convert.ToDouble(min);
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
   /// 物料抽样信息表
   /// </summary>
    public class MaterialSampleItemManager
    {
        IMaterialSampleSetReposity irep = null;
        public MaterialSampleItemManager()
        {
            irep = new MaterialSampleSetReposity();
        }
        /// <summary>
        ///   由料号得到抽样项次
        /// </summary>
        /// <param name="sampleMaterial">料号</param>
        /// <returns></returns>
        public List<MaterialSampleSetModel> GetMaterilalSampleItemBy(string sampleMaterial)
        {
            return irep.Entities.Where(e => e.SampleMaterial == sampleMaterial).ToList();
        }

    }
}

