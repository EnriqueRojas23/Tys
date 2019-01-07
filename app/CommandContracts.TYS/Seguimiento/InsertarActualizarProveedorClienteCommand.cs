

using CommandContracts.Common;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarProveedorClienteCommand : Command
    {
        public int? idproveedorcliente { get; set; }
        public int idcliente { get; set; }
        public int idproveedor { get; set; }

    }
}
