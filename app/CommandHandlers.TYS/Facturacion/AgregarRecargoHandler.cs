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
    using Domain.TYS.Seguimiento;

    public class AgregarRecargoHandler : ICommandHandler<AgregarRecargoCommand>
    {
        private readonly IRepository<OrdenTrabajo> _OrdenTrabajoRepository;

        public AgregarRecargoHandler(IRepository<OrdenTrabajo> pOrdenTrabajoRepository)
        {
            this._OrdenTrabajoRepository = pOrdenTrabajoRepository;
        }

        public CommandContracts.Common.CommandResult Handle(AgregarRecargoCommand command)
        {
            OrdenTrabajo dominio = null;
            if (command.idordentrabajo.HasValue)
                dominio = _OrdenTrabajoRepository.Get(x => x.idordentrabajo == command.idordentrabajo).LastOrDefault();
            else
                throw new ArgumentException("Tiene que ingresar una OT");

            dominio.recargo = command.recargo;
            dominio.subtotalfinal = dominio.subtotal.Value + dominio.recargo;
            try
            {
                _OrdenTrabajoRepository.SaveChanges();
                return new InsertarActualizarPreliquidacionOutput() { idordentrabajo = dominio.idordentrabajo };
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