
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
    public class ListarConceptosTarifaQuery : IQueryHandler<ListarConceptosTarifaParameters>
    {

        public QueryResult Handle(ListarConceptosTarifaParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idorigen", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idorigen);
                parametros.Add("idcliente", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idcliente);
                parametros.Add("idformula", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idformula);
                parametros.Add("idtipotransporte", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idtipotransporte);
                parametros.Add("iddepartamento", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.iddepartamento);
                parametros.Add("idprovincia", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idprovincia);
                parametros.Add("iddistrito", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.iddistrito);


                var resultado = new ListarConceptosTarifaResult
                {
                    Hits = connection.Query<ListarConceptosTarifaDto>
                        (
                            "seguimiento.pa_listarconceptostarifas",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}
