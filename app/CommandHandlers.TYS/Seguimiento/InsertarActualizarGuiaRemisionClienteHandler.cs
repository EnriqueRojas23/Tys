using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;

namespace CommandHandlers.TYS
{
    public class InsertarActualizarGuiaRemisionClienteHandler : ICommandHandler<InsertarActualizarGuiaRemisionClienteCommand>
    {
        private readonly IRepository<GuiaRemisionCliente> _GuiaRemisionClienteRepository;

        public InsertarActualizarGuiaRemisionClienteHandler(IRepository<GuiaRemisionCliente> pGuiaRemisionClienteRepository)
        {
            this._GuiaRemisionClienteRepository = pGuiaRemisionClienteRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarGuiaRemisionClienteCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");

            GuiaRemisionCliente dominio = null;
            if (command.idguiaremisioncliente.HasValue)
                dominio = _GuiaRemisionClienteRepository.Get(x => x.idguiaremisioncliente == command.idguiaremisioncliente).LastOrDefault();
            else
                dominio = new GuiaRemisionCliente();

            dominio.bulto = command.bulto;
            dominio.documento = command.documento;
            dominio.idcobrarpor = command.idcobrarpor;
            dominio.nroguia = command.nroguia;
            dominio.peso = command.peso;
            dominio.pesovol = command.pesovol;
            dominio.volumen = command.volumen;
            dominio.idordentrabajo = command.idordentrabajo;

            try
            {
                if (!command.idguiaremisioncliente.HasValue)
                    _GuiaRemisionClienteRepository.Add(dominio);
                _GuiaRemisionClienteRepository.Commit();

                return new InsertarActualizarGuiaRemisionClienteOutput() { idguiaremisioncliente = dominio.idguiaremisioncliente };
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