using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lm.Eic.App.DbAccess.Bpm.Mapping;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
namespace Lm.Eic.App.DbAccess.Bpm.Repository.QmsRep
{

    #region 配置文件
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
    #endregion

    #region IQC

    public interface IIqcInspectionItemConfigRepository : IRepository<InspectionIqcItemConfigModel>
    {

    }
    public class IqcInspectionItemConfigRepository : BpmRepositoryBase<InspectionIqcItemConfigModel>, IIqcInspectionItemConfigRepository
    {

    }



    public interface IIqcInspectionMasterRepository : IRepository<InspectionIqcMasterModel>
    {
        int UpAuditDetailData(string orderId, string materialId, string upAuditData);


    }
    public class IqcInspectionMasterRepository : BpmRepositoryBase<InspectionIqcMasterModel>, IIqcInspectionMasterRepository
    {
        public int UpAuditDetailData(string orderId, string materialId, string inspectionItemStatus)
        {
            string upDetailsql = string.Format("Update   Qms_IqcInspectionDetail   Set  InspectionItemStatus='{0}'  Where OrderId='{1}' and  MaterialId='{2}'", inspectionItemStatus, orderId, materialId);
            return DbHelper.Bpm.ExecuteNonQuery(upDetailsql);
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
    public interface IFqcInspectionDatailRepository : IRepository<InspectionFqcDetailModel> { }

    public class FqcInspectionDatailRepository : BpmRepositoryBase<InspectionFqcDetailModel>, IFqcInspectionDatailRepository
    { }


    public interface IFqcInspectionMasterRepository : IRepository<InspectionFqcMasterModel>
    {

        int UpAuditDetailData(string orderId, int orderIdNumber, string upAuditData);
    }

    public class FqcInspectionMasterRepository : BpmRepositoryBase<InspectionFqcMasterModel>, IFqcInspectionMasterRepository
    {
        public int UpAuditDetailData(string orderId, int orderIdNumber, string upAuditData)
        {
            string upDetailsql = string.Format("Update   Qms_FqcInspectionDetail   Set   InspectionItemStatus='{0}'  Where OrderId='{1}' and  OrderIdNumber='{2}'", upAuditData, orderId, orderIdNumber);
            return DbHelper.Bpm.ExecuteNonQuery(upDetailsql);
        }

    }

    #endregion




    public interface IBpmRepositoryMdoelReository<EntityModel> : IRepository<EntityModel>
      where EntityModel : class, new()
    { }
    public class BpmRepositoryMdoelReository<EntityModel> : BpmRepositoryBase<EntityModel>, IBpmRepositoryMdoelReository<EntityModel>
     where EntityModel : class, new()
    { }

}
