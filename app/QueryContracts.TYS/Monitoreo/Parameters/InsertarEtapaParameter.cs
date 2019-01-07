
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Monitoreo.Parameters
{
    public class InsertarEtapaParameter : QueryParameter
    {
        public List<InsertarEtapaParameterDto> Hits { get; set; }
    }

    public class InsertarEtapaParameterDto
    {
        public long? idetapa { get; set; }
        public int idmaestroetapa { get; set; }
        public long idordentrabajo { get; set; }
        public int? idmanifiesto { get; set; }
        public string descripcion { get; set; }
        public string recurso { get; set; }
        public string documento { get; set; }
        public DateTime fechaetapa { get; set; }
        public DateTime fecharegistro { get; set; }
        public int idusuarioregistro { get; set; }
        public bool visible { get; set; }
        public string grr { get; set; }
    }
}
