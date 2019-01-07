using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Monitoreo.Parameters;
using QueryContracts.TYS.Monitoreo.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.TYS.Monitoreo
{
    public class ListarEtapasMonitoreoQuery : IQueryHandler<ListarEtapasMonitoreoParameters>
    {

        public QueryResult Handle(ListarEtapasMonitoreoParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("tipoetapa", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.tipoetapa);

                var resultado = new ListarEtapasMonitoreoResults
                {
                    Hits = connection.Query<ListarEtapasMonitoreoDto>
                    (
                       "monitoreo.pa_listamaestroetapa",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ),
                };
                return resultado;
            }
        

        }
    }
}
