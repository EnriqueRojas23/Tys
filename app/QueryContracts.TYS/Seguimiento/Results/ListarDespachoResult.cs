using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarDespachoResult : QueryResult
    {
        public IEnumerable<ListarDespachoDto> Hits { get; set; }
    }
    public class ListarDespachoDto
    {
        public int idvehiculo { get; set; }
        public string placa { get; set; }
        public string proveedor { get; set; }
        public string chofer { get; set; }
        public string agencia { get; set; }
        public string rutas { get; set; }
        public string  tiposoperacion { get; set; }
        public decimal vol { get; set; }
        public decimal peso { get; set; }
        public string hojaruta { get; set; }
        public string estado { get; set; }
        public int cantidadprecintos { get; set; }
        public int cantidadvalija { get; set; } 
        public int iddespacho { get; set; }
        public int idestado { get; set; }
        public string estadovehiculo { get; set; }
       
            


    }
}


