using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.DbAccess.Bpm.Mapping.QuantityMapping;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.Uti.Common.YleeDbHandler;
using Lm.Eic.Uti.Common.YleeExcelHanlder;
namespace Lm.Eic.App.DbAccess.Bpm.Repository.QuantityRep
{
    public interface ISampleIqcRecordReposity : IRepository<SampleIqcRecordModel> { }
    /// <summary>
    ///  IQC物料抽样模块存储 
    /// </summary>
    public class SampleIqcRecordReposity : QuantityRepositoryBase<SampleIqcRecordModel>, ISampleIqcRecordReposity
    { }


    public interface ISampleItemsIpqcRecordReposity : IRepository<SampleItemsIqcRecordModel> { }
    /// <summary>
    ///  IQC物料抽项目存储 
    /// </summary>
    public class SampleItemsIpqcRecordReposity : QuantityRepositoryBase<SampleItemsIqcRecordModel>, ISampleItemsIpqcRecordReposity
    { }


    public interface IProductNgRecordReposity : IRepository<ProductNgRecordModel> { }
    /// <summary>
    ///  抽样不合格模块存储 
    /// </summary>
    public class ProductNgRecordReposity : QuantityRepositoryBase<ProductNgRecordModel>, IProductNgRecordReposity
    { }


    public interface IMaterialSampleItemsReposity : IRepository<MaterialSampleItemsModel> { }
    /// <summary>
    ///  物抽测项目模块存储 
    /// </summary>
    public class MaterialSampleItemsReposity : QuantityRepositoryBase<MaterialSampleItemsModel>, IMaterialSampleItemsReposity
    { }

    public interface ISampleRuleTableReposity : IRepository<SampleRuleTableModel> { }
    /// <summary>
    /// 物料抽样规则模块存储
    /// </summary>
    public class  SampleRuleTableReposity:QuantityRepositoryBase <SampleRuleTableModel>,ISampleRuleTableReposity
    { }


    public interface ISampleWayLawReosity : IRepository<SampleWayLawModel> { };
     /// <summary>
     /// 抽样放宽加严控制存储
     /// </summary>
    public class SampleWayLawReosity : QuantityRepositoryBase<SampleWayLawModel>, ISampleWayLawReosity
    { }
    /// <summary>
    /// ipqc 抽测
    /// </summary>
    public interface ISampleItemsIpqcDataReosity:IRepository <SampleItemsIpqcDataModel>{};
    public class  SampleItemsIpqcDataReosity:  QuantityRepositoryBase<SampleItemsIpqcDataModel>,ISampleItemsIpqcDataReosity
    { }
                                        
}
