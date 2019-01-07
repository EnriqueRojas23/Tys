
using Domain.TYS.Pago;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Pago
{
    public class ProveedorConfiguration : EntityTypeConfiguration<Proveedor>
    {
        public ProveedorConfiguration() : base()
        {
            ToTable("pago.proveedor");
            HasKey(p => p.idproveedor);
            //Property(p => p.idproveedor).IsRequired();
        }
    }
}
