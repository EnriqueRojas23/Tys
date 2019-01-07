
using Domain.TYS.Pago;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Pago
{
    public class TipoTransporteConfiguration : EntityTypeConfiguration<TipoTransporte>
    {
        public TipoTransporteConfiguration()
            : base()
        {
            ToTable("pago.tipotransporte");
            HasKey(p => p.idtipotransporte);
            //Property(p => p.idtipotransporte).IsRequired();
        }
    }
}
