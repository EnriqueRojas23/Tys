
using Componentes.Common.CustomAttributes;
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Monitoreo.Results
{
    public class ListarDetalleEmbarqueResults : QueryResult
    {
        public IEnumerable<ListarDetalleEmbarqueDto> Hits { get; set; }
        
    }
    public class ListarDetalleEmbarqueDto
    {
        public long idordentrabajo { get; set; }
        public long idmanifiesto { get; set; }
        public string numeroembarque { get; set; }
        public string distrito { get; set; }
        public string nummanifiesto { get; set; }
        public string numcp { get; set; }
        public DateTime fechadespacho { get; set; }
        public string tipooperacion { get; set; }
        public decimal Peso { get; set; }
        public decimal Volumen { get; set; }
        public int cantidad { get; set; }
        public int bulto { get; set; }
        public string origen { get; set; }
        public bool reintegrotributario { get; set; }


    }
}
