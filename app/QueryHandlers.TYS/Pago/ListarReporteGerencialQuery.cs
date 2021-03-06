﻿
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
    public class ListarReporteGerencialQuery : IQueryHandler<ListarReporteGerencialParameters>
    {

        public QueryResult Handle(ListarReporteGerencialParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("fechaini", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.fechaini);
                parametros.Add("fechafin", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.fechafin);
                parametros.Add("idproveedor", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idproveedor);
                parametros.Add("iddestino", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.iddestino);

                var resultado = new ListarReporteGerencialResult
                {
                    Hits = connection.Query<ListarReporteGerencialDto>
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
