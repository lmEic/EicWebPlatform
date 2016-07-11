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



    /// <summary>
    /// 
    /// </summary>
    public interface IIQCSampleItemRecordReposity : IRepository<IQCSampleItemRecordModel> { }
    /// <summary>
    ///  IQC物料抽项目存储 
    /// </summary>
    public class IQCSampleItemRecordReposity : QuantityRepositoryBase<IQCSampleItemRecordModel>, IIQCSampleItemRecordReposity
    { }

    /// <summary>
    /// 
    /// </summary>
    public interface IProductNgRecordReposity : IRepository<ProductNgRecordModel> { }
    /// <summary>
    ///  抽样不合格模块存储 
    /// </summary>
    public class ProductNgRecordReposity : QuantityRepositoryBase<ProductNgRecordModel>, IProductNgRecordReposity
    { }

    /// <summary>
    /// 物料抽检项次
    /// </summary>
    public interface IMaterialSampleSetReposity : IRepository<MaterialSampleSet> { }
    /// <summary>
    ///  物抽测项目模块存储 
    /// </summary>
    public class MaterialSampleSetReposity : QuantityRepositoryBase<MaterialSampleSet>, IMaterialSampleSetReposity
    { }
}
