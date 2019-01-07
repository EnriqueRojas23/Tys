

using CommandContracts.Common;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarChoferCommand : Command
    {
        public int? idchofer { get; set; }
        public string nombrechofer { get; set; }
        public string apellidochofer { get; set; }
        public string dni { get; set; }
        public string brevete { get; set; }
        public int edad { get; set; }
        public int idsexo { get; set; }
        public string telefono { get; set; }
        public string direccionchofer { get; set; }
        public bool activo { get; set; }
           

    }
}
