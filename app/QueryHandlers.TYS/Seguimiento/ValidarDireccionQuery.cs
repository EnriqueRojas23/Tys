
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
    public class ValidarDireccionQuery : IQueryHandler<ValidarDireccionParameter>
    {

        public QueryResult Handle(ValidarDireccionParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idcliente", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idcliente);
                parametros.Add("codigo", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.codigo);

                var resultado = new ValidarDireccionResult();
                resultado = connection.Query<ValidarDireccionResult>
                        (
                            "seguimiento.pa_validadireccion",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
