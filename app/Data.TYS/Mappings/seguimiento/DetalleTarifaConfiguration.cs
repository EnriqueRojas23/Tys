
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class DetalleTarifaConfiguration : EntityTypeConfiguration<DetalleTarifa>
    {
        public DetalleTarifaConfiguration()
            : base()
        {
            ToTable("seguimiento.detalletarifa");
            HasKey(p => p.iddetalletarifa);
            //Property(p => p.iddetalletarifa).IsRequired();
        }
    }
}
