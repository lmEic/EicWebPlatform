using Lm.Eic.App.DbAccess.Bpm.Mapping;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep
{
    /// <summary>
    ///8D记录详表
    /// </summary>
    public interface IEightDReportDetailsRepository : IRepository<EightDReportDetailModel> { }
    /// <summary>
    ///8D记录详表
    /// </summary>
    public class EightDReportDetailsRepository : BpmRepositoryBase<EightDReportDetailModel>, IEightDReportDetailsRepository
    { }


    /// <summary>
    ///8D记录处理主表
    /// </summary>
    public interface IEightDReportMasterRepository : IRepository<EightReportMasterModel> { }
    /// <summary>
    ///8D记录处理主表
    /// </summary>
    public class EightDReportMasterRepository : BpmRepositoryBase<EightReportMasterModel>, IEightDReportMasterRepository
    { }

}
