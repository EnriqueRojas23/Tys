

using QueryContracts.Common;
using System;
namespace QueryContracts.TYS.Monitoreo.Parameters
{
    public class ListarEmbarqueFluvialParameters : QueryParameter
    {

        public string numeroembarque { get; set; }
        public int? idvehiculo { get; set; }

    }
}
