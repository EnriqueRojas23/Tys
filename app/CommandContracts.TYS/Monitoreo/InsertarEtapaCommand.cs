

using CommandContracts.Common;
using System;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarEtapaCommand : Command
    {
        public long? idetapa { get; set; }
        public int idmaestroetapa { get; set; }
        public int idordentrabajo { get; set; }
        public int? idmanifiesto { get; set; }
        public string descripcion { get; set; }
        public string recurso { get; set; }
        public string documento { get; set; }
        public DateTime fechaetapa { get; set; }
        public DateTime fecharegistro { get; set; }
        public int idusuarioregistro { get; set; }
        public bool visible { get; set; }

    }
}
