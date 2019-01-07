
using Domain.TYS.Pago;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Pago
{
    public class AsignacionConfiguration : EntityTypeConfiguration<Asignacion>
    {
        public AsignacionConfiguration () : base()
        {
            ToTable("pago.asignacion");
            HasKey(p => p.idasignacion);
            //Property(p => p.idasignacion).IsRequired();
        }
    }
}
