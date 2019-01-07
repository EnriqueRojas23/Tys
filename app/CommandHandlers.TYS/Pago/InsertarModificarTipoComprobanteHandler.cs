
using CommandContracts.Common;
using CommandContracts.TYS.Pago;
using CommandContracts.TYS.Pago.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Pago;
using System;
using System.Collections.Generic;
using System.Linq;
namespace CommandHandlers.TYS.Pago
{
    public class InsertarModificarTipoComprobanteHandler : ICommandHandler<InsertarActualizarTipoComprobanteCommand>
    {
        private readonly IRepository<TipoComprobante> _TipoComprobanteRepository;

        public InsertarModificarTipoComprobanteHandler(IRepository<TipoComprobante> pTipoComprobanteRepository)
        {
            this._TipoComprobanteRepository = pTipoComprobanteRepository;
        }
        public CommandResult Handle(InsertarActualizarTipoComprobanteCommand command)
        {

            TipoComprobante dominio = null;
            if (command.idtipocomprobante.HasValue)
                dominio = _TipoComprobanteRepository.Get(x => x.idtipocomprobante == command.idtipocomprobante).LastOrDefault();
            else
                dominio = new TipoComprobante();
            if (command.activo)
            {
                dominio.activo = command.activo;
            }
            else
            {
                dominio.codigo = command.codigo;
                dominio.tipocomprobante = command.tipocomprobante;
            }

            try
            {
                if (!command.idtipocomprobante.HasValue)
                    _TipoComprobanteRepository.Add(dominio);
                _TipoComprobanteRepository.Commit();


                return new InsertarTipoComprobanteOutput() {  idtipocomprobante = dominio.idtipocomprobante };

            }
            catch (Exception ex)
            {
                _TipoComprobanteRepository.Delete(dominio);
                _TipoComprobanteRepository.Commit();
                throw;
            }
            
        }
    }
}
