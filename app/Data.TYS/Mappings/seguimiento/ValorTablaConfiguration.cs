
using Domain.TYS.Pago;
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class ValorTablaConfiguration : EntityTypeConfiguration<Valortabla>
    {
        public ValorTablaConfiguration()
            : base()
        {
            ToTable("seguimiento.valortabla");
            HasKey(p => p.idvalortabla);
            //Property(p => p.idvalortabla).IsRequired();
        }
    }
}
