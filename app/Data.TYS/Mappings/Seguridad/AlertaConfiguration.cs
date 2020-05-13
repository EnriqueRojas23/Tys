using Domain.TYS.Seguridad;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguridad
{
    public class AlertaConfiguration : EntityTypeConfiguration<Alerta>
    {
        public AlertaConfiguration(): base()
        {
            ToTable("seguridad.alerta");
            HasKey(p => new { p.idalerta });
            
        }
    }
}
