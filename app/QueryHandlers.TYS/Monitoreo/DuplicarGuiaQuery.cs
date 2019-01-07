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
    public class DuplicarGuiaQuery : IQueryHandler<DuplicarGuiaParameters>
    {

        public QueryResult Handle(DuplicarGuiaParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idordentrabajo", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idordentrabajo);
                parametros.Add("idguiaremisioncliente", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idguiaremisioncliente);
                parametros.Add("rechazototal", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.rechazototal);
                parametros.Add("idordennueva", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idordennueva);


                var resultado = new DuplicarGuiaResults();
                resultado = connection.Query<DuplicarGuiaResults>
                    (
                       "monitoreo.pa_duplicarguiaremisionremitente",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ).LastOrDefault();
                
                return resultado;
            }
        

        }
    }
}
