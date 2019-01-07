
using Domain.TYS.Pago;
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class DetalleValijaConfiguration : EntityTypeConfiguration<DetalleValija>
    {
        public DetalleValijaConfiguration()
            : base()
        {
            ToTable("seguimiento.detallevalija");
            HasKey(p => p.iddespachovalija);
            //Property(p => p.iddespachovalija).IsRequired();
        }
    }
}
