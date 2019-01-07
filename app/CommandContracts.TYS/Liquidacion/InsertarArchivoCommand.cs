

using CommandContracts.Common;
using System;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarArchivoCommand : Command
    {
        public long? idarchivo { get; set; }
        public long idordentrabajo { get; set; }
        public string nombrearchivo { get; set; }
        public string rutaacceso { get; set; }
        public string extension { get; set; }


    }
}
