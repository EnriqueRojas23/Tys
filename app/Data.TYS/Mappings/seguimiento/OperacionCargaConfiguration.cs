
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class OperacionCargaConfiguration : EntityTypeConfiguration<OperacionCarga>
    {
        public OperacionCargaConfiguration()
            : base()
        {
            ToTable("seguimiento.operacioncarga");
            HasKey(p => p.idcarga);
            //Property(p => p.idcarga).IsRequired();
        }
    }
}
