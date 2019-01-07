﻿using QueryContracts.Common;
using System;

namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ListarOrdenTrabajoClienteParameters : QueryParameter
    {
        public string idscliente { get; set; }
        public string numcp { get; set; }
        public string fecinicio { get; set; }
        public string fecfin { get; set; }
        public string grr { get; set; }

    }
}