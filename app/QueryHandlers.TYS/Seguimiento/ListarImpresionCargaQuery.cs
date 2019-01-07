
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
    public class ListarImpresionCargaQuery : IQueryHandler<ListarImpresionCargaParameters>
    {

        public QueryResult Handle(ListarImpresionCargaParameters parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("numcarga", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numcarga);
                parametros.Add("numhojaruta", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numhojaruta);
                parametros.Add("fecini", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: parameters.fecini);
                parametros.Add("fecfin", dbType: DbType.DateTime, direction: ParameterDirection.Input, value: parameters.fecfin);

                var resultado = new ListarImpresionCargaResult
                {
                    Hits = connection.Query<ListarImpresionCargaDto>
                        (
                            "seguimiento.pa_vistareimpresionxcarga",
                            parametros,
                            commandType: CommandType.StoredProcedure
                        ),
                };

                return resultado;
            }
        }
    }
}

