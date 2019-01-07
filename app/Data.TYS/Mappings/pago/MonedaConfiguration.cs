
using Domain.TYS.Pago;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Pago
{
    public class MonedaConfiguration : EntityTypeConfiguration<Moneda>
    {
        public MonedaConfiguration()
            : base()
        {
            ToTable("pago.moneda");
            HasKey(p => p.idmoneda);
            //Property(p => p.idmoneda).IsRequired();
        }
    }
}
