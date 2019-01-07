

using CommandContracts.Common;
using System;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarIncidenteCommand : Command
    {
        public long? idincidencia { get; set; }
        public long idordentrabajo { get; set; }
        public DateTime fechaincidencia { get; set; }
        public DateTime fecharegistro { get; set; }
        public int idusuarioregistro { get; set; }
        public string descripcion { get; set; }
        public string observacion { get; set; }
        public int idmaestroincidencia { get; set; }
        public string recurso { get; set; }
        public string documento { get; set; }
        public bool visible { get; set; }
        public string movimiento { get; set; }

    }
}
