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
    public class ListarTarifaOrdenQuery : IQueryHandler<ListarTarifaOrdenParameters>
    {
        public QueryResult Handle(ListarTarifaOrdenParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idorigendistrito", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idorigendistrito);
                parametros.Add("idorigenprovincia", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idorigenprovincia);
                parametros.Add("idorigendepartamento", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idorigendepartamento);
                
                parametros.Add("idformula", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idformula);
                parametros.Add("idcliente", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idcliente);
                parametros.Add("iddepartamento", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.iddepartamento);
                parametros.Add("idprovincia", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idprovincia);
                parametros.Add("iddistrito", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.iddistrito);
                parametros.Add("idtipotransporte", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idtipotransporte);
                parametros.Add("idconceptocobro", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idconceptocobro);

                var resultado = new ListarTarifaOrdenResult
                {
                    Hits = connection.Query<ListarTarifaOrdenDto>
                        (
                            "seguimiento.pa_traertarifaxorden",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}