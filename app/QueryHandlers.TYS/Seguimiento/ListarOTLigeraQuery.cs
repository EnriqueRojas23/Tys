
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
    public class ListarOTLigeraQuery : IQueryHandler<ListarOTLigeraParameters>
    {

        public QueryResult Handle(ListarOTLigeraParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idcliente", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idcliente);
                parametros.Add("numcp", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numcp);
                parametros.Add("devolucion", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.devolucion);

                var resultado = new ListarOTLigeraResult
                {
                    Hits = connection.Query<ListarOTLigeraDto>
                        (
                            "seguimiento.pa_listarordentrabajorapida",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
