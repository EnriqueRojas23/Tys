

using CommandContracts.Common;
using System;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarDespachoCommand : Command
    {
        public long? iddespacho { get; set; }
        public int idvehiculo { get; set; }
        public DateTime? fechasalida { get; set; }
        public int? idusuariosalida { get; set; }
        public int idestado { get; set; }
        public int _tipoop { get; set; }
           

    }
}
