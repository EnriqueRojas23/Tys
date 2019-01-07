
using Domain.TYS.Monitoreo;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Monitoreo
{
    public class IncidenteConfiguration : EntityTypeConfiguration<Incidencia>
    {
        public IncidenteConfiguration()
            : base()
        {
            ToTable("monitoreo.incidencia");
            HasKey(p => p.idincidencia);
            //Property(p => p.idincidencia).IsRequired();
        }
    }
}
