using Lm.Eic.App.DbAccess.Bpm.Mapping;
using Lm.Eic.App.DomainModel.Bpm.WorkFlow.GeneralForm;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Repository.WorkFlow.GeneralForm
{
    /// <summary>
    ///内部联络单持久化
    /// </summary>
    public interface IInternalContactFormRepository : IRepository<InternalContactFormModel> { }
    /// <summary>
    ///内部联络单持久化
    /// </summary>
    public class InternalContactFormRepository : BpmRepositoryBase<InternalContactFormModel>, IInternalContactFormRepository
    { }

}
