
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ListarOrdenTrabajoComprobanteParameters : QueryParameter
    {
        //public int? idcliente { get; set; }
        public string numcp { get; set; }
        public string guia { get; set; }
        public int? idvehiculo { get; set; }
        public int idetapa { get; set; }
        
    }
}
