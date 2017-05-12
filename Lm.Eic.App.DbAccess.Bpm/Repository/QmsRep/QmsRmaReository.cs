using Lm.Eic.App.DbAccess.Bpm.Mapping;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
namespace Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRmaReportInitiateRepository : IRepository<RmaReportInitiateModel>
    {

    }
    public class RmaReportInitiateRepository : BpmRepositoryBase<RmaReportInitiateModel>, IRmaReportInitiateRepository
    {
    }


    /// <summary>
    /// 
    /// </summary>
    public interface IRmaBussesDescriptionRepository : IRepository<RmaBusinessDescriptionModel>
    {

    }
    public class RmaBussesDescriptionRepository : BpmRepositoryBase<RmaBusinessDescriptionModel>, IRmaBussesDescriptionRepository
    {
    }


    /// <summary>
    /// 
    /// </summary>
    public interface IRmaInspectionManageRepository : IRepository<RmaInspectionManageModel>
    {

    }
    public class RmaInspectionManageRepository : BpmRepositoryBase<RmaInspectionManageModel>, IRmaInspectionManageRepository
    {

    }


}