

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarChoferHandler : ICommandHandler<InsertarActualizarChoferCommand>
    {
        private readonly IRepository<Chofer> _ChoferRepository;


        public InsertarActualizarChoferHandler(IRepository<Chofer> pChoferRepository)
        {
            this._ChoferRepository = pChoferRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarChoferCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una Chofer");


           Chofer  dominio = null;
           if (command.idchofer.HasValue)
               dominio = _ChoferRepository.Get(x => x.idchofer == command.idchofer).LastOrDefault();
            else
               dominio = new Chofer();
            if (!command.activo)
                dominio.activo = false;
            else
            {
                //dominio.iddireccion = command.iddireccion;
                dominio.nombrechofer = command.nombrechofer;
                dominio.apellidochofer = command.apellidochofer;
                dominio.dni = command.dni;
                dominio.brevete = command.brevete;
                dominio.telefono = command.telefono;
                dominio.direccionchofer = command.direccionchofer;
                dominio.activo = true;
            }
            


            try
            {
                if (!command.idchofer.HasValue)
                    _ChoferRepository.Add(dominio);
                _ChoferRepository.Commit();


                return new InsertarActualizarChoferOutput() {   idchofer = dominio.idchofer };

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
