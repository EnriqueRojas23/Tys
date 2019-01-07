

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
using System.Collections.Generic;

using Domain.TYS.Monitoreo;


namespace CommandHandlers.TYS
{
    public class ActualizarLiquidacionHandler : ICommandHandler<ActualizarLiquidacionCommand> 
    {

        private readonly IRepository<OrdenTrabajo> _OrdenTrabajoRepository;

        public ActualizarLiquidacionHandler(IRepository<OrdenTrabajo> pOrdenTrabajoRepository)
        {
            this._OrdenTrabajoRepository = pOrdenTrabajoRepository;
        }
        public CommandContracts.Common.CommandResult Handle(ActualizarLiquidacionCommand command)
        {

            OrdenTrabajo dominio = 
                _OrdenTrabajoRepository.Get(x => x.idordentrabajo.Equals(command.idordentrabajo)).FirstOrDefault();

            dominio.idordentrabajo = command.idordentrabajo;
            dominio.fechaentregaconciliacion = command.fechaentregaconciliacion;
            dominio.idusuarioconciliacion = command.idusuarioconciliacion;  
            dominio.idestado = command.idestado;
            dominio.archivado = command.archivado;

            try
            {

                _OrdenTrabajoRepository.SaveChanges();

                return new ActualizarLiquidacionOutput() {   idordentrabajo = dominio.idordentrabajo};

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
