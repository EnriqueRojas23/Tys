
using Domain.TYS.Pago;
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class DetalleRutaConfiguration : EntityTypeConfiguration<DetalleRuta>
    {
        public DetalleRutaConfiguration()
            : base()
        {
            ToTable("seguimiento.detalleruta");
            HasKey(p => p.iddetalleruta);
            //Property(p => p.iddetalleruta).IsRequired();
        }
    }
}
