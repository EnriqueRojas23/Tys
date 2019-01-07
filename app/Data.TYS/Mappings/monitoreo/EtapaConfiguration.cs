
using Domain.TYS.Monitoreo;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Monitoreo
{
    public class EtapaConfiguration : EntityTypeConfiguration<Etapa>
    {
        public EtapaConfiguration()
            : base()
        {
            ToTable("monitoreo.etapa");
            HasKey(p => p.idetapa);
            //Property(p => p.idetapa).IsRequired();
        }
    }
}
