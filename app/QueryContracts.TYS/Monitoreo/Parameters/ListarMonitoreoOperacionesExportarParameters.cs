

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Monitoreo.Parameters
{
    public class ListarMonitoreoOperacionesExportarParameters : QueryParameter
    {

        public int? idruta { get; set; }
        public int? idestacion { get; set; }
        public string fechainicio { get; set; }
        public string fechafin { get; set; }
        public string guia { get; set; }
        public string chofer { get; set; }
        public string placa { get; set; }
        public string documento { get;set; }
        public string recurso { get; set; }
    }
}
