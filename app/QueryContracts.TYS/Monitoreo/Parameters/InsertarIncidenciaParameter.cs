
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Monitoreo.Parameters
{
    public class InsertarIncidenciaParameter : QueryParameter
    {
        public List<InsertarIncidenciaParameterDto> Hits { get; set; }
    }

    public class InsertarIncidenciaParameterDto
    {
        public long? idincidencia { get; set; }
        public int idmaestroincidencia { get; set; }
        public long idordentrabajo { get; set; }
        public string descripcion { get; set; }
        public string observacion { get; set; }
        public DateTime fechaincidencia { get; set; }
        public DateTime fecharegistro { get; set; }
        public int idusuarioregistro { get; set; }
        public bool activo { get; set; }
        public string documento { get; set; }
        public string recurso { get; set; }
    }
}
