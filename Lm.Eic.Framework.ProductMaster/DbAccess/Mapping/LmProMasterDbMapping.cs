using Lm.Eic.Framework.ProductMaster.Model;
using Lm.Eic.Framework.ProductMaster.Model.ITIL;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Lm.Eic.Framework.ProductMaster.DbAccess.Mapping
{
    public class ConfigDataDictionaryModelMapping : EntityTypeConfiguration<ConfigDataDictionaryModel>
    {
        public ConfigDataDictionaryModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.ToTable("Config_DataDictionary");
        }
    }

    /// <summary>
    ///ConfigFilePathModel
    /// </summary>
    public class ConfigFilePathModelMapping : EntityTypeConfiguration<ConfigFilePathModel>
    {
        public ConfigFilePathModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("Config_FilePathInfo");
        }
    }




    /// <summary>
    ///ItilDevelopModuleManageModel
    /// </summary>
    public class ItilDevelopModuleManageModelMapping : EntityTypeConfiguration<ItilDevelopModuleManageModel>
    {
        public ItilDevelopModuleManageModelMapping()
        {
            this.HasKey(t => t.Id_Key);
            this.Property(t => t.Id_Key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.ToTable("ITIL_DevelopModuleManage");
        }
    }
}