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
    public class ObtenerExisteCEQuery : IQueryHandler<ObtenerExisteCEParameters>
    {

        public QueryResult Handle(ObtenerExisteCEParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("ce", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.ce);
                parametros.Add("idtransporte", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idtransporte);
                parametros.Add("idembarque", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idembarque);


                var resultado = new ObtenerExisteCEResults();
                resultado = connection.Query<ObtenerExisteCEResults>
                    (
                       "monitoreo.pa_existece",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ).LastOrDefault();
                
                return resultado;
            }
        

        }
    }
}
