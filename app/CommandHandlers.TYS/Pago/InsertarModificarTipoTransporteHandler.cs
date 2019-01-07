
using CommandContracts.TYS.Pago;
using CommandContracts.TYS.Pago.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Pago;
using System;
using System.Linq;
namespace CommandHandlers.TYS.Pago
{
    public class InsertarModificarTipoTransporteHandler : ICommandHandler<InsertarActualizarTipoTransporteCommand>
    {
        private readonly IRepository<TipoTransporte> _TipoTransporteRepository;
        public InsertarModificarTipoTransporteHandler(IRepository<TipoTransporte> pTipoTransporteRepository)
        {
            this._TipoTransporteRepository = pTipoTransporteRepository;
        }


        public CommandContracts.Common.CommandResult Handle(InsertarActualizarTipoTransporteCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una etapa");


            TipoTransporte dominio = null;
            if (command.idtipotransporte.HasValue)
                dominio = _TipoTransporteRepository.Get(x => x.idtipotransporte == command.idtipotransporte).LastOrDefault();
            else
                dominio = new TipoTransporte();
            if (command.activo)
            {
                dominio.activo = command.activo;
            }
            else
            {
                dominio.tipotransporte = command.tipotransporte;
            }

            try
            {
                if (!command.idtipotransporte.HasValue)
                    _TipoTransporteRepository.Add(dominio);
                _TipoTransporteRepository.Commit();


                return new InsertarTipoTransporteOutput() {  idtipotransporte = dominio.idtipotransporte };

            }
            catch (Exception ex)
            {
                _TipoTransporteRepository.Delete(dominio);
                _TipoTransporteRepository.Commit();
                throw;
            }
        }
    }
}
