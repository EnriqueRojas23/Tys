
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Facturacion.Parameters
{
    public class VincularPreliquidacionOTParameters : QueryParameter
    {
        public string idsordentrabajo { get; set; }
        public long? idpreliquidacion { get; set; }
    }
}
