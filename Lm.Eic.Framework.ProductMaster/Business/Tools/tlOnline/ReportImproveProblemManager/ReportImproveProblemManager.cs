using Lm.Eic.Framework.ProductMaster.Model.Tools;
using Lm.Eic.Uti.Common.YleeExtension.FileOperation;
using Lm.Eic.Uti.Common.YleeExtension.Validation;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Business.Tools.tlOnline
{
  public  class ReportImproveProblemManager
    {

        public OpResult StoreReportImproveProblem(ReportImproveProblemModels model)
        {
            try
            {
                return ReportImproveProblemFactory.ReportImproveProblemCrud.Store(model,true);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public String AutoCreateCaseIdBuild()
        {
            int nowYear = DateTime.Now.Year;
            string maxCaseId = ReportImproveProblemFactory.ReportImproveProbleCaseId.CountNowYearMonthCasIdNumber(nowYear);
            if(maxCaseId==null)
            {
                maxCaseId = DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM")+"000";
            }          
            int count = Convert.ToInt16(maxCaseId.Substring(maxCaseId.Length - 3, 3)) + 1;
            return DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + count.ToString("000");
        }

        public List<ReportImproveProblemModels> GetReportImproveProbleDataBy(ReportImproveProblemModelsDto queryDto)
        {
            return ReportImproveProblemFactory.ReportImproveProblemCrud.FindBy(queryDto);
        }

       
    }
}
