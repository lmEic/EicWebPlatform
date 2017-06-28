using Lm.Eic.App.DbAccess.Bpm.Mapping;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using System.Collections.Generic;
using System;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRmaReportInitiateRepository : IRepository<RmaReportInitiateModel>
    {
      List<RmaReportInitiateModel>  GetInitiateDatasBy(int formRmaYear, int formRmaMonth, int toRmaYear, int toRmaMonth);
    }
    public class RmaReportInitiateRepository : BpmRepositoryBase<RmaReportInitiateModel>, IRmaReportInitiateRepository
    {
        public List<RmaReportInitiateModel> GetInitiateDatasBy(int formRmaYear, int formRmaMonth, int toRmaYear, int toRmaMonth)
        {
            StringBuilder sqlText = new StringBuilder();
            sqlText.Append("SELECT   RmaId, CustomerShortName, ProductName, RmaIdStatus, RmaYear, RmaMonth, OpPerson, OpDate, OpTime,  OpSign, Id_Key   FROM   Qms_RmaReportInitiate ");
            sqlText.AppendFormat(" WHERE   (RmaYear >= '{0}')", formRmaYear);
            sqlText.AppendFormat(" AND (RmaYear <= '{0}') ", toRmaYear);
            sqlText.AppendFormat(" AND (RmaMonth >= '{0}')", formRmaMonth);
            sqlText.AppendFormat(" AND (RmaMonth <= '{0}')", toRmaMonth);
            return DbHelper.Bpm.LoadEntities<RmaReportInitiateModel>(sqlText.ToString());
        }
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