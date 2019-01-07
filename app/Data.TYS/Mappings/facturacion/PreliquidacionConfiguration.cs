
using Domain.TYS.Facturacion;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Facturacion
{
    public class PreliquidacionConfiguration : EntityTypeConfiguration<Preliquidacion>
    {
        public PreliquidacionConfiguration()
            : base()
        {
            ToTable("facturacion.preliquidacion");
            HasKey(p => p.idpreliquidacion);
            //Property(p => p.idpreliquidacion).IsRequired();
        }
    }
}
