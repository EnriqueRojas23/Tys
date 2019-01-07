

using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Configuracion.Results
{
    public class ListarMultiusoPorTipoResult : QueryResult
    {
        public IEnumerable<ListarMultiusoPorTipoDto> Hits { get; set; }
    }

    public class ListarMultiusoPorTipoDto
    {
        public int mlt_int_id { get; set; }
        public int mlt_int_id_padre { get; set; }
        public string mlt_str_nombre { get; set; }
        public string mlt_str_descripcion { get; set; }
        public string mlt_str_valor { get; set; }
        public string mlt_str_alcance { get; set; }
        public DateTime mlt_dat_fecha_creacion { get; set; }
        public string mlt_str_usuario_creacion { get; set; }
        public string mtl_str_tipo { get; set; }
        public string mlt_str_valor2 { get; set; }
    }
}
