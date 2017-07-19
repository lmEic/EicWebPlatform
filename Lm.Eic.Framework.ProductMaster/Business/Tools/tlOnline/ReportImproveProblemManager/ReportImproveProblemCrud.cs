using Lm.Eic.Framework.ProductMaster.DbAccess.Repository;
using Lm.Eic.Framework.ProductMaster.Model.Tools;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeExtension.Validation;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.Framework.ProductMaster.Business.Tools.tlOnline
{
    internal class ReportImproveProblemFactory
    {
        internal static ReportImproveProblemCrud ReportImproveProblemCrud
        {
            get { return OBulider.BuildInstance<ReportImproveProblemCrud>(); }
        }

        internal static ReportImproveProblemCrud ReportImproveProbleCaseId
        {
            get { return OBulider.BuildInstance<ReportImproveProblemCrud>(); }
        }
    }

   internal class ReportImproveProblemCrud:CrudBase<ReportImproveProblemModels, IReportImproveProblemRepository>
    {
        #region Crud

      
        public ReportImproveProblemCrud() : base(new ReportImproveProblemRepository(), "上报问题改善") { }
        protected override void AddCrudOpItems()
        {
            this.AddOpItem(OpMode.Add,ReportImproveProblemAdd);
            this.AddOpItem(OpMode.Edit, ReportImproveProblemEdit);
            this.AddOpItem(OpMode.Delete, ReportImproveProblemDelete);

        }

        private OpResult ReportImproveProblemDelete(ReportImproveProblemModels model)
        {
         
            return irep.Update(e => e.Id_Key == model.Id_Key, s => new ReportImproveProblemModels { Id_Key = model.Id_Key }).ToOpResult_Delete(OpContext);
        }

        private OpResult ReportImproveProblemEdit(ReportImproveProblemModels model)
        {
            return irep.Update(k => k.Id_Key == model.Id_Key, model).ToOpResult_Eidt(OpContext);
        }

        private OpResult ReportImproveProblemAdd(ReportImproveProblemModels model)
        {
            return irep.Insert(model).ToOpResult_Add(OpContext);
        }
        #endregion

        #region Find
         internal string CountNowYearMonthCasIdNumber(int nowYear)
        {
            return irep.Entities.Where(e => e.CaseIdYear == nowYear).Max(f => f.CaseId);
        }

        internal List<ReportImproveProblemModels>FindBy(ReportImproveProblemModelsDto queryDto)
        {
            if (queryDto == null) return new List<ReportImproveProblemModels>();
            try
            {
                switch (queryDto.SearchMode)
                {
                    case 1:
                        return irep.Entities.Where(m => m.ProblemSolve == queryDto.ProblemSolve).ToList();
                    default:
                        return new List<ReportImproveProblemModels>();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }


      
        #endregion

    }
}
