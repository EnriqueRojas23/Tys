

using CommandHandlers.Common;
using Domain.Common.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using Domain.TYS.Monitoreo;
using QueryHandlers.Common;
using QueryContracts.Common;





namespace CommandHandlers.TYS.Facturacion
{

    using CommandContracts.TYS.Facturacion;
    using Domain.TYS.Facturacion;
    using CommandContracts.TYS.Facturacion.Output;


    public class InsertarActualizarDetalleComprobanteHandler :   ICommandHandler<InsertarActualizarDetalleComprobanteCommand> 
    {

        private readonly IRepository<DetalleComprobante> _DetalleComprobanteRepository;

        public InsertarActualizarDetalleComprobanteHandler(IRepository<DetalleComprobante> pDetalleComprobanteRepository)
        {
            this._DetalleComprobanteRepository = pDetalleComprobanteRepository;
        }


        public CommandContracts.Common.CommandResult Handle(InsertarActualizarDetalleComprobanteCommand command)
        {

         //   if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");
            DetalleComprobante dominio = null;
            if (command.iddetallecomprobante.HasValue)
                dominio = _DetalleComprobanteRepository.Get(x => x.iddetallecomprobante == command.iddetallecomprobante).LastOrDefault();
            else
                dominio = new DetalleComprobante();

            dominio.idcomprobantepago = command.idcomprobantepago;
            dominio.idordentrabajo = command.idordentrabajo;
            dominio.igv = command.igv;
            dominio.subtotal = command.subtotal;
            dominio.total = command.total;
            dominio.recargo = command.recargo;
            dominio.descripcion = command.descripcion;
            

            try
            {
                if (!command.iddetallecomprobante.HasValue)
                    _DetalleComprobanteRepository.Add(dominio);
                _DetalleComprobanteRepository.SaveChanges();

                return new InsertarActualizarDetalleComprobanteOutput() { iddetallecomprobante = dominio.iddetallecomprobante };

            }
            catch (Exception ex)
            {
                throw;
            }

        }

       
    }
}
