

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Monitoreo.Parameters
{
    public class DuplicarGuiaParameters : QueryParameter
    {
        public long idguiaremisioncliente { get; set; }
        public bool rechazototal { get; set; }
        public long idordentrabajo { get; set; }
        public long idordennueva { get; set; }

    }
}
