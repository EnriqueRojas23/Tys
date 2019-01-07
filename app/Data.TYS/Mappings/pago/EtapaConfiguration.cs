
using Domain.TYS.Pago;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Pago
{
    public class EtapaConfiguration : EntityTypeConfiguration<EtapaProveedor>
    {
        public EtapaConfiguration () : base()
        {
            ToTable("pago.etapa");
            HasKey(p => p.idetapa);
           // Property(p => p.idetapa).IsRequired();
        }
    }
}
