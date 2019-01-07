
using Domain.TYS.Monitoreo;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Monitoreo
{
    public class DetalleEtapaConfiguration : EntityTypeConfiguration<DetalleEtapa>
    {
        public DetalleEtapaConfiguration()
            : base()
        {
            ToTable("monitoreo.detalleetapa");
            HasKey(p => p.iddetalleetapa);
            //Property(p => p.iddetalleetapa).IsRequired();
        }
    }
}
