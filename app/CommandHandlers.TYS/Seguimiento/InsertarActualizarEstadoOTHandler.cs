

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
using System.Collections.Generic;


using CommandContracts.TYS.Seguimiento;
using QueryContracts.TYS.Seguimiento.Parameters;
using QueryContracts.TYS.Seguimiento.Results;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarEstadoOTHandler : ICommandHandler<InsertarActualizarEstadoOTCommand>
    {
        private readonly IRepository<OrdenTrabajo> _OrdenTrabajoRepository;


        public InsertarActualizarEstadoOTHandler(IRepository<OrdenTrabajo> pOrdenTrabajoRepository)
        {
            this._OrdenTrabajoRepository = pOrdenTrabajoRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarEstadoOTCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");


            OrdenTrabajo dominio = null;
           if (command.idordentrabajo.HasValue)
               dominio = _OrdenTrabajoRepository.Get(x => x.idordentrabajo == command.idordentrabajo).LastOrDefault();
            else
               dominio = new OrdenTrabajo();
               dominio.idestado = command.idestado;
            try
            {
                if (!command.idordentrabajo.HasValue)
                    _OrdenTrabajoRepository.Add(dominio);
                _OrdenTrabajoRepository.Commit();
                
                return new InsertarActualizarOrdenTrabajoOutput() { idordentrabajo = dominio.idordentrabajo };

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
