

namespace QueryContracts.TYS.Account.Results
{
    using System;
    using System.Collections.Generic;

    using QueryContracts.Common;

    public class ObtenerUsuarioResult : QueryResult
    {
        public ObtenerUsuarioResult()
        {
            ListaRoles = new List<RolDto>();
        }

        public int Usr_int_id { get; set; }
        public string Usr_str_nombre { get; set; }
        public string Usr_str_apellidos { get; set; }
        public string Usr_str_red { get; set; }
        public string Usr_str_email { get; set; }
        public int? Usr_int_cambiarpwd { get; set; }
        public int? Usr_int_aprobado { get; set; }
        public int? Usr_int_bloqueado { get; set; }
        public int? Usr_int_online { get; set; }
        public DateTime? Usr_dat_fecregistro { get; set; }
        public DateTime? Usr_dat_ultfecbloqueo { get; set; }
        public DateTime? Usr_dat_ultfeclogin { get; set; }
        public DateTime? Usr_dat_fecvctopwd { get; set; }
        public int Usr_int_numintentos { get; set; }
        public int? Sis_int_id { get; set; }
        public int? Rol_int_id { get; set; }
        public DateTime? Usr_dat_fecvctousuario { get; set; }
        public string usr_str_tipoacceso { get; set; }
        public string clientes { get; set; }


        public string respuestamensaje { get; set; }

        public IEnumerable<RolDto> ListaRoles { get; set; }
    }

    public class RolDto
    {
        public int Rol_int_id { get; set; }
        public string Rol_str_alias { get; set; }
        public string Rol_str_descrip { get; set; }
    }
}
