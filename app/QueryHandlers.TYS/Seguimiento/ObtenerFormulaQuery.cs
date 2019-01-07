
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
    public class ObtenerFormulaQuery : IQueryHandler<ObtenerFormulaParameters>
    {

        public QueryResult Handle(ObtenerFormulaParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idformula", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idformula);

                var resultado = new ObtenerFormulaResult();
                resultado = connection.Query<ObtenerFormulaResult>
                        (
                            "seguimiento.pa_obtenerformulas",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
