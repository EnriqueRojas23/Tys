
using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Monitoreo.Parameters;
using QueryContracts.TYS.Monitoreo.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace QueryHandlers.TYS.Monitoreo
{
    public class ValidarOtsCerradasQuery : IQueryHandler<ValidarOtsCerradasParameter>
    {

        public QueryResult Handle(ValidarOtsCerradasParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idmanfiesto", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.idmanifiesto);
                    
                var resultado = new ValidarOtsCerradasResult();
                resultado = connection.Query<ValidarOtsCerradasResult>
                        (
                            "monitoreo.pa_validarotscerradas",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
