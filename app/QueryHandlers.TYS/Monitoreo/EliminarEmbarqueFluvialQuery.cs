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
    public class EliminarEmbarqueFluvialQuery : IQueryHandler<EliminarEmbarqueFluvialParameters>
    {

        public QueryResult Handle(EliminarEmbarqueFluvialParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idembarque", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idembarque);


                var resultado = new EliminarEmbarqueFluvialResults();
                resultado =  connection.Query<EliminarEmbarqueFluvialResults>
                    (
                       "monitoreo.pa_eliminarembarque",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ).LastOrDefault();
                
                return resultado;
            }
        

        }
    }
}
