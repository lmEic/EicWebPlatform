
using Lm.Eic.App.DomainModel.Mes.Mes_Nbosa;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Mes.Mes_NBOSA.Repository.HousingSNControllerRep
{
    /// <summary>
    ///
    /// </summary>
    public interface IHousingSN_ControllerRepository : IRepository<HousingSN_ControllerModel>
    {
    }
    /// <summary>
    ///
    /// </summary>
    public class HousingSN_ControllerRepository : OpticalMes_NBOSA_RepositoryBase<HousingSN_ControllerModel>, IHousingSN_ControllerRepository
    {

    }
}
