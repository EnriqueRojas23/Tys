

using Domain.TYS.Seguridad;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguridad
{
    public class PaginaConfiguration : EntityTypeConfiguration<Pagina>
    {
        public PaginaConfiguration()
            : base()
        {
            ToTable("seguridad.tbl_seg_pagina");

            HasKey(p => new { p.pag_int_id });
            //Property(p => p.pag_int_id).IsRequired();
        }
    }
}
