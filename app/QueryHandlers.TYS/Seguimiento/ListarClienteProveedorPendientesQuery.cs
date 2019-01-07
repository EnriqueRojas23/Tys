
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
    public class ListarClienteProveedorPendientesQuery : IQueryHandler<ListarClienteProveedorPendientesParameters>
    {

        public QueryResult Handle(ListarClienteProveedorPendientesParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idcliente", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idcliente);

                var resultado = new ListarClienteProveedorPendientesResult
                {
                    Hits = connection.Query<ListarClienteProveedorPendientesDto>
                        (
                            "seguimiento.pa_listarproveedorescliente",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
