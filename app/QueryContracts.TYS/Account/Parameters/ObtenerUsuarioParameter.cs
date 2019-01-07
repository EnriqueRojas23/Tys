

namespace QueryContracts.TYS.Account.Parameters
{
    using QueryContracts.Common;

    public class ObtenerUsuarioParameter : QueryParameter
    {
        public string Usr_str_red { get; set; }
        public int? Usr_int_id { get; set; }
    }
}
