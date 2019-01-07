using Componentes.Common.CustomAttributes;
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Pago.Results
{
    public class ListarExportarAsignacionResult : QueryResult
    {
        public IEnumerable<ListarExportarAsignacionDto> Hits { get; set; }
    }
    public class ListarExportarAsignacionDto
    {
        public int idasignacion { get; set; }
        public int idproveedor { get; set; }
        public int idetapa { get; set; }
        public int idtipotransporte { get; set; }
        public int idmoneda { get; set; }
        public int idtipocomprobante { get; set; }
        [Export(Cabecera = "Proveedor", Orden = 1, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string razonsocial { get; set; }
        [Export(Cabecera = "Etapa", Orden = 2, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string etapa { get; set; }
        [Export(Cabecera = "Moneda", Orden = 3, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string moneda { get; set; }
        [Export(Cabecera = "T.Comprobante", Orden = 4, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string tipocomprobante { get; set; }
        [Export(Cabecera = "T.Transporte", Orden = 5, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string tipotransporte { get; set; }
    }
}


