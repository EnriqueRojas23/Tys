
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class OrdenTrabajoConfiguration : EntityTypeConfiguration<OrdenTrabajo>
    {
        public OrdenTrabajoConfiguration()
            : base()
        {
            ToTable("seguimiento.ordentrabajo");
            HasKey(p => p.idordentrabajo);
            //Property(p => p.idordentrabajo).IsRequired();
        }
    }
}
