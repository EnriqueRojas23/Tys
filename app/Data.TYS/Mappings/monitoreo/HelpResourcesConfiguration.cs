
using Domain.TYS.Monitoreo;
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Monitoreo
{
    public class HelpResourcesConfiguration : EntityTypeConfiguration<HelpResources>
    {
        public HelpResourcesConfiguration()
            : base()
        {
            ToTable("monitoreo.helpresources");
            HasKey(p => p.idhelp);
            //Property(p => p.idhelp).IsRequired();
        }
    }
}
