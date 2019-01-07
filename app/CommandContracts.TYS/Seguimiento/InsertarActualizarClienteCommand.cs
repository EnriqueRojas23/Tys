

using CommandContracts.Common;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarClienteCommand : Command
    {
        public int? idcliente { get; set; }
        public string razonsocial { get; set; }
        public string ruc { get; set; }
        public string nombrecorto { get; set; }
        public int? iddireccion { get; set; }
        public int idubigeo { get; set; }
        public decimal lineacredito { get; set; }
        public int idmonedalinea { get; set; }
        public string rutalogo { get; set; }
        public bool activo { get; set; }
        public bool pagocontado { get; set; }
           

    }
}
