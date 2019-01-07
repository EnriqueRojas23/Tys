

using CommandContracts.Common;
namespace CommandContracts.TYS.Seguimiento
{
    public class InsertarActualizarTarifaCommand : Command
    {
        public int? idtarifa { get; set; }
        public int idcliente { get; set; }
        public int? idorigen { get; set; }
        public int? iddepartamento { get; set; }
        public int? idprovincia { get; set; }
        public int? idorigenprovincia { get; set; }
        public int? iddistrito { get; set; }
        public int? idorigendistrito { get; set; }
        public int idformula { get; set; }
        public int? idzona { get; set; }
        public bool urgente { get; set; }
        public bool autoserv { get; set; }
        public bool val { get; set; }
        public int idtipotransporte { get; set; }
        public int idtipounidad { get; set; }
        public int idmoneda { get; set; }
        public decimal? montobase { get; set; }
        public decimal? minimo { get; set; }
        public decimal? desde { get; set; }
        public decimal? hasta { get; set; }
        public decimal precio { get; set; }
        public decimal? adicional { get; set; }
           

    }
}
