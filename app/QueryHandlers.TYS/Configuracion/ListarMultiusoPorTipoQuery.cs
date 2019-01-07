
using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Configuracion.Parameters;
using QueryContracts.TYS.Configuracion.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;

namespace QueryHandlers.TYS.Configuracion
{
    public class ListarMultiusoPorTipoQuery : IQueryHandler<ListarMultiusoPorTipoParameter>
    {
        public QueryResult Handle(ListarMultiusoPorTipoParameter parameters)
        {
            if (parameters == null) throw new ArgumentException("Se requiere el parametro");

            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("mlt_str_tipo", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.mlt_str_tipo);

                var resultado = new ListarMultiusoPorTipoResult
                {
                    Hits = connection.Query<ListarMultiusoPorTipoDto>
                        (
                            commandType: CommandType.StoredProcedure,
                            sql: "configuracion.pa_listar_multiuso_tipo",
                            param: parametros
                        )
                };

                return resultado;
            }
        }
    }
}
