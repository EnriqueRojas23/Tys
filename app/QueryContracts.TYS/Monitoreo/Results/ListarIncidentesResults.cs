
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Monitoreo.Results
{
    public class ListarIncidentesResults : QueryResult
    {
        public IEnumerable<ListarIncidentesDto> Hits { get; set; }
        
    }
    public class ListarIncidentesDto
    {
        public long idincidencia { get; set; }
        public string codincidencia { get; set; }
        public string  numcp { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fecharegistro { get; set; }
        public string usuario { get; set; }
        public string incidencia { get; set; }
        public string descripcion { get; set; }
        public int idmaestroincidencia { get; set; }
        public bool visible { get; set; }
        public string movimiento { get; set; }
        public string tipo { get; set; }
       
    }
}
