
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
    public class EliminarProveedorClienteQuery : IQueryHandler<EliminarProveedorClienteParameter>
    {

        public QueryResult Handle(EliminarProveedorClienteParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idproveedorcliente", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idproveedorcliente);

                var resultado = new EliminarProveedorClienteResult();
                resultado = connection.Query<EliminarProveedorClienteResult>
                        (
                            "seguimiento.pa_eliminarproveedorcliente",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
