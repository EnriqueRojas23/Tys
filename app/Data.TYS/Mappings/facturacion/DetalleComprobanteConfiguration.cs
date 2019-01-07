
using Domain.TYS.Facturacion;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Facturacion
{
    public class DetalleComprobanteConfiguration : EntityTypeConfiguration<DetalleComprobante>
    {
        public DetalleComprobanteConfiguration()
            : base()
        {
            ToTable("facturacion.detallecomprobante");
            HasKey(p => p.iddetallecomprobante);
            //Property(p => p.iddetallecomprobante).IsRequired();
        }
    }
}
