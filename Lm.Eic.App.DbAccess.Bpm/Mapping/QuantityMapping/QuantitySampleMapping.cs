﻿using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
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
    /// 抽样记录
    /// </summary>
    public class IQCSampleRecordMapping : EntityTypeConfiguration<IQCSampleRecordModel>
    {
       public  IQCSampleRecordMapping()
       {
           this.HasKey(t => t.Id_key );
           this.Property(t => t.Id_key ).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
           this.ToTable("QCMS_IQCSampleRecordTable");
       }
    }

    /// <summary>
    /// 抽样项次打印
    /// </summary>
    public class IQCSamplePrintItemsRecordMapping : EntityTypeConfiguration<IQCSampleItemRecordModel>
    {
        public IQCSamplePrintItemsRecordMapping()
        {
            this.HasKey(t => t.Id_key);
            this.Property(t => t.Id_key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("QCMS_IQCPrintSampleTable");
        }
    }

  
}