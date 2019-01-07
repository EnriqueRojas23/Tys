

using CommandContracts.Common;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarGuiaRemisionClienteCommand : Command
    {
        public long? idguiaremisioncliente { get; set; }
        public long? idordentrabajo { get; set; }
        public string nroguia { get; set; }
        public int idcobrarpor { get; set; }
        public int? bulto { get; set; }
        public decimal? peso { get; set; }
        public decimal? volumen { get; set; }
        public decimal? pesovol { get; set; }
        public string documento { get; set; }

           

    }
}
