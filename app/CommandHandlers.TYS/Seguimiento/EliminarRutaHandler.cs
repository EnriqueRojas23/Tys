

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class EliminarRutaHandler : ICommandHandler<EliminarRutaCommand>
    {
        private readonly IRepository<Ruta> _RutaRepository;


        public EliminarRutaHandler(IRepository<Ruta> pRutaRepository)
        {
            this._RutaRepository = pRutaRepository;
        }

        public CommandContracts.Common.CommandResult Handle(EliminarRutaCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una Ruta");
            Ruta dominio = null;
            dominio = _RutaRepository.Get(x => x.idruta == command.idruta).LastOrDefault();
            try
            {
                _RutaRepository.Delete(dominio);
                _RutaRepository.Commit();
                return new InsertarActualizarRutaOutput() { idruta = dominio.idruta };

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
