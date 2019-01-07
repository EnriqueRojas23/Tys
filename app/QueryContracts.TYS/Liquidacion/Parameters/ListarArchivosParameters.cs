

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Monitoreo.Parameters
{
    public class ListarArchivosParameters : QueryParameter
    {
        public long? idarchivo { get; set; }
        public long? idordentrabajo { get; set; }
    }
}
