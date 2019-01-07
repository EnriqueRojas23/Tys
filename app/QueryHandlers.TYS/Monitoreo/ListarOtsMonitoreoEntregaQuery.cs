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
    public class ListarOtsMonitoreoEntregaQuery : IQueryHandler<ListarOtsMonitoreoEntregaParameters>
    {

        public QueryResult Handle(ListarOtsMonitoreoEntregaParameters parameters)
        {
            using(var connection = (SqlConnection)ConnectionFactory.CreateFromSecuritySession())
            {
                var parametros = new DynamicParameters();
                parametros.Add("idcliente", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idcliente);
                parametros.Add("iddestino", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.iddestino);
                parametros.Add("numcp", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.numcp);
                parametros.Add("nummanifiesto", dbType: DbType.String, direction: ParameterDirection.Input, value: parameters.nummanifiesto);
                parametros.Add("idestado", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idestado);
                parametros.Add("idtipotransporte", dbType: DbType.Int32, direction: ParameterDirection.Input, value: parameters.idtipotransporte);

                var resultado = new ListarOtsMonitoreoEntregaResults
                {
                    Hits = connection.Query<ListarOtsMonitoreoEntregaDto>
                    (
                       "monitoreo.pa_listarotparamonitoreo_entrega",
                       parametros,
                       commandType: CommandType.StoredProcedure
                    ),
                };
                return resultado;
            }
        

        }
    }
}
