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
    public class ListarOrdenesxManifiestoQuery : IQueryHandler<ListarOrdenesxManifiestoParameters>
    {

        public QueryResult Handle(ListarOrdenesxManifiestoParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idmanifiesto", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idmanifiesto);

                var resultado = new ListarOrdenesxManifiestoResults
                {
                    Hits = connection.Query<ListarOrdenesxManifiestoDto>
                    (
                       "monitoreo.pa_listarordenesxmanifiesto",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ),
                };
                return resultado;
            }
        

        }
    }
}
