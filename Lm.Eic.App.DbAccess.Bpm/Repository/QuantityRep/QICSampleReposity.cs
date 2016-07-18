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
    public interface IIQCSampleRecordReposity : IRepository<IQCSampleRecordModel> { }
    /// <summary>
    ///  IQC物料抽样模块存储 
    /// </summary>
    public class IQCSampleRecordReposity : QuantityRepositoryBase<IQCSampleRecordModel>, IIQCSampleRecordReposity
    { }

    public interface IIQCSampleItemRecordReposity : IRepository<IQCSampleItemRecordModel> { }
    /// <summary>
    ///  IQC物料抽项目存储 
    /// </summary>
    public class IQCSampleItemRecordReposity : QuantityRepositoryBase<IQCSampleItemRecordModel>, IIQCSampleItemRecordReposity
    { }

    /// <summary>
    /// 抽样不合格记录存储
    /// </summary>
    public interface IProductNgRecordReposity : IRepository<ProductNgRecordModel> { }
    /// <summary>
    ///  抽样不合格模块存储 
    /// </summary>
    public class ProductNgRecordReposity : QuantityRepositoryBase<ProductNgRecordModel>, IProductNgRecordReposity
    { }


    public interface IMaterialSampleSetReposity : IRepository<MaterialSampleSetModel> { }
    /// <summary>
    ///  物抽测项目模块存储 
    /// </summary>
    public class MaterialSampleSetReposity : QuantityRepositoryBase<MaterialSampleSetModel>, IMaterialSampleSetReposity
    { }

    public interface ISamplePlanTableReposity : IRepository<SamplePlanTableModel> { }
    public class  SamplePlanTableReposity:QuantityRepositoryBase <SamplePlanTableModel>,ISamplePlanTableReposity
    { }

    public interface ISampleContorlLimitReosity : IRepository<SampleContorlLimitModel> { };
     /// <summary>
     /// 抽样放宽加严控制存储
     /// </summary>
    public class  SampleContorlLimitReosity:QuantityRepositoryBase <SampleContorlLimitModel>,ISampleContorlLimitReosity
    { }
}
