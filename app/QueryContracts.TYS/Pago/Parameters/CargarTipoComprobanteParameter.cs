
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Pago.Parameters
{
    public class CargarTipoComprobanteParameter : QueryParameter
    {
        public int  idtipotransporte { get; set; }
        public int idetapa { get; set; }
        public int idproveedor { get; set; }
    }
}
