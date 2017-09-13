using Lm.Eic.App.DomainModel.Bpm.Hrm.WorkOverHours;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.DbAccess.Bpm.Mapping.HrmMapping
{
  public  class WorkOverHoursMapping: EntityTypeConfiguration<WorkOverHoursMangeModels>
    {
        /// <summary>
        /// 加班管理Mapping
        /// </summary>
        public WorkOverHoursMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Attendance_WorkOverHourDatas");
        }

    }
}
