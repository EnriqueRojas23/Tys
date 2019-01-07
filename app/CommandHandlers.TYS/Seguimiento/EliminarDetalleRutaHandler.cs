

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class EliminarDetalleRutaHandler : ICommandHandler<EliminarDetalleRutaCommand>
    {
        private readonly IRepository<DetalleRuta> _DetalleRutaRepository;


        public EliminarDetalleRutaHandler(IRepository<DetalleRuta> pDetalleRutaRepository)
        {
            this._DetalleRutaRepository = pDetalleRutaRepository;
        }

        public CommandContracts.Common.CommandResult Handle(EliminarDetalleRutaCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una Ruta");
            DetalleRuta dominio = null;
            dominio = _DetalleRutaRepository.Get(x => x.iddetalleruta == command.iddetalleruta).LastOrDefault();
            try
            {
                _DetalleRutaRepository.Delete(dominio);
                _DetalleRutaRepository.Commit();
                return new InsertarActualizarDetalleRutaOutput() { iddetalleruta = dominio.iddetalleruta };

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
