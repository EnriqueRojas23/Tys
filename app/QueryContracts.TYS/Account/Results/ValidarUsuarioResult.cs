﻿    using System;

namespace QueryContracts.TYS.Account.Results
{
    using QueryContracts.Common;

    public class ValidarUsuarioResult : QueryResult
    {

        public int Usr_int_pwdvalido { get; set; }
        public int Usr_int_rolinvalido { get; set; }
        public string Usr_str_recordarpwd { get; set; }
        public int? Usr_int_aprobado { get; set; }
        public int? Usr_int_bloqueado { get; set; }
        public DateTime? Usr_dat_fecvctopwd { get; set; }
        public int? Usr_int_numintentos { get; set; }
        public DateTime? Usr_dat_ultfeclogin { get; set; }
        public string Usr_str_red { get; set; }
        public int Usr_int_id { get; set; }


    }
}
