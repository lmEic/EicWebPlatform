using Lm.Eic.App.DbAccess.Bpm.Repository.PmsRep.DailyReport;
using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeObjectBuilder;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Pms.DailyReport
{

   internal class BorardCrudFactory
        {
             /// <summary>
             /// 日报录入
             /// </summary>
            public static DailyReportCrud DailyReportCrud
            { get { return OBulider.BuildInstance<DailyReportCrud>(); } }
             /// <summary>
            /// 日报模板
             /// </summary>
            public static DailyReportTemplateCrud DailyReportTemplateCrud
            { get { return OBulider.BuildInstance<DailyReportTemplateCrud>(); } }
            /// <summary>
            /// 工序
            /// </summary>
            public static ProductFlowCrud ProductFlowCrud
            { get { return OBulider.BuildInstance<ProductFlowCrud>(); } }
        }
    /// <summary>
    /// 日报录入CRUD
    /// </summary>
    public class DailyReportCrud : CrudBase<DailyReportModel, IDailyReportRepositoryRepository>
    {
        public DailyReportCrud() : base(new DailyReportRepositoryRepository(), "日报")
        {
        }

        protected override void AddCrudOpItems()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 日报模板CRUD
    /// </summary>
    public class DailyReportTemplateCrud : CrudBase<DailyReportTemplateModel, IDailyReportTemplateRepositoryRepository>
    {
        public DailyReportTemplateCrud() : base(new DailyReportTemplateRepositoryRepository(), "日报模板")
        {
        }

        protected override void AddCrudOpItems()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 工序CRUD
    /// </summary>
    public class ProductFlowCrud : CrudBase<ProductFlowModel, IProductFlowRepositoryRepository>
    {
        public ProductFlowCrud() : base(new ProductFlowRepositoryRepository(),"工序")
        {
        }

        protected override void AddCrudOpItems()
        {
            throw new NotImplementedException();
        }


        
    }
}
