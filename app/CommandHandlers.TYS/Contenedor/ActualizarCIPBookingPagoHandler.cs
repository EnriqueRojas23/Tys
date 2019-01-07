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
    public class ActualizarCIPBookingPagoHandler : ICommandHandler<ActualizarCIPBookingPagoCommand>
    {
        private readonly IRepository<ReservaBookingPago> _reservaBookingPago;
        private readonly IRepository<ReservaBookingDetalle> _reservabookingdetalle;

        public ActualizarCIPBookingPagoHandler(IRepository<ReservaBookingPago> pReservaBookingPago, IRepository<ReservaBookingDetalle> pReservaBookingDetalle)
        {
            this._reservaBookingPago = pReservaBookingPago;
            this._reservabookingdetalle = pReservaBookingDetalle;
        }

        public CommandResult Handle(ActualizarCIPBookingPagoCommand command)
        {
            var dominio = _reservaBookingPago.Get(x => x.rbp_int_id == command.rbp_int_id).LastOrDefault();
            if (dominio == null) throw new ReservaBookingException("No se encontro el pago seleccionado");

            string[] aux = command.ids.Split(',');
            List<long> ids_final = new List<long>();

            foreach (var item in aux)
            {
                ids_final.Add(Convert.ToInt64(item));
            }



            dominio.rbp_str_cip = command.cip;
          //  dominio.
          
            try 
            {
                _reservaBookingPago.Commit();
                _reservaBookingPago.Commit();


                var handler = new ActualizarEstadoReservaDetalleHandler(this._reservabookingdetalle);
                var output = (ActualizarEstadoReservaDetalleOutput)handler.Handle(new ActualizarEstadoReservaDetalleCommand()
                {
                    lst_rbd_int_id = ids_final,
                    //rbd_str_estado_bookingdet = ActualizarEstadoReservaDetalleCommand.EstadoReservaDetalle.PendientePagar, 
                    rbd_str_estado_bookingdet = ActualizarEstadoReservaDetalleCommand.EstadoReservaDetalle.PendientePagar,
                    //suarioCreacion = command.rbp_str_usuario_creacion,
                    DescripcionOpcional = "Generando Resumen pago"
                }); 
                              

                return new ActualizarCIPBookingPagoOutput() { ActualizarCIP = true };

            }
            catch (Exception ex){
                return new ActualizarCIPBookingPagoOutput() { ActualizarCIP = false };

            }
        }
    }
}
