﻿using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarProvinciasResult : QueryResult
    {
        public IEnumerable<ListarProvinciasDto> Hits { get; set; }
    }
    public class ListarProvinciasDto
    {
        public int idprovincia { get; set; }
        public string provincia { get; set; }

    }
}


