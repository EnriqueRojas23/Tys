
using Domain.TYS.Pago;
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class CamionCompletoConfiguration : EntityTypeConfiguration<CamionCompleto>
    {
        public CamionCompletoConfiguration()
            : base()
        {
            ToTable("seguimiento.camioncompleto");
            HasKey(p => p.idcamioncompleto);
            //Property(p => p.idcamioncompleto).IsRequired();
        }
    }
}
