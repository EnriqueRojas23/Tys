

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
    public class InsertarActualizarIncidenteHandler : ICommandHandler<InsertarActualizarIncidenteCommand> 
    {

        private readonly IRepository<Incidencia> _IncidenteRepository;

        public InsertarActualizarIncidenteHandler(IRepository<Incidencia> pIncidenteRepository)
        {
            this._IncidenteRepository = pIncidenteRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarIncidenteCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");


            Incidencia dominio = null;
           if (command.idincidencia.HasValue)
               dominio = _IncidenteRepository.Get(x => x.idincidencia == command.idincidencia).LastOrDefault();
            else
               dominio = new Incidencia();

           dominio.descripcion = command.descripcion;
           dominio.fechaincidencia = command.fechaincidencia;
           dominio.fecharegistro = command.fecharegistro;
           dominio.idmaestroincidencia = command.idmaestroincidencia;
           dominio.idordentrabajo = command.idordentrabajo;
           dominio.idusuarioregistro = command.idusuarioregistro;
           dominio.observacion = command.observacion;
           dominio.activo = true;

            try
            {
                if (!command.idincidencia.HasValue)
                    _IncidenteRepository.Add(dominio);
                _IncidenteRepository.SaveChanges();
                
                


                return new InsertarActualizarIncidenteOutput() { idincidencia = dominio.idincidencia };

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
