using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Web.TYS.DataAccess
{
    public sealed class Constantes
    {
        public sealed class Seguridad
        {
            public sealed class Usuario
            {
                public const string listadopedido_filtro_aliasusuario = "ALIAS";
                public const string listadopedido_filtro_nombrecompleto = "NOMBRE";
                public const string listadopedido_filtro_rol = "ROL";
            }

            public sealed class Rol
            {
                public const string listadorol_filtro_nombrerol = "NOMBRE";
            }
        }

        public static string GetModuleAcronym()
        {
            if (ConfigurationManager.AppSettings["ModuleAcronym"] == null) throw new ArgumentException("No esta configurado el ModuleAcronym en el archivo de configuración.");
            var res = Convert.ToString(ConfigurationManager.AppSettings["ModuleAcronym"]);
            return res;
        }

        public enum EstadoOT : int
        {
            PendienteProgramacion = 6,
            PendienteInicioCarga = 7,
            PendienteDespacho = 8,
            PendienteRecepcionIntermedia = 9,
            PendienteRecepcionDestino = 10,
            PendienteEntrega = 11,
            PendienteRetornoDocumentario = 12,
            Facturado = 13,
            PendienteFacturacion = 21,
            Cerrado = 25
        }

        public enum EstadoCarga : int
        {
            Pendiente = 14,
            Confirmada = 15,
            EnRuta = 20
        }

        public enum EstadoVehiculo : int
        {
            Creado = 1,
            Inspeccionado = 2,
            Asignado = 3,
            Inoperativo = 4,
            EnRuta = 19
        }

        public enum EstadoFactura : int
        {
            Pendiente = 22,
            Emitido = 23,
            Anulado = 24
        }

        public enum EstadoDespacho : int
        {
            Creado = 16,
            EnRuta = 17,
            Completado = 18
        }

        public enum TipoOperacion : int
        {
            DistribucionLocal = 111,
            EntregaDirecta = 112,
            DistribucionPorAgencia = 123,
            Transferencia = 124
        }

        public enum TipoTransporte : int
        {
            Aereo = 58,
            Terrestre = 59,
            Fluvial = 60
        }
        public enum SeguimientoFluvial : int
        {
            LlegadaPuerto = 25,
            FechaEmbarque = 26,
            FechaConocimientoEmbarque = 27,
            NumeroConocimientoEmbarque = 28,
            FechaSalidaPuerto = 29,
            FechaArribo = 30,
            FechaDesembarque = 31,
            FechaLlegadaAlmacén = 32,
        }   
        public enum MaestroIncidentes : int
        {
            SeRegistro = 1,
            SePlanifico = 2,
            SeConfirmoCarga = 3,
            SeDespacho = 4,
            DesasocioManifiesto = 5,
            SeLiquido = 13,
            GRTGenerada = 14,
            ManifiestoHR = 15,
            Preliquidacionanulada = 16,
            Comprobanteanulado = 17,
            ComprobanteEliminado = 18,
            ComprobanteGenerado = 19,
            SeElimino  = 33
        }

        public enum MaestroEtapas : int
        {
            TransbordoTerrestre = 1,
            TransbordoAereo = 2,
            TransbordoFluvial = 3,
            ConfirmarRecibo = 4,
            EntregaConforme = 5,
            ControlSUNAT = 6,
            DescargaPuerto = 7,
            EntregaConMercaderiaDañada = 8,
            EntregaConMercaderiaFaltante = 9,
            EntregaRechazoTotal = 10,
            EntregaRechazoParcial = 11,
            NoEntregaLocalCerrado = 12,
            NoEntregaNoexisteladireccióndeentrega = 13,
            AsignaciondeEmbarque = 14,
            FechaZarpe = 15,
            FechaLlegadaPuerto = 16,
            FechaAtraque = 17,
            Llegadaalpunto = 18,
            DesasignacionEmbarque = 19,
            FechaFinCarga = 20,
            ConPreliquidacion = 22
        }

        public enum TipoComprobante : int
        {
            Factura = 81,
            Boleta = 82,
            NotaCredito = 84,
            NotaDebito = 83
        }

        public enum MaestroTablas : int
        {
            Estacion = 1
            ,

            EntregarA = 2
                ,

            CargadaEn = 3
                ,

            ModoTransporte = 4
                ,

            ConceptoCobro = 5
                ,

            Moneda = 6
                ,

            Unidad = 7
                ,

            TipoVehiculo = 8
                ,

            ServicioComplementario = 9
                ,

            Muelle = 10
                ,

            TipoComprobante = 11
                ,

            Etapas = 12
                ,

            NoConforme = 13
                ,

            NoEntrega = 14
                ,

            TipoFacturacion = 15
                ,

            TipoMercaderia = 16
                ,

            ConceptoFormula = 17
                ,

            MarcaVehiculo = 18
                ,

            ModeloVehiculo = 19
                ,

            Combustible = 20
                ,

            ColorVehiculo = 21
                ,

            Vehiculo = 22
                ,

            TipoOperacion = 23
                ,

            Agencia = 24
                ,

            OT = 25
                ,

            DescripcionGRR = 28
              ,

            VehiculoFluvial = 29
                ,

            Puerto = 30
                ,

            Preliquidacion = 31
                , DocumentoAjuste = 32
            ,MotivoAnulacion = 34
        }

        public enum EstadoPreliquidacion
        {
            PendienteFactura = 22
            ,

            Facturado = 23
                , Anulado = 24
        }
    }
}