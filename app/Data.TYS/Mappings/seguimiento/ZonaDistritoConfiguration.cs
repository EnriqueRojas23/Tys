
using Domain.TYS.Pago;
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class ZonaDistritoConfiguration : EntityTypeConfiguration<ZonaDistrito>
    {
        public ZonaDistritoConfiguration()
            : base()
        {
            ToTable("seguimiento.zonadistrito");
            HasKey(p => p.idzonadistrito);
            //Property(p => p.idzonadistrito).IsRequired();
        }
    }
}
