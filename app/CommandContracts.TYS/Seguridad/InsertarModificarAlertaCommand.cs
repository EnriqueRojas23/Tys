using CommandContracts.Common;
using System;
namespace CommandContracts.TYS.Seguridad
{
    public class InsertarModificarAlertaCommand : Command
    {
        public int? idalerta { get; set; }
        public int? usr_int_id { get; set; }
        public int idperiodicidad { get; set; }
        public string estados { get; set; }
        public int idmedio { get; set; }



    }
}
