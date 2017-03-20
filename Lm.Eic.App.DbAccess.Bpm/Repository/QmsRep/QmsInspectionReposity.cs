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

    public interface IInspectionModeSwitchConfigRepository : IRepository<InspectionModeSwitchConfigModel>
    {

    }
    public class InspectionModeSwitchConfigRepository : BpmRepositoryBase<InspectionModeSwitchConfigModel>, IInspectionModeSwitchConfigRepository
    {
    }




    #region IQC

    public interface IIqcInspectionItemConfigRepository : IRepository<InspectionIqCItemConfigModel>
    {
       
    }
    public class IqcInspectionItemConfigRepository : BpmRepositoryBase<InspectionIqCItemConfigModel>, IIqcInspectionItemConfigRepository
    {
     
    }

    public interface IIqcInspectionMasterRepository : IRepository<InspectionIqcMasterModel>
    {
        int UpAuditMaterData(string orderId, string materialId,string upAuditData);
        int UpAuditDetailData(string orderId, string materialId, string upAuditData);


    }
    public class IqcInspectionMasterRepository : BpmRepositoryBase<InspectionIqcMasterModel>, IIqcInspectionMasterRepository
    {
        public int UpAuditDetailData(string orderId, string materialId, string inspectionItemStatus)
        {
            string upDetailsql = string.Format("Update   Qms_IqcInspectionDetail   Set  InspectionItemStatus='{0}'  Where OrderId='{1}' and  MaterialId='{2}'", inspectionItemStatus, orderId, materialId);
            return DbHelper.Bpm.ExecuteNonQuery(upDetailsql);
        }

        public int  UpAuditMaterData(string orderId, string materialId,string upAduitData)
        {
            string upMaterSql = string.Format("Update   Qms_IqcInspectionMaster   Set InspectionStatus='{0}'  Where OrderId='{1}' and  MaterialId='{2}'", upAduitData, orderId, materialId);
            return DbHelper.Bpm.ExecuteNonQuery(upMaterSql);
        }
    }

    public interface IIqcInspectionDetailRepository : IRepository<InspectionIqcDetailModel> { }
    public class IqcInspectionDetailRepository : BpmRepositoryBase<InspectionIqcDetailModel>, IIqcInspectionDetailRepository
    { }
    #endregion

    #region  FQC

    public interface IFqcInspectionItemConfigRepository : IRepository<InspectionFqcItemConfigModel> { }

    public class FqcInspectionItemConfigRepository : BpmRepositoryBase<InspectionFqcItemConfigModel>, IFqcInspectionItemConfigRepository
    {

    }
    #endregion



}
