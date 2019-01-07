using QueryContracts.TYS.Seguimiento.Parameters;
using QueryContracts.TYS.Seguimiento.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceAgents.Common;

namespace Web.TYS.DataAccess.Seguimiento
{
    public class TarifaData
    {
        /// <summary>
        /// Distrito Vs Destinos
        /// </summary>
        /// <param name="idorigen"></param>
        /// <param name="iddestino"></param>
        /// <param name="idcliente"></param>
        /// <param name="idformula"></param>
        /// <param name="idtipotransporte"></param>
        /// <param name="idconceptocobro"></param>
        /// <returns></returns>
        public static List<ListarTarifaOrdenDto> evaluarTarifasOrden_OrigenDistrito(int idorigen, int iddestino, int idcliente, int idformula, int idtipotransporte, int? idconceptocobro)
        {

            //Crear dos metodos y crear dos stores procedures para evaular las que vienen de provincias, neceistan que sean de tipo depar provinci y distrito
            var ubigeo = DataAccess.Seguimiento.SeguimientoData.GetListarUbigeo();

            if (idconceptocobro == 0)
                idconceptocobro = null;
            var parametersaux = new ListarTarifaOrdenParameters
            {
                idorigendistrito = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddistrito,
                iddistrito = iddestino,
                idcliente = idcliente,
                idformula = idformula,
                idtipotransporte = idtipotransporte,
                idconceptocobro = idconceptocobro
            };
            var resultado = (ListarTarifaOrdenResult)parametersaux.Execute();
            if (idconceptocobro == null)
                resultado.Hits = resultado.Hits.Where(x => x.detalle == null).ToList();
            //Traer provincia del distrito

            var provincia = ubigeo.Where(x => x.iddistrito.Equals(iddestino)).FirstOrDefault();
            if (resultado.Hits.ToList().Count == 0)
            {
                parametersaux = new ListarTarifaOrdenParameters
                {
                    idorigendistrito = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddistrito,
                    idprovincia = provincia.idprovincia,
                    idcliente = idcliente,
                    idformula = idformula,
                    idtipotransporte = idtipotransporte,
                    idconceptocobro = idconceptocobro
                };
                resultado = (ListarTarifaOrdenResult)parametersaux.Execute();
                if (idconceptocobro == null)
                    resultado.Hits = resultado.Hits.Where(x => x.detalle == null).ToList();
                resultado.Hits = resultado.Hits.Where(x => x.iddistrito == 0).ToList();
            }
            if (resultado.Hits.ToList().Count == 0)
            {
                var departamento = ubigeo.Where(x => x.idprovincia.Equals(provincia.idprovincia)).FirstOrDefault();
                parametersaux = new ListarTarifaOrdenParameters
                {
                    idorigendistrito = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddistrito,
                    iddepartamento = departamento.iddepartamento,
                    idcliente = idcliente,
                    idformula = idformula,
                    idtipotransporte = idtipotransporte,
                    idconceptocobro = idconceptocobro
                };
                resultado = (ListarTarifaOrdenResult)parametersaux.Execute();
                if (idconceptocobro == null)
                    resultado.Hits = resultado.Hits.Where(x => x.detalle == null).ToList();
                if (resultado.Hits.ToList().Count != 0) return resultado.Hits.Where(x => x.iddistrito == 0 && x.idprovincia == 0).ToList();
            }
            return resultado.Hits.ToList();
        }
        /// <summary>
        /// Provincia Vs Destinos
        /// </summary>
        /// <param name="idorigen"></param>
        /// <param name="iddestino"></param>
        /// <param name="idcliente"></param>
        /// <param name="idformula"></param>
        /// <param name="idtipotransporte"></param>
        /// <param name="idconceptocobro"></param>
        /// <returns></returns>
        public static List<ListarTarifaOrdenDto> evaluarTarifasOrden_OrigenProvincias(int idorigen, int iddestino, int idcliente, int idformula, int idtipotransporte, int? idconceptocobro)
        {

            //Crear dos metodos y crear dos stores procedures para evaular las que vienen de provincias, neceistan que sean de tipo depar provinci y distrito
            var ubigeo = DataAccess.Seguimiento.SeguimientoData.GetListarUbigeo();

            if (idconceptocobro == 0)
                idconceptocobro = null;
            var parametersaux = new ListarTarifaOrdenParameters
            {
                idorigenprovincia = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().idprovincia,
                iddistrito = iddestino,
                idcliente = idcliente,
                idformula = idformula,
                idtipotransporte = idtipotransporte,
                idconceptocobro = idconceptocobro
            };
            var resultado = (ListarTarifaOrdenResult)parametersaux.Execute();
            if (idconceptocobro == null)
                resultado.Hits = resultado.Hits.Where(x => x.detalle == null).ToList();
            //Traer provincia del distrito

            var provincia = ubigeo.Where(x => x.iddistrito.Equals(iddestino)).FirstOrDefault();
            if (resultado.Hits.ToList().Count == 0)
            {
                parametersaux = new ListarTarifaOrdenParameters
                {
                    idorigenprovincia = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().idprovincia,
                    idprovincia = provincia.idprovincia,
                    idcliente = idcliente,
                    idformula = idformula,
                    idtipotransporte = idtipotransporte,
                    idconceptocobro = idconceptocobro
                };
                resultado = (ListarTarifaOrdenResult)parametersaux.Execute();
                if (idconceptocobro == null)
                    resultado.Hits = resultado.Hits.Where(x => x.detalle == null).ToList();
                resultado.Hits = resultado.Hits.Where(x => x.iddistrito == 0).ToList();
            }
            if (resultado.Hits.ToList().Count == 0)
            {
                var departamento = ubigeo.Where(x => x.idprovincia.Equals(provincia.idprovincia)).FirstOrDefault();
                parametersaux = new ListarTarifaOrdenParameters
                {
                    idorigenprovincia = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().idprovincia,
                    iddepartamento = departamento.iddepartamento,
                    idcliente = idcliente,
                    idformula = idformula,
                    idtipotransporte = idtipotransporte,
                    idconceptocobro = idconceptocobro
                };
                resultado = (ListarTarifaOrdenResult)parametersaux.Execute();
                if (idconceptocobro == null)
                    resultado.Hits = resultado.Hits.Where(x => x.detalle == null).ToList();
                if (resultado.Hits.ToList().Count != 0) return resultado.Hits.Where(x => x.iddistrito == 0 && x.idprovincia == 0).ToList();
            }
            return resultado.Hits.ToList();
        }


        /// <summary>
        /// Departamento Vs Destinos
        /// </summary>
        /// <param name="idorigen"></param>
        /// <param name="iddestino"></param>
        /// <param name="idcliente"></param>
        /// <param name="idformula"></param>
        /// <param name="idtipotransporte"></param>
        /// <param name="idconceptocobro"></param>
        /// <returns></returns>
        public static List<ListarTarifaOrdenDto> evaluarTarifasOrden_OrigenDepartamento(int idorigen, int iddestino, int idcliente, int idformula, int idtipotransporte, int? idconceptocobro)
        {

            //Crear dos metodos y crear dos stores procedures para evaular las que vienen de provincias, neceistan que sean de tipo depar provinci y distrito
            var ubigeo = DataAccess.Seguimiento.SeguimientoData.GetListarUbigeo();

            if (idconceptocobro == 0)
                idconceptocobro = null;
            var parametersaux = new ListarTarifaOrdenParameters
            {
                idorigendepartamento = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento,
                iddistrito = iddestino,
                idcliente = idcliente,
                idformula = idformula,
                idtipotransporte = idtipotransporte,
                idconceptocobro = idconceptocobro
            };
            var resultado = (ListarTarifaOrdenResult)parametersaux.Execute();
            if (idconceptocobro == null)
                resultado.Hits = resultado.Hits.Where(x => x.detalle == null).ToList();
            //Traer provincia del distrito

            var provincia = ubigeo.Where(x => x.iddistrito.Equals(iddestino)).FirstOrDefault();
            if (resultado.Hits.ToList().Count == 0)
            {
                parametersaux = new ListarTarifaOrdenParameters
                {
                    idorigendepartamento = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento,
                    idprovincia = provincia.idprovincia,
                    idcliente = idcliente,
                    idformula = idformula,
                    idtipotransporte = idtipotransporte,
                    idconceptocobro = idconceptocobro
                };
                resultado = (ListarTarifaOrdenResult)parametersaux.Execute();
                if (idconceptocobro == null)
                    resultado.Hits = resultado.Hits.Where(x => x.detalle == null).ToList();
                resultado.Hits = resultado.Hits.Where(x => x.iddistrito == 0).ToList();
            }
            if (resultado.Hits.ToList().Count == 0)
            {
                var departamento = ubigeo.Where(x => x.idprovincia.Equals(provincia.idprovincia)).FirstOrDefault();
                parametersaux = new ListarTarifaOrdenParameters
                {
                    idorigendepartamento = ubigeo.Where(x => x.iddistrito.Equals(idorigen)).FirstOrDefault().iddepartamento,
                    iddepartamento = departamento.iddepartamento,
                    idcliente = idcliente,
                    idformula = idformula,
                    idtipotransporte = idtipotransporte,
                    idconceptocobro = idconceptocobro
                };
                resultado = (ListarTarifaOrdenResult)parametersaux.Execute();
                if (idconceptocobro == null)
                    resultado.Hits = resultado.Hits.Where(x => x.detalle == null).ToList();
                if (resultado.Hits.ToList().Count != 0) return resultado.Hits.Where(x => x.iddistrito == 0 && x.idprovincia == 0).ToList();
            }
            return resultado.Hits.ToList();
        }
    }
}