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
    #region  IQC 对应的SQL表
    /// <summary>
    /// 抽样记录
    /// </summary>
    public class IQCSampleRecordMapping : EntityTypeConfiguration<SampleIqcRecordModel>
    {
       public  IQCSampleRecordMapping()
       {
           this.HasKey(t => t.Id_key );
           this.Property(t => t.Id_key ).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
           this.ToTable("QCMS_IQCSampleRecordTable");
       }
    }

    
    /// <summary>
    /// IQC抽样项次记录
    /// </summary>
    public class IQCSampleItemsRecordMapping : EntityTypeConfiguration<SampleItemsIqcRecordModel>
    {
        public IQCSampleItemsRecordMapping()
        {
            this.HasKey(t => t.Id_key);
            this.Property(t => t.Id_key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("QCMS_IQCPrintSampleTable");
        }
    }


    /// <summary>
    /// 抽样不合格产品记录
    /// </summary>
    public class SampleProductNgRecordMapping : EntityTypeConfiguration<ProductNgRecordModel>
    {
        public SampleProductNgRecordMapping()
        {
            this.HasKey(t => t.Id_key);
            this.Property(t => t.Id_key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("QCMS_IQCSampleRecordNGTable");
        }
    }


    /// <summary>
    /// 物料抽测项次
    /// </summary>
    public class MaterialSampleItemMapping : EntityTypeConfiguration<MaterialSampleItemsModel>
    {
        public MaterialSampleItemMapping()
        {
            this.HasKey(t => t.Id_key );
            this.Property(t => t.Id_key ).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("QCMS_MaterialSampleSet");
        }
    }
    
    
    /// <summary>
    ///抽样数量规则
    /// </summary>
    public class SampleRuleTableMapping : EntityTypeConfiguration<SampleRuleTableModel>
    {
        public SampleRuleTableMapping()
        {
            this.HasKey(t => t.Id_key);
            this.Property(t => t.Id_key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("QCMS_SamplePlanTable");
        }
    }

     
     /// <summary>
     /// 放宽加严法则
     /// </summary>
    public class SampleWayLawMapping : EntityTypeConfiguration<SampleWayLawModel>
   {
       public SampleWayLawMapping ()
        {
            this.HasKey(e => e.Id_Key);
            this.Property(e => e.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("QCMS_SampleControlParamter");
        }                            
   }
#endregion

    public class  SampleItemsIpqcDataMapping:EntityTypeConfiguration <SampleItemsIpqcDataModel>
    {
        public SampleItemsIpqcDataMapping ()
        {
            this.HasKey(e => e.Id_key);
            this.Property(e => e.Id_key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("IPQC_SampleItemData");

        }
    }
}
