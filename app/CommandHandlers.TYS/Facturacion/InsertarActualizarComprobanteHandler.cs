

using CommandContracts.TYS.Seguimiento;
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

    using CommandHandlers.TYS.Facturacion;
    using Domain.TYS.Facturacion;
    using CommandContracts.TYS.Facturacion;
    using CommandContracts.TYS.Facturacion.Output;
    
    public class    InsertarActualizarComprobanteHandler :   ICommandHandler<InsertarActualizarComprobanteCommand> 
    {

        private readonly IRepository<Comprobante> _ComprobanteRepository;
        private readonly IRepository<DetalleComprobante> _DetalleComprobanteRepository;

        public InsertarActualizarComprobanteHandler(IRepository<Comprobante> pComprobanteRepository , IRepository<DetalleComprobante> pDetalleComprobante)
        {
            this._ComprobanteRepository = pComprobanteRepository;
            this._DetalleComprobanteRepository = pDetalleComprobante;
        }


        public CommandContracts.Common.CommandResult Handle(InsertarActualizarComprobanteCommand command)
        {

         //   if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");
            Comprobante dominio = null;
            if (command.idcomprobantepago.HasValue)
                dominio = _ComprobanteRepository.Get(x => x.idcomprobantepago == command.idcomprobantepago).LastOrDefault();
            else
                dominio = new Comprobante();

            var DetallesComprobante = _DetalleComprobanteRepository.Get(x => x.idcomprobantepago == command.idcomprobantepago).ToList();

       

            if (command._tipoop == 1)
            {
                dominio.emisionrapida = command.emisionrapida;
                dominio.fechaemision = command.fechaemision;
                dominio.idcliente = command.idcliente;
                dominio.idpreliquidacion = command.idpreliquidacion;
                dominio.idtipocomprobante = command.idtipocomprobante;
                dominio.igv = command.igv;
                dominio.subtotal = command.subtotal;
                dominio.total = command.total;
                dominio.numerocomprobante = command.numerocomprobante;
                dominio.idusuarioregistro = command.idusuarioregistro;
                dominio.descripcion = command.descripcion;
                dominio.idestado = command.idestado;
                dominio.totalpeso = command.totalpeso;
                dominio.totalvolumen = command.totalvolumen;
                dominio.totalbulto = command.totalbulto;
                dominio.idfacturavinculada = command.idfacturavinculada;
                dominio.ordencompra = command.ordencompra;
                
            }
            else if (command._tipoop == 2)// Anulado
            {
                dominio.idestado = command.idestado;
                dominio.idpreliquidacion = null;
            }


            try
            {
                if (!command.idcomprobantepago.HasValue)
                    _ComprobanteRepository.Add(dominio);
                _ComprobanteRepository.SaveChanges();


                foreach (var item in DetallesComprobante)
                {
                    _DetalleComprobanteRepository.Delete(item);
                    _DetalleComprobanteRepository.SaveChanges();
                }

                return new InsertarActualizarComprobanteOutput() {  idcomprobantepago = dominio.idcomprobantepago };

            }
            catch (Exception ex)
            {
                throw;
            }

        }

       
    }
}
