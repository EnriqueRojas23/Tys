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
    public class ObtenerEmbarqueFluvialQuery : IQueryHandler<ObtenerEmbarqueFluvialParameters>
    {

        public QueryResult Handle(ObtenerEmbarqueFluvialParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idembarque", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idembarque);


                var resultado = new ObtenerEmbarqueFluvialResults();
                resultado =  connection.Query<ObtenerEmbarqueFluvialResults>
                    (
                       "monitoreo.pa_obtenerembarque",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ).LastOrDefault();
                
                return resultado;
            }
        

        }
    }
}
