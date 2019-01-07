
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ListarValorxTablaParameters : QueryParameter
    {
        public int? idtabla { get; set; }
        public string search { get; set; }
    }
}
