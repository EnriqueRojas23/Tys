

using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.Core.NPTP2.Results
{
    public class ListadoViajeResult : QueryResult
    {
        public IEnumerable<ListadoViajeDto> Hits { get; set; }
    }

    public class ListadoViajeDto 
    {
        public string navvia11 { get; set; }
		public string codnav08 { get; set; }
		public string numvia11  { get; set; }
		public string numman11  { get; set; }
        public DateTime? feclle11 { get; set; }
         

    }
}
