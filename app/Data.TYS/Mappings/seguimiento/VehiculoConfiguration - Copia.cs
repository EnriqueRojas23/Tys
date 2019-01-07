
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class ChoferConfiguration : EntityTypeConfiguration<Chofer>
    {
        public ChoferConfiguration()
            : base()
        {
            ToTable("seguimiento.chofer");
            HasKey(p => p.idchofer);
            //Property(p => p.idchofer).IsRequired();
        }
    }
}
