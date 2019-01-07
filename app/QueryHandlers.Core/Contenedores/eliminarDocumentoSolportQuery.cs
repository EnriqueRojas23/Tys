
using System.Data;
using System.Data.SqlClient;
using Data.Common;
using QueryContracts.Common;
using QueryContracts.Core.Contenedores.Parameters;
using QueryContracts.Core.Contenedores.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System.Linq;

namespace QueryHandlers.Core.Contenedores
{
    public class EliminarDocumentoSolportQuery : IQueryHandler<EliminarDocumentoSolportParameter>
    {
        public QueryResult Handle(EliminarDocumentoSolportParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("IdReserva", dbType: DbType.Int64, direction: ParameterDirection.Input, value: parameters.IdReserva);
                parametros.Add("NomFichero", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.NombreFichero);


                var resultado = connection.Query<EliminarDocumentoSolportResult>
                        (
                            commandType: CommandType.StoredProcedure,
                            sql: "usp_NOL_EliminarArchivosReserva",
                            param: parametros
                        ).LastOrDefault();

                return resultado;
            }
        }
    }
}
