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

namespace CommandHandlers.TYS.Contenedor
{
    public class ActualizarBookingWSHandler : ICommandHandler<ActualizarBookingWSCommand>
    {
        private readonly IRepository<ReservaBooking> _reservaBooking;
        public ActualizarBookingWSHandler(IRepository<ReservaBooking> pReservaBooking)
        {
            this._reservaBooking = pReservaBooking;
        }

        public CommandResult Handle(ActualizarBookingWSCommand command)
        {
            var dominio = _reservaBooking.Get(x => x.rb_int_id == command.rb_int_id).LastOrDefault();
            if (dominio == null) throw new ReservaBookingException("No se encontro el booking seleccionado");

            if(!string.IsNullOrEmpty(command.rb_int_identificador_terminal)) dominio.rb_int_identificador_terminal = decimal.Parse(command.rb_int_identificador_terminal);
            else _reservaBooking.Delete(dominio);

            _reservaBooking.Commit();
            return new ActualizarBookingWSOutput() { TransaccionCorrecta = true };

        }
    }
}
