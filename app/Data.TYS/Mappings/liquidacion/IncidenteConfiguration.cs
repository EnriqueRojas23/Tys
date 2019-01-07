
using Domain.TYS.Monitoreo;
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Monitoreo
{
    public class ArchivoConfiguration : EntityTypeConfiguration<Archivo>
    {
        public ArchivoConfiguration()
            : base()
        {
            ToTable("liquidacion.archivo");
            HasKey(p => p.idarchivo);
            //Property(p => p.idarchivo).IsRequired();
        }
    }
}
