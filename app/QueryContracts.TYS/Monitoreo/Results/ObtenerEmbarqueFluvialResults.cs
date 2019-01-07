
using Componentes.Common.CustomAttributes;
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Monitoreo.Results
{
    public class ObtenerEmbarqueFluvialResults : QueryResult
    {
        public long idembarque { get; set; }
        public string numeroembarque { get; set; }
        public string conocimientoembarque { get; set; }
        public int idtransporte { get; set; }
        public int idpuerto { get; set; }
        public DateTime fecharegistro { get; set; }
        public int usuarioregistro { get; set; }
        public DateTime? fechainiciocarga { get; set; }
        public DateTime? fechafincarga { get; set; }
        public DateTime? fechazarpe { get; set; }
        public DateTime? fechaatraque { get; set; }
        public DateTime? fechallegada { get; set; }
        public bool embarquecompleto { get; set; }
        public string transporte { get; set; }

    }
}
