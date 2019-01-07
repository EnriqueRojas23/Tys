
using Domain.TYS.Pago;
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class ZonaConfiguration : EntityTypeConfiguration<Zona>
    {
        public ZonaConfiguration()
            : base()
        {
            ToTable("seguimiento.zona");
            HasKey(p => p.idzona);
            //Property(p => p.idzona).IsRequired();
        }
    }
}
