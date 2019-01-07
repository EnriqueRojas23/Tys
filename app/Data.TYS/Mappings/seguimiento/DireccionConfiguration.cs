
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class DireccionConfiguration : EntityTypeConfiguration<Direccion>
    {
        public DireccionConfiguration()
            : base()
        {
            ToTable("seguimiento.direccion");
            HasKey(p => p.iddireccion);
            //Property(p => p.iddireccion).IsRequired();
        }
    }
}
