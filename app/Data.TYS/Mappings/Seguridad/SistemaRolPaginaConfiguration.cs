﻿
using Domain.TYS.Seguridad;
using System.Data.Entity.ModelConfiguration;
namespace Data.TYS.Mappings.Seguridad
{
    public class SistemaRolPaginaConfiguration : EntityTypeConfiguration<SistemaRolPagina>
    {
        public SistemaRolPaginaConfiguration()
            : base()
        {
            ToTable("seguridad.tbl_seg_sistema_rol_pagina");

            HasKey(p => new { p.srp_int_id });
            //Property(p => p.srp_int_id).IsRequired();
        }
    }
}
