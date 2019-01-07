

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarLineaConsumidaClienteHandler : ICommandHandler<InsertarActualizarLineaConsumidaClienteCommand>
    {
        private readonly IRepository<Cliente> _ClienteRepository;


        public InsertarActualizarLineaConsumidaClienteHandler(IRepository<Cliente> pClienteRepository)
        {
            this._ClienteRepository = pClienteRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarLineaConsumidaClienteCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");


           Cliente  dominio = null;
           if (command.idcliente.HasValue)
               dominio = _ClienteRepository.Get(x => x.idcliente == command.idcliente).LastOrDefault();
            else
               throw new ArgumentException("Tiene que ingresar una cliente");


           dominio.lineaconsumida = command.lineaconsumida;
            


            try
            {
                _ClienteRepository.Commit();


                return new InsertarActualizarClienteOutput() {   idcliente = dominio.idcliente };

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
