
using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Pago.Parameters;
using QueryContracts.TYS.Pago.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
namespace QueryHandlers.TYS.Pago
{
    public class ExportarReporteGenercialQuery : IQueryHandler<ExportarReporteGerencialParameters>
    {

        public QueryResult Handle(ExportarReporteGerencialParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idproveedor", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idproveedor);
                parametros.Add("iddestino", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.iddestino);
                parametros.Add("fechaini", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.fechaini);
                parametros.Add("fechafin", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.fechafin);

                var resultado = new ExportarReporteGerencialResult
                {
                    Hits = connection.Query<ExportarReporteGerencialDto>
                        (
                            "pago.pa_listarreportegerencial",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
