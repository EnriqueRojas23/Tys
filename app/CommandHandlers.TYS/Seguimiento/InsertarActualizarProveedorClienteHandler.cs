

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarProveedorClienteHandler : ICommandHandler<InsertarActualizarProveedorClienteCommand>
    {
        private readonly IRepository<ProveedorCliente> _ProveedorClienteRepository;


        public InsertarActualizarProveedorClienteHandler(IRepository<ProveedorCliente> pProveedorClienteRepository)
        {
            this._ProveedorClienteRepository = pProveedorClienteRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarProveedorClienteCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");


           ProveedorCliente  dominio = null;
           if (command.idproveedorcliente.HasValue)
               dominio = _ProveedorClienteRepository.Get(x => x.idproveedorcliente == command.idproveedorcliente).LastOrDefault();
            else
               dominio = new ProveedorCliente();

           dominio.idcliente = command.idcliente;
           dominio.idproveedor = command.idproveedor;


            try
            {
                if (!command.idproveedorcliente.HasValue)
                    _ProveedorClienteRepository.Add(dominio);
                _ProveedorClienteRepository.Commit();


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
