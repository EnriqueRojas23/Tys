using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarServiciosResult : QueryResult
    {
        public IEnumerable<ListarServiciosDto> Hits { get; set; }
    }
    public class ListarServiciosDto
    {
        public long idserviciooperacion { get; set; }
        public int idcarga { get; set; }
        public int idservicio { get; set; }
        public string servicio { get; set; }
        public int cantidad { get; set; }

    }
}


