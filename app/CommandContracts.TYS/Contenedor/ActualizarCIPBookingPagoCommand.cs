

using CommandContracts.Common;
using System;
using System.Collections.Generic;
namespace CommandContracts.TYS.Contenedor
{
    public class ActualizarCIPBookingPagoCommand : Command
    {
        public long rbp_int_id { get; set; } 
        public string cip { get; set; }
        public string ids { get; set; }
    }
}
