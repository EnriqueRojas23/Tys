
using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Seguimiento.Parameters
{
    public class ListarDistritoZonaEditarParameters : QueryParameter
    {
        public int? idzona { get; set; }
        public int? idprovincia { get; set; }
    }
}
