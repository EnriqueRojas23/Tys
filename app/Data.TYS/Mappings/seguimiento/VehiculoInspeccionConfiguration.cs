
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class VehiculoInspeccionConfiguration : EntityTypeConfiguration<VehiculoInspeccion>
    {
        public VehiculoInspeccionConfiguration()
            : base()
        {
            ToTable("seguimiento.vehiculoinspeccion");
            HasKey(p => p.idvehiculoinspeccion);
            //Property(p => p.idvehiculoinspeccion).IsRequired();
        }
    }
}
