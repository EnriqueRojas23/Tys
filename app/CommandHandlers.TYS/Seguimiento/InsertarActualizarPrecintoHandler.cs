

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarPrecintoHandler : ICommandHandler<InsertarActualizarPrecintoCommand>
    {
        private readonly IRepository<Precinto> _PrecintoRepository;


        public InsertarActualizarPrecintoHandler(IRepository<Precinto> pPrecintoRepository)
        {
            this._PrecintoRepository = pPrecintoRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarPrecintoCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una Chofer");


            Precinto dominio = null;
           if (command.idprecinto.HasValue)
               dominio = _PrecintoRepository.Get(x => x.idprecinto == command.idprecinto).LastOrDefault();
            else
               dominio = new Precinto();
           
                //dominio.iddireccion = command.iddireccion;
           dominio.precinto = command.precinto;

            try
            {
                if (!command.idprecinto.HasValue)
                    _PrecintoRepository.Add(dominio);
                _PrecintoRepository.Commit();


                return new InsertarActualizarChoferOutput() {   idchofer = dominio.idprecinto };

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
