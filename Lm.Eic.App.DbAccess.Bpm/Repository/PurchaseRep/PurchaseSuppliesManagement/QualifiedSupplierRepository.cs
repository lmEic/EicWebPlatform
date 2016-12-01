using Lm.Eic.App.DbAccess.Bpm.Mapping;
using Lm.Eic.App.DomainModel.Bpm.Purchase;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Lm.Eic.App.DbAccess.Bpm.Repository.PurchaseRep.PurchaseSuppliesManagement
{
  /// <summary>
  ///合格供应商
  /// </summary>
    public interface IEligibleSupplierRepository : IRepository<EligibleSuppliersModel> { }
  /// <summary>
  ///合格供应商
  /// </summary>
    public class EligibleSupplierRepository : BpmRepositoryBase<EligibleSuppliersModel>, IEligibleSupplierRepository
    { }
    /// <summary>
   /// 供应商合格文件
    /// </summary>
  public interface ISupplierQualifiedCertificateRepository : IRepository<SuppliersQualifiedCertificateModel> { }
  public class SupplierQualifiedCertifcateRepository : BpmRepositoryBase<SuppliersQualifiedCertificateModel>, ISupplierQualifiedCertificateRepository
    { }



  /// <summary>
  /// 供应商信息
  /// </summary>
  public interface ISupplierInfoRepository : IRepository<SuppliersInfoModel> { }
  public class SupplierInfoRepository : BpmRepositoryBase<SuppliersInfoModel>, ISupplierInfoRepository 
  { }


/// <summary>
  /// 供应商考核表
/// </summary>
  public interface ISupplierSeasonAuditRepository : IRepository<SupplieSeasonAuditModel> { }

  public class SupplierSeasonAuditRepository:BpmRepositoryBase<SupplieSeasonAuditModel>,ISupplierSeasonAuditRepository
    { }
}
