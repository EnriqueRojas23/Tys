
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
    public class InsertarIncidenciaQuery : IQueryHandler<InsertarIncidenciaParameter>
    {

        public QueryResult Handle(InsertarIncidenciaParameter parameters)
        {
            using (var connection = (SqlConnection)ConnectionFactory.CreateFromUserSession())
            {
               // connection.Open();
                DataTable dt = crear_DataTable("monitoreo.incidencia");

                var objetapas = parameters.Hits;
                foreach (var item in objetapas)
                {
                    DataRow dr = dt.NewRow();
                    //dr["idincidencia"] = item.idincidencia;
                    dr["idmaestroincidencia"] = item.idmaestroincidencia;
                    dr["idordentrabajo"] = item.idordentrabajo;
                    dr["descripcion"] = item.descripcion;
                    dr["observacion"] = item.observacion;
                    dr["fechaincidencia"] = item.fechaincidencia;
                    dr["fecharegistro"] = item.fecharegistro;
                    dr["idusuarioregistro"] = item.idusuarioregistro;
                    dr["activo"] = item.activo;
                    dr["documento"] = item.documento;
                    dr["recurso"] = item.recurso;
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

            DataColumn idmaestroincidencia = new DataColumn();
            idmaestroincidencia.DataType = Type.GetType("System.Int32");
            idmaestroincidencia.ColumnName = "idmaestroincidencia";

            dt.Columns.Add(idmaestroincidencia);

            DataColumn idordentrabajo = new DataColumn();
            idordentrabajo.DataType = Type.GetType("System.Int64");
            idordentrabajo.ColumnName = "idordentrabajo";

            dt.Columns.Add(idordentrabajo);

            DataColumn descripcion = new DataColumn();
            descripcion.DataType = Type.GetType("System.String");
            descripcion.ColumnName = "descripcion";

            dt.Columns.Add(descripcion);

            DataColumn observacion = new DataColumn();
            observacion.DataType = Type.GetType("System.String");
            observacion.ColumnName = "observacion";

            dt.Columns.Add(observacion);

            DataColumn fechaincidencia = new DataColumn();
            fechaincidencia.DataType = Type.GetType("System.DateTime");
            fechaincidencia.ColumnName = "fechaincidencia";

            dt.Columns.Add(fechaincidencia);



            DataColumn fecharegistro = new DataColumn();
            fecharegistro.DataType = Type.GetType("System.DateTime");
            fecharegistro.ColumnName = "fecharegistro";

            dt.Columns.Add(fecharegistro);


            DataColumn idusuarioregistro = new DataColumn();
            idusuarioregistro.DataType = Type.GetType("System.Int32");
            idusuarioregistro.ColumnName = "idusuarioregistro";

            dt.Columns.Add(idusuarioregistro);



            DataColumn activo = new DataColumn();
            activo.DataType = Type.GetType("System.Boolean");
            activo.ColumnName = "activo";
            dt.Columns.Add(activo);


            DataColumn documento = new DataColumn();
            documento.DataType = Type.GetType("System.String");
            documento.ColumnName = "documento";
            dt.Columns.Add(documento);

            DataColumn recurso = new DataColumn();
            recurso.DataType = Type.GetType("System.String");
            recurso.ColumnName = "recurso";

            dt.Columns.Add(recurso);

          

            return dt;
        }
    }
}
