
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class ManifiestoConfiguration : EntityTypeConfiguration<Manifiesto>
    {
        public ManifiestoConfiguration()
            : base()
        {
            ToTable("seguimiento.manifiesto");
            HasKey(p => p.idmanifiesto);
            //Property(p => p.idmanifiesto).IsRequired();
        }
    }
}
