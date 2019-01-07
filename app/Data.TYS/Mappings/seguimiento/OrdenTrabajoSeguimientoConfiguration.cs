
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class OrdenTrabajoSeguimientoConfiguration : EntityTypeConfiguration<OrdenTrabajoSeguimiento>
    {
        public OrdenTrabajoSeguimientoConfiguration()
            : base()
        {
            ToTable("seguimiento.ordentrabajoseguimiento");
            HasKey(p => p.idseguimiento);
            //Property(p => p.idseguimiento).IsRequired();
        }
    }
}
