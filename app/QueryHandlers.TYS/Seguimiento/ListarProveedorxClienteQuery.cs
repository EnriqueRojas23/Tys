
using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Seguimiento.Parameters;
using QueryContracts.TYS.Seguimiento.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
namespace QueryHandlers.TYS.Seguimiento
{
    public class ListarProveedorxClienteQuery : IQueryHandler<ListarProveedorxClienteParameters>
    {

        public QueryResult Handle(ListarProveedorxClienteParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idcliente", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idcliente);

                var resultado = new ListarProveedorxClienteResult
                {
                    Hits = connection.Query<ListarProveedorxClienteDto>
                        (
                            "seguimiento.pa_listarproveedorxcliente",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
