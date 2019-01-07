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
    public class DuplicarOTQuery : IQueryHandler<DuplicarOTParameters>
    {

        public QueryResult Handle(DuplicarOTParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idordentrabajo", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idordentrabajo);
                parametros.Add("idusuarioregistro", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idusuarioregistro);
                parametros.Add("bulto", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.cantidad);
                parametros.Add("rechazototal", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.rechazototal);
                parametros.Add("idordentrabajofinal", dbType: DbType.Int64, direction: ParameterDirection.Output, value: parameters.idordentrabajofinal);



                var resultado = new DuplicarOTResults();
                resultado = connection.Query<DuplicarOTResults>
                    (
                       "monitoreo.pa_duplicarordentrabajo",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ).LastOrDefault();
                
                return resultado;
            }
        

        }
    }
}
