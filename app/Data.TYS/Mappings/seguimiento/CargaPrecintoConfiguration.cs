
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class CargaPrecintoConfiguration : EntityTypeConfiguration<CargaPrecinto>
    {
        public CargaPrecintoConfiguration()
            : base()
        {
            ToTable("seguimiento.cargaprecinto");
            HasKey(p => p.idcargaprecinto);
            //Property(p => p.idcargaprecinto).IsRequired();
        }
    }
}
