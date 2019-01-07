using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public sealed class ConstantesNegocio
    {
        public ConstantesNegocio()
        {

        }

        public static readonly string SESION_ENTIDAD_USUARIO = "SESION_ENTIDAD_USUARIO";
        public static readonly string TIPO_DOCUMENTO_RUC = "RUC";
        public static readonly string TIPO_DOCUMENTO_USERDB = "USERDB";
        public static readonly string NO = "NO";
        public static readonly string SI = "SI";
        public static readonly string SELECCION_ELIJAOPCION = "-1";
        public static readonly string SESION_USUARIOS_VOL = "SESION_USUARIOS_VOL";
        public static readonly string SESION_USUARIOS_SIN = "SESION_USUARIOS_SIN";
        public static readonly string SESION_PAGOSERVICIOS = "SESION_PAGOSERVICIOS";
        public static readonly string SESION_ENTIDAD_LOTEDOCUMENTO = "SESION_LOTEDOCUMENTO";
        public static readonly string SESION_LISTAOPERACIONES = "SESION_LISTAOPERACIONES";
        public static readonly string SESION_LISTAOPERACIONES_MASIVAS = "SESION_LISTAOPERACIONES_MASIVAS";
        public static readonly string SESION_LISTAOPERACIONES_SELECCIONADAS = "SESION_LISTAOPERACIONES_SELECCIONADAS";
        public static readonly string SESION_LISTACLIENTES_ENDOSE = "SESION_LISTACLIENTES_ENDOSE";
        public static readonly string SESION_ERROR = "ERROR";
        public static readonly string SESION_LISTACONSULTACONTEDOR = "SESION_LISTACONSULTACONTEDOR";

        public static readonly string CONTENEDOR_PAGADO_NO = "N";
        public static readonly string CONTENEDOR_PAGADO_SI = "S";

        public static readonly string NRO_INTENTOS_ACTUALIZACION = "REINTENTO";
        public static readonly string MENSAJE_REINTENTO_ERP = "MENSAJEREINTENTOERP";
        public static readonly string MENSAJE_REINTENTO_GESFOR = "MENSAJEREINTENTOGESFOR";

        public static readonly string TIPO_PAGODOCUMENTOS = "D";

        public static readonly int TIPOVAL_CONTENEDOR_NOPERMITIDO = 0;
        public static readonly int TIPOVAL_CONTENEDOR_EXCEPCION = 1;
        public static readonly string CAMPOVALIDACION = "continuarAtencion";

        public static readonly string SESION_LISTAFICHAOPERACIONES = "SESION_LISTAFICHAOPERACIONES";
        public static readonly string SESION_LISTAFICHAOPERACIONES_SELECCIONADAS = "SESION_LISTAFICHAOPERACIONES_SELECCIONADAS";

        public static readonly string SESION_LISTAFICHACONTENEDORES = "SESION_LISTAFICHACONTENEDORES";
        public static readonly string SESION_LISTAFICHACONTENEDORES_SELECCIONADAS = "SESION_LISTAFICHACONTENEDORES_SELECCIONADAS";

        #region Estado Documentos

        public static readonly string ESTADO_REGISTRO_PAGONET = "Registro_PagoNet";
        public static readonly string ESTADO_ENVIADO_PAGONET = "Enviado_PagoNet";
        public static readonly string ESTADO_PROCESADO_PAGONET = "Procesado_PagoNet";
        public static readonly string ESTADO_CANCELADO_PAGONET = "Cancelado_PagoNet";
        public static readonly string ESTADO_RECHAZADO_PAGONET = "Rechazado_PagoNet";
        public static readonly string ESTADO_ENVIADO_ERP = "Enviado_ERP";
        public static readonly string ESTADO_PROCESADO_ERP = "Procesado_ERP";
        public static readonly string ESTADO_ERROR_ERP = "Error_ERP";
        public static readonly string ESTADO_ENVIADO_FACTURACION = "Enviado_Facturacion";
        public static readonly string ESTADO_PROCESADO_FACTURACION = "Procesado_Facturacion";
        public static readonly string ESTADO_ERROR_FACTURACION = "Error_Facturacion";
        public static readonly string ESTADO_PROCESADO_CREDITO = "Procesado_Credito";
        public static readonly string ESTADO_ANULACION_SERVICIO = "Anulacion_Servicio";

        public static readonly string ESTADO_PROCESADO_SINPAGO = "Procesado_SinPago";

        public static readonly string ESTADO_PENDIENTE = "Pendiente";
        public static readonly string ESTADO_PAGADA = "Pagada";
        public static readonly string ESTADO_ANULADA = "Anulada";
        public static readonly string ESTADO_EXPIRADA = "Expirada";

        #endregion

    }
}

