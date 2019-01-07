
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class RutaEtapaConfiguration : EntityTypeConfiguration<RutaEtapa>
    {
        public RutaEtapaConfiguration()
            : base()
        {
            ToTable("seguimiento.rutaetapa");
            HasKey(p => p.iddetalleruta);
           // Property(p => p.iddetalleruta).IsRequired();
        }
    }
}
