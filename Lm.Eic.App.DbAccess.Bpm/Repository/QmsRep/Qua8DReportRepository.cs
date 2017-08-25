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
    public interface IQua8DReportDetailsRepository : IRepository<Qua8DReportDetailModel> { }
    /// <summary>
    ///8D记录详表
    /// </summary>
    public class Qua8DReportDetailsRepository : BpmRepositoryBase<Qua8DReportDetailModel>, IQua8DReportDetailsRepository
    { }
    /// <summary>
    ///8D记录处理主表
    /// </summary>
    public interface IQua8DReportMasterRepository : IRepository<Qua8DReportMasterModel>
    {
        List<Qua8DReportMasterModel> getReportMasterDatas(string searchFrom, string searchTo);
    }
    /// <summary>
    ///8D记录处理主表
    /// </summary>
    public class Qua8DReportMasterRepository : BpmRepositoryBase<Qua8DReportMasterModel>, IQua8DReportMasterRepository
    {
        public List<Qua8DReportMasterModel> getReportMasterDatas(string searchFrom, string searchTo)
        {
            StringBuilder sqlText = new StringBuilder();
            sqlText.Append("SELECT  *  FROM    Qms_8DReportMaster");
            sqlText.AppendFormat(" WHERE   (YearMonth >= '{0}')", searchFrom);
            sqlText.AppendFormat(" AND (YearMonth <= '{0}') ", searchTo);
            return DbHelper.Bpm.LoadEntities<Qua8DReportMasterModel>(sqlText.ToString());
        }
    }
}
