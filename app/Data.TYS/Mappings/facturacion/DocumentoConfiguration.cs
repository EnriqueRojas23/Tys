
using Domain.TYS.Facturacion;
using System.Data.Entity.ModelConfiguration;

namespace Data.TYS.Mappings.Facturacion
{
    public class DocumentoConfiguration : EntityTypeConfiguration<Documento>
    {
        public DocumentoConfiguration()
            : base()
        {
            ToTable("facturacion.documento");
            HasKey(p => p.idnumerodocumento);
            //Property(p => p.idnumerodocumento).IsRequired();
        }
    }
}
