using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarTarifaOrdenResult : QueryResult
    {
        public IEnumerable<ListarTarifaOrdenDto> Hits { get; set; }
    }
    public class    ListarTarifaOrdenDto
    {
        public int idtarifa { get; set; }
        public int idcliente { get; set; }
        public int idmoneda { get; set; }
        public decimal montobase { get; set; }
        public decimal minimo { get; set; }
        public decimal desde { get; set; }
        public decimal hasta { get; set; }
        public decimal precio { get; set; }
        public string conceptos { get; set; }
        public string moneda { get; set; }
        public string formula { get; set; }
        public int idprovincia { get; set; }
        public int iddistrito { get; set; }
        public int iddepartamento { get; set; }
        public string detalle { get; set; }
        public decimal? adicional { get; set; }
        public int idtipounidad { get; set; }

            


    }
}


