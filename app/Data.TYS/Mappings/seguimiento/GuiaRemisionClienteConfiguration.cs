
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class GuiaRemisionClienteConfiguration : EntityTypeConfiguration<GuiaRemisionCliente>
    {
        public GuiaRemisionClienteConfiguration()
            : base()
        {
            ToTable("seguimiento.guiaremisioncliente");
            HasKey(p => p.idguiaremisioncliente);
            //Property(p => p.idguiaremisioncliente).IsRequired();
        }
    }
}
