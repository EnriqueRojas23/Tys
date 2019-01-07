
using Domain.TYS.Monitoreo;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Monitoreo
{
    public class EmbarqueConfiguration : EntityTypeConfiguration<Embarque>
    {
        public EmbarqueConfiguration()
            : base()
        {
            ToTable("monitoreo.embarque");
            HasKey(p => p.idembarque);
            //Property(p => p.idembarque).IsRequired();
        }
    }
}
