

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ObtenerOperacionCargaTotalVehiculoResult : QueryResult
    {
        public long idcarga { get; set; }
        public string numcarga {get;set;}
        public int idcliente {get;set;}
        public decimal vol {get;set;}
        public decimal peso { get; set; }
        public int idproveedor {get;set;}
        public int? idvehiculo { get; set; }
        public DateTime fechacreacion {get;set;}
        public string observacion {get;set;}
        public int idplanificador { get; set; }
        public int idagencia { get; set; }
        public int idtipooperacion { get; set; }
        public int idestacion { get; set; }
        public int idruta { get; set; }
        public bool activo {get;set;}
        public int usuarioregistro {get;set;}
        public string tipo { get; set; }
        public string placa { get; set; }
        public string proveedor { get; set; }
        public string chofer { get; set; }
        public decimal pesoporcentaje { get; set; }
        public decimal volporcentaje { get; set; }
        public string planificador { get; set; }
        public decimal cargautil { get; set; }
        public decimal volumenvehiculo { get; set; }
        public string serviciosadicionales { get; set; }

    }
}
