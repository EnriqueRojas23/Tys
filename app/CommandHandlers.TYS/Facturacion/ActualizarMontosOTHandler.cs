
using CommandHandlers.Common;
using Domain.Common.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using QueryHandlers.Common;
using QueryContracts.Common;

namespace CommandHandlers.TYS.Facturacion
{
    using CommandContracts.TYS.Facturacion;
    using CommandContracts.TYS.Facturacion.Output;
    using Domain.TYS.Facturacion;
    using Domain.TYS.Seguimiento;

    public class  ActualizarMontosOTHandler :   ICommandHandler<ActualizarMontosOTCommand> 
    {
        private readonly IRepository<OrdenTrabajo> _OrdenTrabajoRepository;

        public ActualizarMontosOTHandler(IRepository<OrdenTrabajo> pOrdenTrabajoRepository)
        {
            this._OrdenTrabajoRepository = pOrdenTrabajoRepository;
        }
        public CommandContracts.Common.CommandResult Handle(ActualizarMontosOTCommand command)
        {
            OrdenTrabajo dominio = null;
            if (command.idordentrabajo.HasValue)
                dominio = _OrdenTrabajoRepository.Get(x => x.idordentrabajo == command.idordentrabajo).LastOrDefault();
            else
                throw new ArgumentException("Tiene que ingresar una OT");

            dominio.subtotal = command.subtotal;
            dominio.igv = command.igv;
            dominio.total = command.total;
            try
            {
                _OrdenTrabajoRepository.SaveChanges();
                return new ActualizarMontosOTOutput() {    idordentrabajo = dominio.idordentrabajo };

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
