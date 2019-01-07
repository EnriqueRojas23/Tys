

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Pago.Results
{
    public class ObtenerComprobanteResult : QueryResult
    {
        public long  idcomprobante { get; set; }
        public int idorigen { get; set; }
        public int iddestino { get; set; }
        public DateTime fechacomprobante { get; set; }
        public DateTime fecharegistro { get; set; }
        public string serienumero { get; set; }
        public int idproveedor { get; set; }
        public int idetapa { get; set; }
        public int idtipotransporte { get; set; }
        public int idtipocomprobante { get; set; }
        public int idmoneda { get; set; }
        public decimal monto { get; set; }
        public string concepto { get; set; }
        public int idvehiculo { get; set; }
        public int idtipofacturacion { get; set; }
        public string actainforme { get; set; }

        public bool activo { get; set; }
        public string placa { get; set; }
        public string observacion { get; set; }
    }
    
}
