﻿
using QueryContracts.Common;
namespace QueryContracts.TYS.Seguridad.Parameters
{
    public class ValidarExisteUsuarioParameter  : QueryParameter
    {
        
        public string usr_str_red { get; set; }
        public string usr_str_email { get; set; }

    }
}
