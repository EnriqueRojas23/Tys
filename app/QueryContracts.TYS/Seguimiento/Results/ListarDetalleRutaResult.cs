using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarDetalleRutaResult : QueryResult
    {
        public IEnumerable<ListarDetalleRutaDto> Hits { get; set; }
    }
    public class ListarDetalleRutaDto
    {
        public int iddetalleruta { get; set; }
        public int idruta { get; set; }
        public int idorden { get; set; }
        public int idorigen { get; set; }
        public int iddepartamento { get; set; }
        public int idprovincia { get; set; }
        public int iddistrito { get; set; }
        public string km { get; set; }
        public int idtipotransporte { get; set; }
        public string leadida { get; set; }
        public string leadretorno { get; set; }
        public string leaddocumentario { get; set; }
        public string etapas { get; set; }
        public string departamento { get; set; }
        public string provincia { get; set; }
        public string distrito { get; set; }
        public string origen { get; set; }
        public string tipotransporte { get; set; }



    }
}


