
using QueryContracts.Common;
using System;

namespace QueryContracts.Core.Contenedores.Parameters
{
    public class EliminarDocumentoSolportParameter : QueryParameter
    {
        public Int64 IdReserva { get; set; }
        public string NombreFichero { get; set; }

    }
}
