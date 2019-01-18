
using QueryContracts.Common;
namespace QueryContracts.TYS.Account.Results
{
    public class ObtenerUsuarioResult1 : QueryResult
    {
        public int usr_int_id { get; set; }
        public string usr_str_nombre { get; set; }
        public string usr_str_apellidos { get; set; }
        public string usr_str_red { get; set; }
        public string usr_str_email { get; set; }
        public int usr_int_bloqueado { get; set; }
        public int usr_bit_aprobado { get; set; }
        public string usr_str_tipoacceso { get; set; }
        public int? idestacionorigen { get; set; }
        public int? idcliente { get; set; }
        public int? iddistrito { get; set; }
        public int? idprovincia { get; set; }

    }
}
