

using CommandContracts.Common;
using System;
namespace CommandContracts.TYS.Seguimiento
{
    public class ActualizarLiquidacionCommand : Command
    {
        public long idordentrabajo { get; set; }
        public bool archivado { get; set; }
        public int idusuarioconciliacion { get; set; }
        public DateTime fechaentregaconciliacion{ get; set; }
        public int idestado { get; set; }


    }
}
