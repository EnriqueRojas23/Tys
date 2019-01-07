using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Contenedores.Parameters;
using QueryContracts.TYS.Contenedores.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.TYS.Contenedores
{
    public class ObtenerBookingAuxQuery : IQueryHandler<ObtenerBookingAuxParameter>
    {
        public QueryResult Handle(ObtenerBookingAuxParameter parameters)
        {
            if (parameters == null) throw new ArgumentException("Se requiere el parametro");
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("rb_int_identificador_terminal", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.rb_int_identificador_terminal);

                var resultado = connection.Query<ObtenerBookingAuxResult>
                         (
                             commandType: CommandType.StoredProcedure,
                             sql: "contenedor.pa_listaraux",
                             param: parametros
                         ).LastOrDefault();

                return resultado;
            }
        }
    }
}
