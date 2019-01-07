
using Domain.TYS.Facturacion;
using System.Data.Entity.ModelConfiguration;

namespace Data.TYS.Mappings.Facturacion
{
    public class ComprobanteConfiguration : EntityTypeConfiguration<Comprobante>
    {
        public ComprobanteConfiguration()
            : base()
        {
            ToTable("facturacion.comprobante");
            HasKey(p => p.idcomprobantepago);
            //Property(p => p.idcomprobantepago).IsRequired();
        }
    }
}
