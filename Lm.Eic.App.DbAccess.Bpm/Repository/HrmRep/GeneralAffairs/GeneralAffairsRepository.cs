using Lm.Eic.App.DbAccess.Bpm.Mapping.HrmMapping;
using Lm.Eic.App.DomainModel.Bpm.Hrm.GeneralAffairs;
using Lm.Eic.Uti.Common.YleeDbHandler;

namespace Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.GeneralAffairs
{
    /// <summary>
    /// 厂服管理持久层
    /// </summary>
    public interface IWorkClothesManageModelRepository : IRepository<WorkClothesManageModel> { }
    
    /// <summary>
    /// 厂服管理持久层
    /// </summary>
    public class WorkClothesManageModelRepository : HrmRepositoryBase<WorkClothesManageModel>, IWorkClothesManageModelRepository
    { }

}
