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
    public class ListarOtsMonitoreoQuery : IQueryHandler<ListarOtsMonitoreoParameters>
    {

        public QueryResult Handle(ListarOtsMonitoreoParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idcliente", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idcliente);
                parametros.Add("iddestino", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.iddestino);
                parametros.Add("numcp", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numcp);
                parametros.Add("numhojaruta", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numhojaruta);
                parametros.Add("nummanifiesto", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.nummanifiesto);
                parametros.Add("grr", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.grr);
                parametros.Add("documento", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.documento);
                parametros.Add("codtienda", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.tienda);

                var resultado = new ListarOtsMonitoreoResults
                {
                    Hits = connection.Query<ListarOtsMonitoreoDto>
                    (
                       "monitoreo.pa_listarotparamonitoreo",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ),
                };
                return resultado;
            }
        

        }
    }
}
