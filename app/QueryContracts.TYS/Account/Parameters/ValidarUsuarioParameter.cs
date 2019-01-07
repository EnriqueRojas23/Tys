

namespace QueryContracts.TYS.Account.Parameters
{
    using QueryContracts.Common;

    public class ValidarUsuarioParameter : QueryParameter
    {
        public string Usr_str_red { get; set; }
        public string Usr_str_password { get; set; }
    }
}
