
using Domain.TYS.Pago;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Pago
{
    public class TipoComprobanteConfiguration : EntityTypeConfiguration<TipoComprobante>
    {
        public TipoComprobanteConfiguration()
            : base()
        {
            ToTable("pago.tipocomprobante");
            HasKey(p => p.idtipocomprobante);
            //Property(p => p.idtipocomprobante).IsRequired();
        }
    }
}
