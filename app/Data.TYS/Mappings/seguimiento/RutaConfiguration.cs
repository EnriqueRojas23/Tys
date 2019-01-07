
using Domain.TYS.Pago;
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class RutaConfiguration : EntityTypeConfiguration<Ruta>
    {
        public RutaConfiguration()
            : base()
        {
            ToTable("seguimiento.ruta");
            HasKey(p => p.idruta);
            //Property(p => p.idruta).IsRequired();
        }
    }
}
