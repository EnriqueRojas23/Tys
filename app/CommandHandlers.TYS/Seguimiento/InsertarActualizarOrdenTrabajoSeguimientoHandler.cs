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

using Domain.TYS.Monitoreo;
using QueryHandlers.TYS.Seguimiento;

namespace CommandHandlers.TYS
{
    public class InsertarActualizarOrdenTrabajoSeguimientoHandler : ICommandHandler<InsertarActualizarOrdenTrabajoSeguimientoCommand>
    {
        private readonly IRepository<OrdenTrabajoSeguimiento> _OrdenTrabajoRepository;

        public InsertarActualizarOrdenTrabajoSeguimientoHandler(IRepository<OrdenTrabajoSeguimiento> pOrdenTrabajoRepository)
        {
            this._OrdenTrabajoRepository = pOrdenTrabajoRepository;
        }
        public CommandContracts.Common.CommandResult Handle(InsertarActualizarOrdenTrabajoSeguimientoCommand command)
        {
            bool nuevoregistro = false;
            OrdenTrabajoSeguimiento dominio = null;
            if (command.idordentrabajo.HasValue)
            {
                dominio = _OrdenTrabajoRepository.Get(x => x.idordentrabajo == command.idordentrabajo).LastOrDefault();
                if (dominio == null)
                {
                    dominio = new OrdenTrabajoSeguimiento();
                    dominio.idordentrabajo = command.idordentrabajo.Value;
                    nuevoregistro = true;
                    
                }
            }
            else
            {
                dominio = new OrdenTrabajoSeguimiento();
                dominio.idordentrabajo = command.idordentrabajo.Value;
                nuevoregistro = true;
                
            }

            if(command.fechaarribo.HasValue)
               if(dominio.fechaarribo != command.fechaarribo)
               dominio.fechaarribo = command.fechaarribo;

            if (command.fechaconocimientoembarque.HasValue)
               if(dominio.fechaconocimientoembarque != command.fechaconocimientoembarque)
               dominio.fechaconocimientoembarque = command.fechaconocimientoembarque;

            if (command.fechadesembarque.HasValue)
               if(dominio.fechadesembarque != command.fechadesembarque)
                dominio.fechadesembarque = command.fechadesembarque;

            if(command.fechaembarque.HasValue)
              if(dominio.fechaembarque != command.fechaembarque)
              dominio.fechaembarque = command.fechaembarque;

            if (command.fechallegadaalmacen.HasValue)
                if(dominio.fechallegadaalmacen != command.fechallegadaalmacen)
                  dominio.fechallegadaalmacen = command.fechallegadaalmacen;

            if (command.fechallegadapuerto.HasValue)
                if(dominio.fechallegadapuerto != command.fechallegadapuerto)
               dominio.fechallegadapuerto = command.fechallegadapuerto;

            if (command.fechasalidadepuerto.HasValue)
                if(dominio.fechasalidadepuerto != command.fechasalidadepuerto)
               dominio.fechasalidadepuerto = command.fechasalidadepuerto;

            if (command.numeroconocimiento != null)
              if(dominio.numeroconocimiento != command.numeroconocimiento)
               dominio.numeroconocimiento = command.numeroconocimiento;

            try
            {
                if (nuevoregistro)
                    _OrdenTrabajoRepository.Add(dominio);
                _OrdenTrabajoRepository.Commit();

                return new InsertarActualizarOrdenTrabajoOutput() { idordentrabajo = dominio.idordentrabajo };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}