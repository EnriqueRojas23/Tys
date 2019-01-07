
using Domain.TYS.Pago;
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class ClienteConfiguration : EntityTypeConfiguration<Cliente>
    {
        public ClienteConfiguration()
            : base()
        {
            ToTable("seguimiento.cliente");
            HasKey(p => p.idcliente);
            //Property(p => p.idcliente).IsRequired();
        }
    }
}
