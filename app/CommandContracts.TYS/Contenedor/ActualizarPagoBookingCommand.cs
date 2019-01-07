
using CommandContracts.Common;
using System;
using System.Collections.Generic;
namespace CommandContracts.TYS.Contenedor
{
    public class ActualizarPagoBookingCommand : Command
    {
        public string cip { get; set; }
        public string estado_cip { get; set; }
    }

}
