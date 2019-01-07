

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarClienteHandler : ICommandHandler<InsertarActualizarClienteCommand>
    {
        private readonly IRepository<Cliente> _ClienteRepository;


        public InsertarActualizarClienteHandler(IRepository<Cliente> pClienteRepository)
        {
            this._ClienteRepository = pClienteRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarClienteCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");


           Cliente  dominio = null;
           if (command.idcliente.HasValue)
               dominio = _ClienteRepository.Get(x => x.idcliente == command.idcliente).LastOrDefault();
            else
               dominio = new Cliente();
            if (!command.activo)
                dominio.activo = false;
            else
            {
                dominio.idmonedalinea = command.idmonedalinea;
                dominio.lineacredito = command.lineacredito;
                dominio.nombrecorto = command.nombrecorto;
                dominio.razonsocial = command.razonsocial;
                dominio.ruc = command.ruc;
                dominio.rutalogo = command.rutalogo;
                dominio.activo = true;
                dominio.pagocontado = command.pagocontado;

            }
            


            try
            {
                if (!command.idcliente.HasValue)
                    _ClienteRepository.Add(dominio);
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
