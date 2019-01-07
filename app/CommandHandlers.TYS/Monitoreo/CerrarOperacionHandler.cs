

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
using System.Collections.Generic;

using Domain.TYS.Monitoreo;
using CommandContracts.Common;


namespace CommandHandlers.TYS
{
    public class CerrarOperacionHandler : ICommandHandler<CerrarOperacionCommand> 
    {

        private readonly IRepository<OrdenTrabajo> _OrdenTrabajoRepository;
        private readonly IRepository<Manifiesto> _ManfiestoRepository;

        public CerrarOperacionHandler(IRepository<OrdenTrabajo> pOrdenTrabajoRepository
            , IRepository<Manifiesto> _pManifiestoRepository)
        {
            this._OrdenTrabajoRepository = pOrdenTrabajoRepository;
            this._ManfiestoRepository = _pManifiestoRepository;
        }

        public CommandResult Handle(CerrarOperacionCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");


            if (command.idmanifiesto == null)
            {
                OrdenTrabajo dominio = null;
                if (command.idorden.HasValue)
                    dominio = _OrdenTrabajoRepository.Get(x => x.idordentrabajo == command.idorden).LastOrDefault();
                else
                    dominio = new OrdenTrabajo();
               // dominio.cierreoperativo = command.cierreoperativo;
                try
                {
                    _OrdenTrabajoRepository.SaveChanges();
                    return new InsertarActualizarIncidenteOutput() { idincidencia = dominio.idordentrabajo };
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            else
            {
                Manifiesto dominio = null;
                dominio = _ManfiestoRepository.Get(x => x.idmanifiesto == command.idmanifiesto).LastOrDefault();
                //dominio.cierreoperativo = true;
                try
                {
                    _ManfiestoRepository.SaveChanges();
                    return new InsertarActualizarIncidenteOutput() { idincidencia = dominio.idmanifiesto };
                }
                catch (Exception ex)
                {
                    throw;
                }

            }

        }

       
    }
}
