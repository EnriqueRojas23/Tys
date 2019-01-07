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
    public class ListarLiquidacionOperacionesQuery : IQueryHandler<ListarLiquidacionOperacionesParameters>
    {

        public QueryResult Handle(ListarLiquidacionOperacionesParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("fecini", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.fechainicio);
                parametros.Add("fecfin", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.fechafin);
                parametros.Add("idcliente", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.idcliente);
                parametros.Add("iddestinatario", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.iddestinatario);
                parametros.Add("numcp", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numcp);
                parametros.Add("grr", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.grr);
                parametros.Add("diastranscurridos", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.diastranscurridos);

                var resultado = new ListarLiquidacionOperacionesResults
                {
                    Hits = connection.Query<ListarLiquidacionOperacionesDto>
                    (
                       "liquidacion.pa_listarliquidaciondocumentaria",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ),
                };
                return resultado;
            }
        

        }
    }
}
