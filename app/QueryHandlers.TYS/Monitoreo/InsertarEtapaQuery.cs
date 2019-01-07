
using Data.Common;
using QueryContracts.Common;
using QueryContracts.TYS.Monitoreo.Parameters;
using QueryContracts.TYS.Monitoreo.Results;
using QueryHandlers.Common;
using QueryHandlers.Common.Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace QueryHandlers.TYS.Monitoreo
{
    public class InsertarEtapaQuery : IQueryHandler<InsertarEtapaParameter>
    {

        public QueryResult Handle(InsertarEtapaParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
               // connection.Open();
                DataTable dt = crear_DataTable("monitoreo.etapa");

                var objetapas = parameters.Hits;
                foreach (var item in objetapas)
                {
                    DataRow dr = dt.NewRow();
                    dr["idmaestroetapa"] = item.idmaestroetapa;
                    dr["idordentrabajo"] = item.idordentrabajo;
                    dr["descripcion"] = item.descripcion;
                    dr["recurso"] = item.recurso;
                    dr["documento"] = item.documento;
                    dr["fechaetapa"] = item.fechaetapa;
                    dr["fecharegistro"] = item.fecharegistro;
                    dr["idusuarioregistro"] = item.idusuarioregistro;
                    dr["visible"] = item.visible;
                    dt.Rows.Add(dr);
                }

                using (SqlBulkCopy s = new SqlBulkCopy(connection))
                {
                    s.DestinationTableName = dt.TableName;

                    foreach (var column in dt.Columns)
                    {
                        s.ColumnMappings.Add(column.ToString(), column.ToString());
                    }

                    s.WriteToServer(dt);
                }
                var result = new InsertarEtapaResult { respuesta = true };
                return result;
            }
        }
        private static DataTable crear_DataTable(string tabla)
        {

            DataTable dt = new DataTable(tabla);

            DataColumn idmaestroetapa = new DataColumn();
            idmaestroetapa.DataType = Type.GetType("System.Int32");
            idmaestroetapa.ColumnName = "idmaestroetapa";

            dt.Columns.Add(idmaestroetapa);

            DataColumn idordentrabajo = new DataColumn();
            idordentrabajo.DataType = Type.GetType("System.Int64");
            idordentrabajo.ColumnName = "idordentrabajo";

            dt.Columns.Add(idordentrabajo);

            DataColumn descripcion = new DataColumn();
            descripcion.DataType = Type.GetType("System.String");
            descripcion.ColumnName = "descripcion";

            dt.Columns.Add(descripcion);

            DataColumn recurso = new DataColumn();
            recurso.DataType = Type.GetType("System.String");
            recurso.ColumnName = "recurso";

            dt.Columns.Add(recurso);

            DataColumn documento = new DataColumn();
            documento.DataType = Type.GetType("System.String");
            documento.ColumnName = "documento";

            dt.Columns.Add(documento);


            DataColumn fechaetapa = new DataColumn();
            fechaetapa.DataType = Type.GetType("System.DateTime");
            fechaetapa.ColumnName = "fechaetapa";

            dt.Columns.Add(fechaetapa);

            DataColumn fecharegistro = new DataColumn();
            fecharegistro.DataType = Type.GetType("System.DateTime");
            fecharegistro.ColumnName = "fecharegistro";

            dt.Columns.Add(fecharegistro);


            DataColumn idusuarioregistro = new DataColumn();
            idusuarioregistro.DataType = Type.GetType("System.Int32");
            idusuarioregistro.ColumnName = "idusuarioregistro";


            dt.Columns.Add(idusuarioregistro);

            DataColumn visible = new DataColumn();
            visible.DataType = Type.GetType("System.Boolean");
            visible.ColumnName = "visible";

            dt.Columns.Add(visible);

            return dt;
        }
    }
}
