using CommandHandlers.Common;
using Domain.Common.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using QueryHandlers.Common;
using QueryContracts.Common;

namespace CommandHandlers.TYS
{
    using CommandContracts.TYS.Facturacion;
    using Domain.TYS.Facturacion;
    using CommandContracts.TYS.Facturacion.Output;

    public class EliminarComprobanteHandler : ICommandHandler<EliminarComprobanteCommand>
    {
        private readonly IRepository<Comprobante> _ComprobanteRepository;
        private readonly IRepository<DetalleComprobante> _DetalleComprobanteRepository;
        private readonly IRepository<Preliquidacion> _PreliquidacionRepository;

        public EliminarComprobanteHandler(IRepository<Comprobante> pComprobanteRepository,
           IRepository<DetalleComprobante> pDetalleComprobanteRepository, IRepository<Preliquidacion> pPreliquidacionRepository)
        {
            this._ComprobanteRepository = pComprobanteRepository;
            this._DetalleComprobanteRepository = pDetalleComprobanteRepository;
            this._PreliquidacionRepository = pPreliquidacionRepository;
        }

        public CommandContracts.Common.CommandResult Handle(EliminarComprobanteCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");

            Comprobante dominio = null;
            dominio = _ComprobanteRepository.Get(x => x.idcomprobantepago == command.idcomprobante).LastOrDefault();

            var prelidominio = _PreliquidacionRepository.Get(x => x.idcomprobantepago == command.idcomprobante).LastOrDefault();

            prelidominio.idcomprobantepago = null;
            prelidominio.idestado = 22;

            _PreliquidacionRepository.SaveChanges();

            var detdominio = _DetalleComprobanteRepository.Get(x => x.idcomprobantepago == dominio.idcomprobantepago).ToList();

            try
            {
                foreach (var item in detdominio)
                {
                    _DetalleComprobanteRepository.Delete(item);
                    _DetalleComprobanteRepository.SaveChanges();
                }

                _ComprobanteRepository.Delete(dominio);
                _ComprobanteRepository.SaveChanges();

                return new InsertarActualizarPreliquidacionOutput() { idpreliquidacion = dominio.idpreliquidacion };
            }
            catch (Exception ex)
            {
                //  _ValortablaRepository.Delete(dominio);
                //_ValortablaRepository.Commit();
                throw;
            }
        }
    }
}