

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Monitoreo.Parameters
{
    public class ListarLiquidacionOperacionesParameters : QueryParameter
    {
        public int? idcliente { get; set; }
        public int? iddestinatario { get; set; }
        public string numcp { get; set; }
        public string grr { get; set; }
        public string fechainicio { get; set; }
        public string fechafin { get; set; }    
        public int? diastranscurridos { get; set; }
    }
}
