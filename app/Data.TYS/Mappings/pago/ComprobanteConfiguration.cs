
using Domain.TYS.Pago;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Pago
{
    public class ComprobanteProveedorConfiguration : EntityTypeConfiguration<ComprobanteProveedor>
    {
        public ComprobanteProveedorConfiguration()
            : base()
        {
            ToTable("pago.comprobante");
            HasKey(p => p.idcomprobante);
            //Property(p => p.idcomprobante).IsRequired();
        }
    }
}
