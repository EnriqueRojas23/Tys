

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Results
{
    public class ObtenerCamionCompletoResult : QueryResult
    {
        public long? idcamioncompleto { get; set; }
        public string codigocamioncompleto { get; set; }
        public int idtipotransporte { get; set; }
        public int idorigen { get; set; }
        public int iddestino { get; set; }
        public int idcliente { get; set; }
        public int idformula { get; set; }
        public int cantidaddestinos { get; set; }
        public int cantidadotscreaadas { get; set; }
        public long idcarga { get; set; }
        public int idvehiculo { get; set; }
        public decimal subtotal { get; set; }
        public decimal total { get; set; }
        public decimal igv { get; set; }
        public decimal sobreestadia { get; set; }
        public int idruta { get; set; }
        public int idestacion { get; set; }
        public int idtipooperacion { get; set; }
        

      

    }
}
