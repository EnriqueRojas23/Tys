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
    public class ActualizarPagoBookingHandler : ICommandHandler<ActualizarPagoBookingCommand>
    {
        private readonly IRepository<ReservaBooking> _reservabooking;
        private readonly IRepository<ReservaBookingPago> _reservabookinpago;
        private readonly IRepository<ReservaBookingDetalle> _reservabookingdetalle;
        private readonly IRepository<ReservaBookingServicioAdicional> _reservabookingServicio;

        public ActualizarPagoBookingHandler(IRepository<ReservaBooking> pReservaBooking, IRepository<ReservaBookingPago> pReservaBookinPago, IRepository<ReservaBookingDetalle> pReservaBookingDetalle, IRepository<ReservaBookingServicioAdicional> pReservaBookingServicio) 
        {
            this._reservabooking = pReservaBooking;
            this._reservabookinpago = pReservaBookinPago;
            this._reservabookingdetalle = pReservaBookingDetalle;
            this._reservabookingServicio = pReservaBookingServicio;
        }

        public CommandResult Handle(ActualizarPagoBookingCommand command)
        {
            //var dominioActualizar = _reservabookinpago.Get(x => x.rbp_str_cip == command.cip && x.rpb_str_estado_pago == "RE").SingleOrDefault();
            var dominioActualizar = _reservabookinpago.Get(x => x.rbp_str_cip == command.cip).SingleOrDefault();
            var booking = _reservabooking.Get(x => x.rb_int_id == dominioActualizar.rb_int_id.Value).SingleOrDefault();
            if (dominioActualizar != null)
            {
                //
                string IDs = "";
                // --| 
                // --| MGZP   - Inicio
                // --| Fecha  : 12/09/2016
                // --| Motivo : Añadir Casos para el metodo de actualización
                // --|          - EX = Expirado
                // --|          - XT = Extorno
                if (command.estado_cip == "PG" || command.estado_cip == "EX" || command.estado_cip == "XT")
                // --| MGZP   - Fin
                //
                {
                    var ListaServiciosContenedores = _reservabookingServicio.Get(x => x.reserva_booking_pago.rbp_int_id == dominioActualizar.rbp_int_id).ToList();
                    var Contenedores = ListaServiciosContenedores.Select(x => x.rbd_int_id).Distinct().ToList();
                    //
                    foreach (var contenedor in Contenedores)
                    {
                        var dominioDetalle = _reservabookingdetalle.Get(x => x.rbd_int_id == contenedor).SingleOrDefault();
                        //
                        if (command.estado_cip == "PG")
                        {
                            dominioActualizar.rpb_str_estado_pago = command.estado_cip;
                            dominioDetalle.rbd_str_estado_bookingdet = "PG";
                        }

                        // --| 
                        // --| <Inicio>
                        // --| Autor  : MGZP
                        // --| Fecha  : 12/09/2016
                        // --| Motivo : Cambiar de Estado segun lo que envie PagoEfectivo
                        // --|          - EX = Expirado
                        // --|          - XT = Extorno
                        if (command.estado_cip == "EX")
                        {
                            if ((dominioActualizar.rpb_str_estado_pago == "RE") || (dominioActualizar.rpb_str_estado_pago == "EX"))
                            {
                                dominioDetalle.rbd_str_estado_bookingdet = "RE";
                                dominioActualizar.rpb_str_estado_pago = "EX";
                            }
                        }
                        //
                        if (command.estado_cip == "XT")
                        {
                            if ((dominioActualizar.rpb_str_estado_pago == "PG") || (dominioActualizar.rpb_str_estado_pago == "XT"))
                            {
                                dominioDetalle.rbd_str_estado_bookingdet = "XT";
                                dominioActualizar.rpb_str_estado_pago = "XT";
                            }
                            else
                            {
                                dominioActualizar.rpb_str_estado_pago = "RE";
                            }
                        }
                        // --| <Fin>
                        //

                        IDs = IDs + contenedor.ToString() + ",";
                    }
                    _reservabookinpago.Commit();
                    _reservabookingdetalle.Commit();
                }
                //

                //
                return new ActualizarPagoBookingOutput() { booking = booking.rb_str_numero_booking, detalle = IDs };
            }
            else
            {
                return new ActualizarPagoBookingOutput() { booking = booking.rb_str_numero_booking, detalle = "" };
            }
           
        }

    }
}

