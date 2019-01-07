

using CommandContracts.Common;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarCamionCompletoCommand : Command
    {
        public long? idcamioncompleto { get; set; }
        public string codigocamioncompleto { get; set; }
        public int? idtipotransporte { get; set; }
        public int? idorigen { get; set; }
        public int? iddestino { get; set; }
        public int? idcliente { get; set; }
        public int? idformula { get; set; }
        public int? idtipounidad { get; set; }
        public int cantidaddestinos { get; set; }
        public int cantidadotscreaadas { get; set; }
        public decimal sobreestadia { get; set; }
        public int idvehiculo { get; set; }
        public long idcarga { get; set; }
        public int idtipooperacion { get; set; }
        public decimal total { get; set; }
        public decimal subtotal { get; set; }
        public decimal igv { get; set; }
        public int? idestacionorigen { get; set; }
        public int? idruta { get; set; }
        public int idconceptocobro { get; set; }
        public int usuarioregistro { get; set; }

    }
}
