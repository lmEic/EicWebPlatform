using Lm.Eic.App.DbAccess.Bpm.Mapping;
using Lm.Eic.App.DomainModel.Bpm.Dev;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Repository.DevRep
{
    public interface IDesignDevelopInputRepository : IRepository<DesignDevelopInputModel> { }

    /// <summary>
    /// 设备基础信息 仓储层
    /// </summary>
    public class DesignDevelopInputRepository : BpmRepositoryBase<DesignDevelopInputModel>, IDesignDevelopInputRepository
    {
    }
}
