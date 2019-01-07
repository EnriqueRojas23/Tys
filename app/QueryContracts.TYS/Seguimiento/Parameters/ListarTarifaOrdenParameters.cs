using QueryContracts.Common;
using System;

namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ListarTarifaOrdenParameters : QueryParameter
    {
        public int idorigendistrito { get; set; }
        public int idorigenprovincia { get; set; }
        public int idorigendepartamento { get; set; }

        public int iddepartamento { get; set; }
        public int idprovincia { get; set; }
        public int iddistrito { get; set; }

        public int idcliente { get; set; }
        public int idformula { get; set; }
        public int idtipotransporte { get; set; }

        public int? idconceptocobro { get; set; }
    }
}