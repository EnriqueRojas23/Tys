using System;
using System.Collections.Generic;
using System.Linq;
using ServiceAgents.Common;
using AutoMapper;
using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;

namespace Web.TYS.DataAccess.Facturacion
{
    using Web.TYS.Areas.Facturacion.Models;
    using Web.TYS.Areas.Seguimiento.Models.Seguimiento;
    using CommandContracts.TYS.Facturacion;
    using CommandContracts.TYS.Facturacion.Output;
    using QueryContracts.TYS.Facturacion.Results;
    using QueryContracts.TYS.Facturacion.Parameters;
    using System.Configuration;

    public class FacturacionData
    {
        public IEnumerable<PreliquidacionModel> GetListarPendientePreliquidacion(int? idcliente, int? iddestino, string numcp)
        {
            var parametros = new ListarPendientePreliquidacionParameters { idcliente = idcliente, iddestino = iddestino, numcp = numcp };
            var result = (ListarPendientePreliquidacionResult)parametros.Execute();

            Mapper.CreateMap<ListarPendientePreliquidacionDto, PreliquidacionModel>();
            return Mapper.Map<IEnumerable<ListarPendientePreliquidacionDto>, IEnumerable<PreliquidacionModel>>(result.Hits);
        }

        public IEnumerable<ListarDocumentosDto> GetListarDocumentos(int? idtipocomprobante, int? idusuario, int? idestacionorigen)
        {
            var parametros = new ListarDocumentosParameters
            {
                idtipocomprobante = idtipocomprobante
                ,
                idusuario = idusuario
                ,
                idestacionorigen = idestacionorigen
            };
            var result = (ListarDocumentosResult)parametros.Execute();
            return result.Hits;
        }

        public IEnumerable<PreliquidacionModel> GetListarCompletadoPreliquidacion(long idpreliquidacion)
        {
            var parametros = new ListarCompletadoPreliquidacionParameters { idpreliquidacion = idpreliquidacion };
            var result = (ListarCompletadoPreliquidacionResult)parametros.Execute();

            Mapper.CreateMap<ListarCompletadoPreliquidacionDto, PreliquidacionModel>();
            return Mapper.Map<IEnumerable<ListarCompletadoPreliquidacionDto>, IEnumerable<PreliquidacionModel>>(result.Hits);
        }

        public long? InsertarActualizarPreliquidacion(PreliquidacionModel model)
        {
            Mapper.CreateMap<PreliquidacionModel, InsertarActualizarPreliquidacionCommand>();
            var comando = Mapper.Map<PreliquidacionModel, InsertarActualizarPreliquidacionCommand>(model);
            var result = (InsertarActualizarPreliquidacionOutput)comando.Execute();
            return result.idpreliquidacion;
        }

        public long InsertarActualizarPreliquidacionDetalle(PreliquidacionModel model)
        {
            var comando = new InsertarActualizarDetalleComprobanteCommand
            {
                idcomprobantepago = model.idcomprobantepago.Value,
                idordentrabajo = model.idordentrabajo,
                igv = model.igv,
                subtotal = model.subtotal,
                total = model.total,
                recargo = model.recargo
            };
            var result = (InsertarActualizarDetalleComprobanteOutput)comando.Execute();
            return result.iddetallecomprobante;
        }

        internal List<OrdenTrabajoModel> GetListarOTS(string ordenes)
        {
            var parameters = new ListarOTSxIdsParameters { ots = ordenes };
            var result = (ListarOTSxIdsResult)parameters.Execute();

            Mapper.CreateMap<ListarOTSxIdsDto, OrdenTrabajoModel>();
            return Mapper.Map<IEnumerable<ListarOTSxIdsDto>, List<OrdenTrabajoModel>>(result.Hits);
        }

        public static string ObtenerUltimaPreliquidacion()
        {
            var parameters = new ObtenerUltimaPreliquidacionParameters { };
            var resultado = (ObtenerUltimaPreliquidacionResult)parameters.Execute();
            if (resultado == null)
                return ConfigurationManager.AppSettings["numliq"].ToString();
            else return resultado.numeropreliquidacion.Split('-')[0].ToString() + "-" + (Convert.ToInt32(resultado.numeropreliquidacion.Split('-')[1].ToString()) + 1).ToString().PadLeft(6, '0');
        }

        internal void ActualizarOTS(string ordenes, long? idpreliquidacion)
        {
            var parameters = new VincularPreliquidacionOTParameters { idsordentrabajo = ordenes, idpreliquidacion = idpreliquidacion };
            parameters.Execute();
        }
        public IEnumerable<ComprobanteDetalleModel> GetListarComprobanteDetalles(long idcomprobante)
        {
            var parametros = new ListarComprobanteDetallesParameters
            {
                idcomprobante = idcomprobante
               
            };

            var result = (ListarComprobanteDetallesResult)parametros.Execute();
            Mapper.CreateMap<ListarComprobanteDetallesDto, ComprobanteDetalleModel>();
            return Mapper.Map<IEnumerable<ListarComprobanteDetallesDto>, IEnumerable<ComprobanteDetalleModel>>(result.Hits);
        }
        public IEnumerable<ComprobanteModel> GetListarComprobantes(int? idcliente, int? idestado, string numerocomprobante
            , string numeroliquidacion, int? idtipocomprobante   , string fecinicio  , string fecfin )
        {
            var parametros = new ListarComprobantesParameters
            {
                idcliente = idcliente
                ,
                idestado = idestado
                ,
                numerocomprobante = numerocomprobante
                ,
                idtipocomprobante = idtipocomprobante
                ,
                numeroliquidacion = numeroliquidacion
                ,fecfin = fecfin
                ,fecinicio = fecinicio
            };

            var result = (ListarComprobantesResult)parametros.Execute();
            Mapper.CreateMap<ListarComprobantesDto, ComprobanteModel>();
            return Mapper.Map<IEnumerable<ListarComprobantesDto>, IEnumerable<ComprobanteModel>>(result.Hits);
        }

        public IEnumerable<ComprobanteModel> GetListarPreliquidacion(int? idcliente, int? idestado, string numerocomprobante, string numeroliquidacion, int? idtipocomprobante)
        {
            var parametros = new ListarPreliquidacionParameters
            {
                idcliente = idcliente
                ,
                idestado = idestado
                ,
                numerocomprobante = numerocomprobante
                ,
                idtipocomprobante = idtipocomprobante
                ,
                numeroliquidacion = numeroliquidacion
            };

            var result = (ListarPreliquidacionResult)parametros.Execute();
            Mapper.CreateMap<ListarPreliquidacionDto, ComprobanteModel>();
            return Mapper.Map<IEnumerable<ListarPreliquidacionDto>, IEnumerable<ComprobanteModel>>(result.Hits);
        }

        internal static long GenerarDetalleComprobante(ComprobanteDetalleModel model)
        {
            Mapper.CreateMap<ComprobanteDetalleModel, InsertarActualizarDetalleComprobanteCommand>();
            var comando = Mapper.Map<ComprobanteDetalleModel, InsertarActualizarDetalleComprobanteCommand>(model);
            var result = (InsertarActualizarDetalleComprobanteOutput)comando.Execute();
            return result.iddetallecomprobante;
        }

        internal static long GenerarComprobante(ComprobanteModel model)
        {
            Mapper.CreateMap<ComprobanteModel, InsertarActualizarComprobanteCommand>();
            var comando = Mapper.Map<ComprobanteModel, InsertarActualizarComprobanteCommand>(model);
            var result = (InsertarActualizarComprobanteOutput)comando.Execute();
            return result.idcomprobantepago;
        }

        public IEnumerable<PreliquidacionModel> ObtenerPreliquidacion(long idpreliquidacion)
        {
            var parameters = new ObtenerPreliquidacionParameters { idpreliquidacion = idpreliquidacion };
            try
            {
                var result = (ObtenerPreliquidacionResult)parameters.Execute();
                Mapper.CreateMap<ObtenerPreliquidacionDto, PreliquidacionModel>();
                return Mapper.Map<IEnumerable<ObtenerPreliquidacionDto>, IEnumerable<PreliquidacionModel>>(result.Hits);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal long AgregarRecargo(PreliquidacionModel model)
        {
            var comando = new AgregarRecargoCommand { idordentrabajo = model.idordentrabajo, recargo = model.recargo.Value };
            var result = (InsertarActualizarPreliquidacionOutput)comando.Execute();
            return result.idordentrabajo;
        }

        internal ComprobanteModel ObtenerComprobante(long? idpreliquidacion, long? idcomprobante)
        {
            var parameters = new ObtenerComprobanteParameters { idpreliquidacion = idpreliquidacion, idcomprobante = idcomprobante };
            var result = (ObtenerComprobanteResult)parameters.Execute();

            Mapper.CreateMap<ObtenerComprobanteResult, ComprobanteModel>();
            return Mapper.Map<ObtenerComprobanteResult, ComprobanteModel>(result);
        }
        internal ComprobanteModel ObtenerFacturaElectronica(long idcomprobante)
        {
            var parameters = new ObtenerFactElectronicaParameters {  DocEnty = idcomprobante };
            var result = (ObtenerFactElectronicaResult)parameters.Execute();

            Mapper.CreateMap<ObtenerFactElectronicaResult, ComprobanteModel>();
            return Mapper.Map<ObtenerFactElectronicaResult, ComprobanteModel>(result);
        }

        internal void EliminarComprobante(long idcomprobante)
        {
            var command = new EliminarComprobanteCommand { idcomprobante = idcomprobante };
            command.Execute();
        }

        internal void AnularComprobante(long idcomprobante)
        {
            var command = new InsertarActualizarComprobanteCommand
            {
                idcomprobantepago = idcomprobante
                ,
                idestado = (Int32)Constantes.EstadoFactura.Anulado
                ,
                _tipoop = 2
            };
            command.Execute();
        }

        //internal void

        internal ObtenerNumeroComprobanteResult obtenerNumeroComprobante(int? idusuario, int? idtipocomprobante, int? idestacionorigen, int? idnumerodocumento)
        {
            var parameter = new ObtenerNumeroComprobanteParameters
            {
                idusuario = idusuario,
                idtipocomprobante = idtipocomprobante,
                idestacionorigen = idestacionorigen,
                idnumerodocumento = idnumerodocumento
            };
            var result = (ObtenerNumeroComprobanteResult)parameter.Execute();
            return result;
        }

        internal void actualizarNumeroComprobante(int idtipocomprobante, string numerocomprobante, int idnumerocomprobante)
        {
            var command = new InsertarActualizarDocumentoCommand
            {
                idnumerodocumento = idnumerocomprobante,
                ultimonumero = numerocomprobante,
                _tipooperacion = 1
            };
            command.Execute();
        }

        internal void ActualizarComprobanteOTS(string ordenes, DateTime? fechacomprobante)
        {
            var parameters = new ActualizarOTComprobanteParameters
            {
                idsordentrabajo = ordenes
                ,
                fechacomprobante = fechacomprobante
            };
            parameters.Execute();
        }

        internal void ActualizarMontosOT(long idordentrabajo, decimal? subtotal, decimal? total, decimal? igv)
        {
            var command = new ActualizarMontosOTCommand
            {
                idordentrabajo = idordentrabajo,
                igv = igv,
                total = total,
                subtotal = subtotal
            };
            command.Execute();
        }

        internal void InsertarActualizarDocumento(DocumentoModel model)
        {
            var parameters = new InsertarActualizarDocumentoCommand
            {
                idnumerodocumento = model.idnumerodocumento,
                ultimonumero = model.ultimonumero,
                idestacion = model.idestacion,
                serie = model.serie,
                primernumero = model.primernumero,
                idusuarioautorizado = model.idusuarioautorizado,
                idtipocomprobante = model.idtipocomprobante,
                _tipooperacion = 0
            };
            parameters.Execute();
        }
    }
}