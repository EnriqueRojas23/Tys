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
    public class ListarOtsFluvialQuery : IQueryHandler<ListarOtsFluvialParameters>
    {

        public QueryResult Handle(ListarOtsFluvialParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idcliente", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idcliente);
                parametros.Add("iddestino", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.iddestino);
                parametros.Add("idestado", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idestado);
                parametros.Add("numcp", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numcp);
                parametros.Add("numhojaruta", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numhojaruta);
                parametros.Add("nummanifiesto", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.nummanifiesto);
                parametros.Add("documento", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.documento);

                var resultado = new ListarOtsFluvialResults
                {
                    Hits = connection.Query<ListarOtsFluvialDto>
                    (
                       "monitoreo.pa_listarotparamonitoreofluvial",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ),
                };
                return resultado;
            }
        

        }
    }
}
