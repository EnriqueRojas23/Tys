

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Monitoreo.Parameters
{
    public class DuplicarOTParameters : QueryParameter
    {
        public long idordentrabajo { get; set; }
        public long idusuarioregistro { get; set; }
        public long idordentrabajofinal { get; set; }
        public int cantidad { get; set; }
        public bool rechazototal { get; set; }

    }
}
