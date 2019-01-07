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
    public class ObtenerUltimaHojaRutaQuery : IQueryHandler<ObtenerUltimaHojaRutaParameter>
    {

        public QueryResult Handle(ObtenerUltimaHojaRutaParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                //parametros.Add("idorden", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idorden);

                var resultado = new ObtenerUltimaHojaRutaResult();
                resultado = connection.Query<ObtenerUltimaHojaRutaResult>
                        (
                            "seguimiento.pa_obtenerultimohojaruta",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
