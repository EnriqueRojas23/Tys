using System;
using System.Linq;
using CommandContracts.Common;
using CommandContracts.TYS.Contenedor;
using CommandContracts.TYS.Contenedor.Outputs;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.contenedor;
using Domain.TYS.contenedor.Exceptions;
using QueryContracts.TYS.Contenedores.Results;
using QueryHandlers.TYS.Contenedores;
using QueryContracts.TYS.Contenedores.Parameters;
using QueryContracts.TYS.Configuracion.Results;
using QueryHandlers.TYS.Configuracion;
using QueryContracts.TYS.Configuracion.Parameters;
using System.Collections.Generic;
using Componentes.Common.Extensions;

namespace CommandHandlers.TYS.Contenedor
{
    public class InsertarTransporteHandler : ICommandHandler<InsertarTransporteCommand>
    {
        private List<ListarMultiusoPorTipoDto> multiusoresult = null;
        private readonly string PROCESO = "INSERTAR_TRANSPORTE";  
        private readonly IRepository<ReservaBookingDetalle> _reservaBookingDetalle;

        public InsertarTransporteHandler(IRepository<ReservaBookingDetalle> pReservaBookingDetalle)
        {
            this._reservaBookingDetalle = pReservaBookingDetalle;
            SetListaMensajeMultiuso(PROCESO);
        }
        
        public CommandResult Handle(InsertarTransporteCommand command)
        {
            var dominiot = _reservaBookingDetalle.Get(x => x.rbd_int_id == command.rbd_int_id).LastOrDefault();
            if (dominiot == null) throw new ReservaBookingException("El espacio del contenedor registrado no existe en el sistema");

            dominiot.rbd_str_ent_cif_transportista_descripcion = command.rbd_str_ent_cif_transportista_descripcion;
            dominiot.rbd_str_ent_cif_transportista = command.rbd_str_ent_cif_transportista;

            dominiot.rbd_str_fecha_recojo = command.rbd_str_fecha_recojo;
            dominiot.rbd_str_hora_recojo = command.rbd_str_hora_recojo;
            dominiot.rbd_str_trans_matricula_camion = command.rbd_str_trans_matricula_camion;
            dominiot.rbd_str_trans_nif_conductor = command.rbd_str_trans_nif_conductor;
            dominiot.rbd_str_trans_nombre_conductor = command.rbd_str_trans_nombre_conductor;

            _reservaBookingDetalle.Commit();

            return new InsertarTransporteOutput { rbd_int_id = dominiot.rbd_int_id };
            
        }

      


        public void SetListaMensajeMultiuso(string pmlt_str_tipo)
        {
            var query = new ListarMultiusoPorTipoQuery();
            var result = (ListarMultiusoPorTipoResult)query.Handle(new ListarMultiusoPorTipoParameter() { mlt_str_tipo = pmlt_str_tipo });
            multiusoresult = result != null ? result.Hits.ToList() : new List<ListarMultiusoPorTipoDto>();
        }

        public string GetMensaje(string codigomensaje)
        {
            if (multiusoresult == null) return string.Empty;
            var objeto = multiusoresult.LastOrDefault(x => x.mlt_str_valor2 == codigomensaje);
            return objeto == null ? string.Empty : objeto.mlt_str_descripcion;
        }
    }
}
