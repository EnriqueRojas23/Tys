

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Monitoreo.Parameters
{
    public class ObtenerExisteCEParameters : QueryParameter
    {
        public string ce { get; set; }
        public int idtransporte { get; set; }
        public long? idembarque { get; set; }

    }
}
