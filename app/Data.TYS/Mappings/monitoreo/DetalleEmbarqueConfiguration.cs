
using Domain.TYS.Monitoreo;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Monitoreo
{
    public class DetalleEmbarqueConfiguration : EntityTypeConfiguration<DetalleEmbarque>
    {
        public DetalleEmbarqueConfiguration()
            : base()
        {
            ToTable("monitoreo.detalleembarque");
            HasKey(p => p.iddetalleembarque);
            //Property(p => p.iddetalleembarque).IsRequired();
        }
    }
}
