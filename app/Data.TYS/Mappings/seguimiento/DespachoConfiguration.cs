
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class DespachoConfiguration : EntityTypeConfiguration<Despacho>
    {
        public DespachoConfiguration()
            : base()
        {
            ToTable("seguimiento.despacho");
            HasKey(p => p.iddespacho);
            //Property(p => p.iddespacho).IsRequired();
        }
    }
}
