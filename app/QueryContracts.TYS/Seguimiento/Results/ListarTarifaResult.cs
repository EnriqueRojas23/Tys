using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarTarifaResult : QueryResult
    {
        public IEnumerable<ListarTarifaDto> Hits { get; set; }
    }
    public class ListarTarifaDto
    {
        public int idtarifa { get; set; }
        public int idcliente { get; set; }
        public int idorigen { get; set; }
        public int idorigenprovincia { get; set; }
        public int idorigendistrito { get; set; }
        public string origenprovincia { get; set; }
        public string origendistrito { get; set; }

        public int iddistrito { get; set; }
        public int idformula { get; set; }
        public bool urgente { get; set; }
        public bool autoserv { get; set; }
        public bool val { get; set; }
        public int idtipotransporte { get; set; }
        public int idtipounidad { get; set; }
        public int idmoneda { get; set; }
        public decimal montobase { get; set; }
        public decimal minimo { get; set; }
        public decimal desde { get; set; }
        public decimal hasta { get; set; }
        public decimal precio { get; set; }
        public string conceptos { get; set; }
        public string origen { get; set; }
        public string departamento { get; set; }
        public string provincia { get; set; }
        public string distrito { get; set; }
        public string zona { get; set; }
        public string tipounidad { get; set; }
        public string tipotransporte { get; set; }
        public string moneda { get; set; }
        public string formula { get; set; }
        public decimal adicional { get; set; }

            


    }
}


