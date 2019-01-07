
using QueryContracts.Common;
using System.Collections.Generic;
namespace QueryContracts.TYS.Seguridad.Result
{
    public class ListarClientesUsuarioResult : EliminarPaginaResult
    {
        public IEnumerable<ListarClientesUsuarioDto> Hits { get; set; }
    }

    public class ListarClientesUsuarioDto
    {
        
        public long  ucl_int_id { get; set; }
        public int usr_int_id { get; set; }
        public string ruc_str_numero { get; set; }

    }
}
