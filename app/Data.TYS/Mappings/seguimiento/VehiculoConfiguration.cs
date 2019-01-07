
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class VehiculoConfiguration : EntityTypeConfiguration<Vehiculo>
    {
        public VehiculoConfiguration()
            : base()
        {
            ToTable("pago.vehiculo");
            HasKey(p => p.idvehiculo);
            //Property(p => p.idvehiculo).IsRequired();
        }
    }
}
