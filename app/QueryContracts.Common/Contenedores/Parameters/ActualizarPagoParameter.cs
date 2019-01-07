
using QueryContracts.Common;
using System;

namespace QueryContracts.Core.Contenedores.Parameters
{
    public class ActualizarPagoParameter : QueryParameter
    {
        public Int64 IdCabecera { get; set; }
        public Int64 IdDetalle { get; set; }

    }
}
