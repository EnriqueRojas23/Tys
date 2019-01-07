using QueryContracts.Common;
using System;
using System.Collections.Generic;

namespace QueryContracts.TYS.Facturacion.Results
{
    public class ObtenerFactElectronicaResult : QueryResult
    {
        public long DocEntry { get; set; }
        public string estado { get; set; }
        public string tipo_de_nota_de_credito { get; set; }
        public string tipo_de_nota_de_debito { get; set; }
        public string fechaRegistro { get; set; }

    }
}