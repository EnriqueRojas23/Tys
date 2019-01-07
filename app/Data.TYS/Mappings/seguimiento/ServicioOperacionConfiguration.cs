
using Domain.TYS.Pago;
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class ServicioOperacionConfiguration : EntityTypeConfiguration<ServicioOperacion>
    {
        public ServicioOperacionConfiguration()
            : base()
        {
            ToTable("seguimiento.serviciooperacion");
            HasKey(p => p.idserviciooperacion);
            //Property(p => p.idserviciooperacion).IsRequired();
        }
    }
}
