using System;
using System.Linq;
using CommandContracts.Common;
using CommandContracts.TYS.Contenedor;
using CommandContracts.TYS.Contenedor.Outputs;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.contenedor;
using Domain.TYS.contenedor.Exceptions;
using System.Collections.Generic;

namespace CommandHandlers.TYS.Contenedor
{
    public class ActualizarWSIdTerminalHandler : ICommandHandler<ActualizarWSIdTerminalCommand>
    {
        private readonly IRepository<ReservaBookingDetalle> _reservabookingdetalle;
        public ActualizarWSIdTerminalHandler(IRepository<ReservaBookingDetalle> pReservaBookingDetalle)
        {
            this._reservabookingdetalle = pReservaBookingDetalle;
        }
        public CommandResult Handle(ActualizarWSIdTerminalCommand command)
        {
            var dominio_detalle = _reservabookingdetalle.Get(x => x.rbd_int_id == command.rbd_int_id).LastOrDefault();
            if (dominio_detalle == null) throw new ReservaBookingException("El Id ingresado no se encuentra en el sistema");
            dominio_detalle.rb_int_identificador_terminal = command.rb_int_identificador_terminal;

            try{
                _reservabookingdetalle.Commit();
                return new ActualizarWSIdTerminalOutput() { IdActualizado = true, MensajeError = string.Empty};

            }
            catch(Exception ex){
                return new ActualizarWSIdTerminalOutput() {  IdActualizado = false, MensajeError = ex.Message};
            }
        }
    }
}
