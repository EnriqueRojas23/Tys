﻿

using Domain.TYS.Seguridad;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguridad
{
    public class SistemaConfiguration : EntityTypeConfiguration<Sistema>
    {
        public SistemaConfiguration()
            : base()
        {
            ToTable("seguridad.tbl_sistema");

            HasKey(p => new { p.sis_int_id });
//            Property(p => p.sis_int_id).IsRequired();
        }
    }
}
