

using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Ordenes.Parameters;
using QueryContracts.TYS.Ordenes.Result;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace QueryHandlers.TYS.Ordenes
{

    public class ActualizarTablaOfisisQuery : IQueryHandler<ActualizarTablaOfisisParameter>
        {
        public QueryResult Handle(ActualizarTablaOfisisParameter parameters)
            {
                using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("pNroOr32", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.NroOrden);
                    parametros.Add("pValing32", dbType: DbType.Decimal, direction: ParameterDirection.Input, value: parameters.Valing32);
                    parametros.Add("pvaling32_2", dbType: DbType.Decimal, direction: ParameterDirection.Input, value: parameters.Valing32New);

                    var resultado = new ActualizarTablaOfisisResult();
                    resultado = connection.Query<ActualizarTablaOfisisResult>
                            (
                                "ordenes.pa_actualizarHBL",
                                parametros,
                                commandType: CommandType.StoredProcedure
                            ).LastOrDefault();
                    return resultado;
                }
            }
        }
    }
