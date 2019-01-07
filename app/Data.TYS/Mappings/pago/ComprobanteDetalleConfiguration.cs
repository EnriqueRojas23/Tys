
using Domain.TYS.Pago;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Pago
{
    public class ComprobanteProveedorDetalleConfiguration : EntityTypeConfiguration<ComprobanteProveedorDetalle>
    {
        public ComprobanteProveedorDetalleConfiguration()
            : base()
        {
            ToTable("pago.comprobantedetalle");
            HasKey(p => p.idcomprobantedetalle);
            //Property(p => p.idcomprobantedetalle).IsRequired();
        }
    }
}
