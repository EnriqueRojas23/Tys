
using Componentes.Common.CustomAttributes;
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Monitoreo.Results
{
    public class ListarEmbarqueFluvialResults : QueryResult
    {
        public IEnumerable<ListarEmbarqueFluvialDto> Hits { get; set; }
        
    }
    public class ListarEmbarqueFluvialDto
    {
        public long idembarque { get; set; }
        public string numeroembarque { get; set; }
        public string conocimientoembarque { get; set; }
        public string transporte { get; set; }
        public string puerto { get; set; }
        public DateTime fecharegistro { get; set; }
        public int idusuarioregistro { get; set; }
        public DateTime fechainiciocarga { get; set; }
        public DateTime? fechafincarga { get; set; }
        public DateTime? fechazarpe { get; set; }
        public DateTime? fechaatraque { get; set; }
        public DateTime? fechallegada { get; set; }
        public bool embarquecompleto { get; set; }

    }
}
