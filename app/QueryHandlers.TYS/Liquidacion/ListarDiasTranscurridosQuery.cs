using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Liquidacion.Parameters;
using QueryContracts.TYS.Liquidacion.Results;
using QueryContracts.TYS.Monitoreo.Parameters;
using QueryContracts.TYS.Monitoreo.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.TYS.Liquidacion
{
    public class ListarDiasTranscurridosQuery : IQueryHandler<ListarDiasTranscurridosParameters>
    {

        public QueryResult Handle(ListarDiasTranscurridosParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();


                var resultado = new ListarDiasTranscurridosResults
                {
                    Hits = connection.Query<ListarDiasTranscurridosDto>
                    (
                       "liquidacion.pa_listardiastranscurridos",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ),
                };
                return resultado;
            }
        

        }
    }
}
