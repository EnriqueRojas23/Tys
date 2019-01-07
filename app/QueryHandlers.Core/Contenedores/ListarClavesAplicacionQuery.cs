
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using Componentes.Common.Extensions;
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Core.Contenedores.Parameters;
using QueryContracts.Core.Contenedores.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;

namespace QueryHandlers.Core.Contenedores
{
    public class ListarClavesAplicacionQuery : IQueryHandler<ListarClavesAplicacionParameter>
    {
        public QueryResult Handle(ListarClavesAplicacionParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("tipova", dbType: DbType.String, direction: ParameterDirection.Input, value: StringEnum.GetStringValue(parameters.TipoValorApp));

                var resultado = new ListarClavesAplicacionResult
                {
                    Hits = connection.Query<ListarClavesAplicacionDto>
                        (
                            commandType: CommandType.StoredProcedure,
                            param: parametros,
                            sql: "sp_nol_listar_claves_valor_app"
                        )
                };
                return resultado;
            }
        }
    }
}
