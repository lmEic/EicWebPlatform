using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.DbAccess.Bpm.Mapping;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
namespace Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep
{
    public interface IInspectionModeConfigRepository : IRepository<InspectionModeConfigModel>
    {
       
    }
    public class InspectionModeConfigRepository : BpmRepositoryBase<InspectionModeConfigModel>, IInspectionModeConfigRepository
    {
     

        
    }

    #region IQC

    public interface IIqcInspectionItemConfigRepository : IRepository<InspectionIqCItemConfigModel>
    {
       
    }
    public class IqcInspectionItemConfigRepository : BpmRepositoryBase<InspectionIqCItemConfigModel>, IIqcInspectionItemConfigRepository
    {
     
    }

    public interface IIqcInspectionMasterRepository : IRepository<InspectionIqcMasterModel> { }
    public class IqcInspectionMasterRepository : BpmRepositoryBase<InspectionIqcMasterModel>, IIqcInspectionMasterRepository
    { }

    public interface IIqcInspectionDetailRepository : IRepository<InspectionIqcDetailModel> { }
    public class IqcInspectionDetailRepository : BpmRepositoryBase<InspectionIqcDetailModel>, IIqcInspectionDetailRepository
    { }
    #endregion
}
