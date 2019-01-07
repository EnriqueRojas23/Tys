using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarImpresionCargaResult : QueryResult
    {
        public IEnumerable<ListarImpresionCargaDto> Hits { get; set; }
    }
    public class ListarImpresionCargaDto
    {
        public long idcarga { get; set; }
        public long idmanfiesto { get; set; }
        public int idvehiculo { get; set; }
        public string placa { get; set; }
        public string transportista { get; set; }
        public string chofer { get; set; }
        public string rutas { get; set; }
        public decimal vol { get; set; }
        public decimal peso { get; set; }
        public string numcarga { get; set; }
        public long iddespacho { get; set; }
        public string numhojaruta { get; set; }

    }
}


