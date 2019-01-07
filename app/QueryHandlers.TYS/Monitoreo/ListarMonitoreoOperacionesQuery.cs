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
    public class ListarMonitoreoOperacionesQuery : IQueryHandler<ListarMonitoreoOperacionesParameters>
    {

        public QueryResult Handle(ListarMonitoreoOperacionesParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idmanifiesto", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idmanifiesto);
       

                var resultado = new ListarMonitoreoOperacionesResults
                {
                    Hits = connection.Query<ListarMonitoreoOperacionesDto>
                    (
                       "monitoreo.pa_listarmonitoreooperaciones",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ),
                };
                return resultado;
            }
        

        }
    }
}
