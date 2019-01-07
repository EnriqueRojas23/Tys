using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;

namespace CommandHandlers.TYS
{
    public class EliminarGuiaRemisionClienteHandler : ICommandHandler<EliminarGuiaRemisionClienteCommand>
    {
        private readonly IRepository<GuiaRemisionCliente> _GuiaRemisionClienteRepository;

        public EliminarGuiaRemisionClienteHandler(IRepository<GuiaRemisionCliente> pGuiaRemisionClienteRepository)
        {
            this._GuiaRemisionClienteRepository = pGuiaRemisionClienteRepository;
        }

        public CommandContracts.Common.CommandResult Handle(EliminarGuiaRemisionClienteCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una OT");

            var dominio = _GuiaRemisionClienteRepository.Get(x => x.idordentrabajo == command.idordentrabajo).ToList();

            try
            {
                foreach (var item in dominio)
                {
                    _GuiaRemisionClienteRepository.Delete(item);
                }
                _GuiaRemisionClienteRepository.Commit();
                return new InsertarActualizarGuiaRemisionClienteOutput() { idguiaremisioncliente = command.idordentrabajo };
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