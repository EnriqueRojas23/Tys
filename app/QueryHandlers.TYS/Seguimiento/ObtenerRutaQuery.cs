﻿
using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Seguimiento.Parameters;
using QueryContracts.TYS.Seguimiento.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace QueryHandlers.TYS.Seguimiento
{
    public class ObtenerRutaQuery : IQueryHandler<ObtenerRutaParameters>
    {

        public QueryResult Handle(ObtenerRutaParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idruta", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idruta);

                var resultado = new ObtenerRutaResult();
                resultado = connection.Query<ObtenerRutaResult>
                        (
                            "seguimiento.pa_obtenerruta",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
