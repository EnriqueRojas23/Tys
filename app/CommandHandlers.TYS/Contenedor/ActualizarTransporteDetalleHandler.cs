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
    public class ActualizarTransporteDetalleHandler : ICommandHandler<ActualizarTransporteDetalleCommand>
    {
        private readonly IRepository<ReservaBookingDetalle> _reservaBookingDetalle;

        public ActualizarTransporteDetalleHandler(IRepository<ReservaBookingDetalle> pReservaBookingDetalle)
        {
            this._reservaBookingDetalle = pReservaBookingDetalle;
        }

        public CommandResult Handle(ActualizarTransporteDetalleCommand command)
        {
            var objActualizar = _reservaBookingDetalle.Get(x => x.rbd_int_id == command.rbd_int_id).SingleOrDefault();

            if (objActualizar != null)
            {
                objActualizar.rbd_str_ent_cif_transportista = command.rbd_str_ent_cif_transportista;
                objActualizar.rbd_str_ent_cif_transportista_descripcion = command.rbd_str_ent_cif_transportista_descripcion;
                objActualizar.rbd_str_trans_matricula_camion = command.rbd_str_trans_matricula_camion;
                objActualizar.rbd_str_trans_nif_conductor = command.rbd_str_trans_nif_conductor;
                objActualizar.rbd_str_trans_nombre_conductor = command.rbd_str_trans_nombre_conductor;
                objActualizar.rbd_str_fecha_recojo = command.rbd_str_fecha_recojo;
                objActualizar.rbd_str_hora_recojo = command.rbd_str_hora_recojo;
                objActualizar.rbd_bit_transporteasignado = true;
                _reservaBookingDetalle.Commit();

                return new ActualizarTransporteDetalleOutput() { ActualizarTransporte = true };
            }
            else
            {
                return new ActualizarTransporteDetalleOutput() { ActualizarTransporte = false };
            }
        }
    }
}
