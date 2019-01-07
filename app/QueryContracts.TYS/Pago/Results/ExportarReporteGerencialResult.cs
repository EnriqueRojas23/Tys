using Componentes.Common.CustomAttributes;
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Pago.Results
{
    public class ExportarReporteGerencialResult : QueryResult
    {
        public IEnumerable<ExportarReporteGerencialDto> Hits { get; set; }
    }
    public class ExportarReporteGerencialDto
    {
        [Export(Cabecera = "Proveedor", Orden = 1, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string proveedor { get; set; }
        [Export(Cabecera = "Etapa", Orden = 2, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string etapa { get; set; }
        [Export(Cabecera = "Destino", Orden = 3, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string destino { get; set; }
        [Export(Cabecera = "Costo", Orden = 4, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string costo { get; set; }
        [Export(Cabecera = "KG", Orden = 5, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string kgs { get; set; }
        [Export(Cabecera = "Costo Unitario", Orden = 6, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string cu { get; set; }
        [Export(Cabecera = "Producción", Orden = 7, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string produccion { get; set; }
        [Export(Cabecera = "Rentabilidad", Orden = 8, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string rentabilidad { get; set; }
        [Export(Cabecera = "Porcentaje", Orden = 9, Tamanio = 25, TipoExportacion = TipoExportacion.ExcelSimple)]
        public string porcentaje { get; set; }

    }
}


