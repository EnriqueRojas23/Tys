
using Domain.TYS.Pago;
using Domain.TYS.Seguimiento;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguimiento
{
    public class ProveedorClienteConfiguration : EntityTypeConfiguration<ProveedorCliente>
    {
        public ProveedorClienteConfiguration()
            : base()
        {
            ToTable("seguimiento.proveedorcliente");
            HasKey(p => p.idproveedorcliente);
            //Property(p => p.idproveedorcliente).IsRequired();
        }
    }
}
