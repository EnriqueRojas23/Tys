

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarZonaHandler : ICommandHandler<InsertarActualizarZonaCommand>
    {
        private readonly IRepository<Zona> _ZonaRepository;


        public InsertarActualizarZonaHandler(IRepository<Zona> pZonaRepository)
        {
            this._ZonaRepository = pZonaRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarZonaCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");

           Zona  dominio = null;
           if (command.idzona.HasValue)
               dominio = _ZonaRepository.Get(x => x.idzona == command.idzona).LastOrDefault();
            else
               dominio = new Zona();

           dominio.zona = command.zona;


            try
            {
                if (!command.idzona.HasValue)
                    _ZonaRepository.Add(dominio);
                _ZonaRepository.Commit();


                return new InsertarActualizarZonaOutput() { idzona = dominio.idzona };

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
