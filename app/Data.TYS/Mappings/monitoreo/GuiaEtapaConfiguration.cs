
using Domain.TYS.Monitoreo;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Monitoreo
{
    public class GuiaEtapaConfiguration : EntityTypeConfiguration<GuiaEtapa>
    {
        public GuiaEtapaConfiguration()
            : base()
        {
            ToTable("monitoreo.guiaetapa");
            HasKey(p => p.idguiaetapa);
            //Property(p => p.idguiaetapa).IsRequired();
        }
    }
}
