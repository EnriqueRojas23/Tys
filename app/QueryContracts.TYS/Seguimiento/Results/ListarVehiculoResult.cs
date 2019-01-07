using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ListarVehiculoResult : QueryResult
    {
        public IEnumerable<ListarVehiculoDto> Hits { get; set; }
    }
    public class ListarVehiculoDto
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
        public string razonsocial { get; set; }
        public string tipovehiculo { get; set; }
        public string marcavehiculo { get; set; }
        public string modelovehiculo { get; set; }
        public string colorvehiculo { get; set; }
        public string combustible { get; set; }
        public string nombrechofer { get; set; }
        public string estado { get; set; }
        public string chofer { get; set; }




    }
}


