using Domain.TYS.Seguridad;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguridad
{
    public class UsuarioConfiguration : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfiguration()
            : base()
        {
            ToTable("seguridad.tbl_usuario");

            HasKey(p => new { p.usr_int_id });
            //Property(p => p.usr_int_id).IsRequired();

        }
    }
}
