

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ObtenerVehiculoResult : QueryResult
    {
        public int idvehiculo { get; set; }
        public int idproveedor { get; set; }
        public int idtipo { get; set; }
        public int idmarca { get; set; }
        public int idmodelo { get; set; }
        public int idanio { get; set; }
        public int idcolor { get; set; }
        public int idcombustible { get; set; }
        public string regmtc { get; set; }
        public string placa { get; set; }
        public string confveh { get; set; }
        public decimal pesobruto { get; set; }
        public decimal cargautil { get; set; }
        public string seriemotor { get; set; }
        public string kilometraje { get; set; }
        public int idestado { get; set; }
        public int idchofer { get; set; }
        public int idorigen { get; set; }
        public string nombrechofer { get; set; }
        public string dni { get; set; }
        public string proveedor { get; set; }
        public string inspecciones { get; set; }


    }
}
