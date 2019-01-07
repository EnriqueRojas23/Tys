using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Monitoreo.Parameters;
using QueryContracts.TYS.Monitoreo.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace QueryHandlers.TYS.Monitoreo
{
    public class ListarManifiestosSinEmbarqueQuery : IQueryHandler<ListarManifiestosSinEmbarqueParameters>
    {

        public QueryResult Handle(ListarManifiestosSinEmbarqueParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idembarque", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idembarque);
                var resultado = new ListarManifiestosSinEmbarqueResults
                {
                    Hits = connection.Query<ListarManifiestosSinEmbarqueDto>
                    (
                       "monitoreo.pa_listamanifiestosinembarque",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ),
                };
                return resultado;
            }
        

        }
    }
}
