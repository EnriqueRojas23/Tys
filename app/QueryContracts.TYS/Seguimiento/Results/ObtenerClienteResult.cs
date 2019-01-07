

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ObtenerClienteResult : QueryResult
    {
        public int idcliente { get; set; }
        public string razonsocial { get; set; }
        public string ruc { get; set; }
        public string nombrecorto { get; set; }
        public string direccion { get; set; }
        public int iddireccion { get; set; }
        public int idubigeo { get; set; }
        public decimal lineacredito { get; set; }
        public string rutalogo { get; set; }
        public bool activo { get; set; }
        public int idmonedalinea { get; set; }
        public string ubigeo { get; set; }
        public string codigo { get; set; }
        public decimal? lineaconsumida { get; set; }
        public bool pagocontado { get; set; }
    }
}
