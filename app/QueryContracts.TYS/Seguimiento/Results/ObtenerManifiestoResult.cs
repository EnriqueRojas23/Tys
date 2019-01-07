

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ObtenerManifiestoResult : QueryResult
    {

        public long idmanifiesto { get; set; }
        public string nummanifiesto { get; set; }
        public DateTime fecharegistro { get; set; }
        public int idusuarioregistro { get; set; }
        public long iddespacho { get; set; }
        public bool activo { get; set; }
        public string numhojaruta { get; set; }
        public int idvehiculo { get; set; }
        public bool reintegrotributario { get; set; }
        public int idtipooperacion { get; set; }
        public int iddestino { get; set; }


    }
}
