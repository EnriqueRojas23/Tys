

namespace QueryHandlers.TYS.Account
{
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    using Data.Common;

    using QueryContracts.Common;
    using QueryContracts.TYS.Account.Parameters;
    using QueryContracts.TYS.Account.Results;

    using QueryHandlers.Common;
    using QueryHandlers.Common.Dapper;

    public class ValidarUsuarioQuery : IQueryHandler<ValidarUsuarioParameter>
    {
        public QueryResult Handle(ValidarUsuarioParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("usr_str_red", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.Usr_str_red);
                parametros.Add("usr_str_password", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.Usr_str_password);

                var resultado = new ValidarUsuarioResult();
                resultado = connection.Query<ValidarUsuarioResult>
                        (
                            "seguridad.sp_validarusuario",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
