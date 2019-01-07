using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarTipoUnidadTarifaResult : QueryResult
    {
        public IEnumerable<ListarTipoUnidadTarifaDto> Hits { get; set; }
    }
    public class ListarTipoUnidadTarifaDto
    {
        public int idtipounidad { get; set; }
        public string tipounidad { get; set; }
        public int iddistrito { get; set; }
        public int idprovincia { get; set; }
        public int iddepartamento { get; set; }
        public string formula { get; set; }
        
    }
}


