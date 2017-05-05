using Lm.Eic.App.DbAccess.Bpm.Mapping;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
namespace Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRmaReportInitiateRepository : IRepository<ReportInitiateModel>
    {

    }
    public class RmaReportInitiateRepository : BpmRepositoryBase<ReportInitiateModel>, IRmaReportInitiateRepository
    {
    }


    /// <summary>
    /// 
    /// </summary>
    public interface IRmaBussesDescriptionRepository : IRepository<BussesDescriptionModel>
    {

    }
    public class RmaBussesDescriptionRepository : BpmRepositoryBase<BussesDescriptionModel>, IRmaBussesDescriptionRepository
    {
    }


    /// <summary>
    /// 
    /// </summary>
    public interface IRmaInspectionManageRepository : IRepository<InspectionManageModel>
    {

    }
    public class RmaInspectionManageRepository : BpmRepositoryBase<InspectionManageModel>, IRmaInspectionManageRepository
    {

    }


}