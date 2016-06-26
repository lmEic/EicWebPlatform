using Lm.Eic.Framework.ProductMaster.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

}
