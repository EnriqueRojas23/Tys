
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class PrecintoConfiguration : EntityTypeConfiguration<Precinto>
    {
        public PrecintoConfiguration()
            : base()
        {
            ToTable("seguimiento.precinto");
            HasKey(p => p.idprecinto);
            //Property(p => p.idprecinto).IsRequired();
        }
    }
}
