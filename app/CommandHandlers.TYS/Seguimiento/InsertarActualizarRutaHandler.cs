

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarRutaHandler : ICommandHandler<InsertarActualizarRutaCommand>
    {
        private readonly IRepository<Ruta> _RutaRepository;


        public InsertarActualizarRutaHandler(IRepository<Ruta> pRutaRepository)
        {
            this._RutaRepository = pRutaRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarRutaCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una Ruta");


           Ruta  dominio = null;
           if (command.idruta.HasValue)
               dominio = _RutaRepository.Get(x => x.idruta == command.idruta).LastOrDefault();
            else
               dominio = new Ruta();
          
           dominio.iddestino = command.iddestino;
           dominio.idorigen = command.idorigen;
           dominio.km = command.km;
           dominio.nombre = command.nombre;
           dominio.ruta = dominio.ruta;
            


            try
            {
                if (!command.idruta.HasValue)
                    _RutaRepository.Add(dominio);
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
