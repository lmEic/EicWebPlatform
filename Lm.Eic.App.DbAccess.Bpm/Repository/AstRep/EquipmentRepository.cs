using Lm.Eic.App.DbAccess.Bpm.Mapping;
using Lm.Eic.App.DomainModel.Bpm.Ast;
using Lm.Eic.Uti.Common.YleeDbHandler;

namespace Lm.Eic.App.DbAccess.Bpm.Repository.AstRep
{
    public interface IEquipmentRepository : IRepository<EquipmentModel> { }

    /// <summary>
    /// 设备基础信息 仓储层
    /// </summary>
    public class EquipmentRepository : BpmRepositoryBase<EquipmentModel>, IEquipmentRepository
    {
    }

    public interface IEquipmentCheckRepository : IRepository<EquipmentCheckRecordModel> { }

    /// <summary>
    ///设备校验 仓储层
    /// </summary>
    public class EquipmentCheckRepository : BpmRepositoryBase<EquipmentCheckRecordModel>, IEquipmentCheckRepository
    {
    }

    public interface IEquipmentMaintenanceRepositor : IRepository<EquipmentMaintenanceRecordModel> { }

    /// <summary>
    /// 设备保养  仓储层
    /// </summary>
    public class EquipmentMaintenanceRepository : BpmRepositoryBase<EquipmentMaintenanceRecordModel>, IEquipmentMaintenanceRepositor
    {
    }

    /// <summary>
    ///设备报废 仓储层
    /// </summary>
    public interface IEquipmentDiscardRepository : IRepository<EquipmentDiscardRecordModel> { }

    /// <summary>
    ///设备报废 仓储层
    /// </summary>
    public class EquipmentDiscardRepository : BpmRepositoryBase<EquipmentDiscardRecordModel>, IEquipmentDiscardRepository
    { }

    /// <summary>
    ///设备维修 仓储层
    /// </summary>
    public interface IEquipmentRepairedRecordRepository : IRepository<EquipmentRepairedRecordModel> { }

    /// <summary>
    ///设备维修 仓储层
    /// </summary>
    public class EquipmentRepairedRecordRepository : BpmRepositoryBase<EquipmentRepairedRecordModel>, IEquipmentRepairedRecordRepository
    { }
}