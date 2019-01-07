
using Domain.TYS.Pago;
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class FormulaConfiguration : EntityTypeConfiguration<Formula>
    {
        public FormulaConfiguration()
            : base()
        {
            ToTable("seguimiento.formula");
            HasKey(p => p.idformula);
            //Property(p => p.idformula).IsRequired();
        }
    }
}
