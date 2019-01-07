


using CommandContracts.Common;
namespace CommandContracts.TYS.Pago
{
    public class InsertarActualizarProveedorCommand : Command
    {
        public int? idproveedor { get; set; }
        public string razonsocial { get; set; }
        public string ruc { get; set; }
        public string placaasociada { get; set; }
        public bool activo { get; set; }
        public string direccion { get; set; }

    }
}
