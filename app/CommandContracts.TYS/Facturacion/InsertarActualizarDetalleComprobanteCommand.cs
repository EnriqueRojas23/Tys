﻿

using CommandContracts.Common;
using System;
namespace CommandContracts.TYS.Facturacion
{
    public class InsertarActualizarDetalleComprobanteCommand : Command
    {

        public long? iddetallecomprobante { get; set; }
        public long idcomprobantepago { get; set; }
        public long? idordentrabajo { get; set; }
        public decimal subtotal { get; set; }
        public decimal igv { get; set; }
        public decimal total { get; set; }
        public decimal? recargo { get; set; }
        public string descripcion { get; set; }

    }
}
