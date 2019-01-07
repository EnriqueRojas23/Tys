
using QueryContracts.Common;
using System.Collections.Generic;
using System;
namespace QueryContracts.TYS.Ordenes.Result
{
    public class ListarHBLResult : QueryResult
    {
        public IEnumerable<ListarHBLDto> Hits { get; set; }
    }
    public class ListarHBLDto
    {
        public string IdOrden { get; set; }
        public string NroOrs32 { get; set; }
        public string NroCon12 { get; set; }
        public string NroDet12 { get; set; }
        public string NroItem13 { get; set; }
        public string Valing32 { get; set; }
        public string Sucursal { get; set; }
        public string Valing32_2 { get; set; }
        public string navvia11 { get; set; }
        public string CodCon { get; set; }
        public string NumeroRuc { get; set; }
        public string Moneda { get; set; }
        public string Opera { get; set; }
        public string Itrm { get; set; }
        public string User { get; set; }
        public string Syst { get; set; }
        public string nav { get; set; }
        public string numvia { get; set; }
        public string MontoTotal { get; set; }
             

   
    }
}
