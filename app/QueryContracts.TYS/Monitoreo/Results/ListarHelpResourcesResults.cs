
using QueryContracts.Common;
using System;
using System.Collections.Generic;
namespace QueryContracts.TYS.Monitoreo.Results
{
    public class ListarHelpResourcesResults : QueryResult
    {
        public IEnumerable<ListarHelpResourcesDto> Hits { get; set; }
        
    }
    public class ListarHelpResourcesDto
    {
        public int idhelp { get; set; }
        public string help { get; set; }

    }
}
