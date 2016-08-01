using Lm.Eic.App.DomainModel.Bpm.Ast;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping.AstMapping
{
    /// <summary>
    /// 设备实体映射
    /// </summary>
    public class EquipmentModelMapping : EntityTypeConfiguration<EquipmentModel>
    {
        public EquipmentModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Ast_Equipment");
        }
    }

    /// <summary>
    ///设备校验EquipmentCheckModel
    /// </summary>
    public class EquipmentCheckModelMapping : EntityTypeConfiguration<EquipmentCheckRecordModel>
    {
        public EquipmentCheckModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Ast_EquipmentCheckRecord");
        }
    }

    /// <summary>
    ///设备保养EquipmentMaintenanceModel
    /// </summary>
    public class EquipmentMaintenanceModelMapping : EntityTypeConfiguration<EquipmentMaintenanceRecordModel>
    {
        public EquipmentMaintenanceModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Ast_EquipmentMaintenanceRecord");
        }
    }

}