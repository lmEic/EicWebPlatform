using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.DomainModel.Bpm.Quanity;



namespace Lm.Eic.App.DbAccess.Bpm.Mapping.QuantityMapping
{
    /// <summary>
    /// 实体应射到Model上
    /// </summary>
    public class IQCSampleMapping : EntityTypeConfiguration<IQCSampleRecordModel>
    {
       public  IQCSampleMapping()
       {
           this.HasKey(t => t.Id_key );
           this.Property(t => t.Id_key ).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
           this.ToTable("IQCSampleRecordTable");
       }
    }



  
}
