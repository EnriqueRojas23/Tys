
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
    public class ListarOTHijasCCQuery : IQueryHandler<ListarOTHijasCCParameters>
    {

        public QueryResult Handle(ListarOTHijasCCParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idcamioncompleto", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idcamioncompleto);

                var resultado = new ListarOTHijasCCResult
                {
                    Hits = connection.Query<ListarOTHijasCCDto>
                        (
                            "seguimiento.pa_listarordeneshijascc",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
