﻿

using QueryContracts.Common;
namespace QueryContracts.TYS.Seguridad.Parameters
{
    public class ObtenerDatosBasicosUsuarioParameter : QueryParameter
    {
        public int? usr_int_id { get; set; }
        public string usr_str_red { get; set; }
    }
}
