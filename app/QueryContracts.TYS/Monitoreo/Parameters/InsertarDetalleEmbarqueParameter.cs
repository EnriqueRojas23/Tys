
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Monitoreo.Parameters
{
    public class InsertarDetalleEmbarqueParameter : QueryParameter
    {
        public List<InsertarDetalleEmbarqueDto> Hits { get; set; }
    }

    public class InsertarDetalleEmbarqueDto
    {
        public long iddetalleembarque { get; set; }
        public long idembarque { get; set; }
        public long? idmanifiesto { get; set; }
        public long idordentrabajo { get; set; }
        public int idpuerto { get; set; }
        public DateTime? fechadescarga { get; set; }
        public int? idusuariodescarga { get; set; }
        public DateTime? fechacontrolsunat { get; set; }
        public int? idusuariocontrolsunat { get; set; }
    }
}
