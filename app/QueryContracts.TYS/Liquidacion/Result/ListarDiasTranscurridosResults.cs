

using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Liquidacion.Results
{
    public class ListarDiasTranscurridosResults : QueryResult
    {
        public IEnumerable<ListarDiasTranscurridosDto> Hits { get; set; }
        
    }
    public class ListarDiasTranscurridosDto
    {
        public int DiasTranscurridos { get; set; }
       
       
    }
}
