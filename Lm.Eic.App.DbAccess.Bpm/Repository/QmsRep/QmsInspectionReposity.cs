using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.DbAccess.Bpm.Mapping;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.App.DomainModel.Bpm.Qms;
namespace Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep
{
    public interface IInspectionModeConfigRepository : IRepository<InspectionModeConfigModel> { }
    public class InspectionModeConfigRepository : BpmRepositoryBase<InspectionModeConfigModel>, IInspectionModeConfigRepository
    { }

    #region IQC

    public interface IIqcInspectionItemConfigRepository : IRepository<IqcInspectionItemConfigModel> { }
    public class IqcInspectionItemConfigRepository : BpmRepositoryBase<IqcInspectionItemConfigModel>, IIqcInspectionItemConfigRepository
    { }

    public interface IIqcInspectionMasterRepository : IRepository<IqcInspectionMasterModel> { }
    public class IqcInspectionMasterRepository : BpmRepositoryBase<IqcInspectionMasterModel>, IIqcInspectionMasterRepository
    { }

    public interface IIqcInspectionDetailRepository : IRepository<IqcInspectionDetailModel> { }
    public class IqcInspectionDetailRepository : BpmRepositoryBase<IqcInspectionDetailModel>, IIqcInspectionDetailRepository
    { }
    #endregion
}
