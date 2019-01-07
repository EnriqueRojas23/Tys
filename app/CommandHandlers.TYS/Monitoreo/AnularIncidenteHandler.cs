

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
    public class AnularIncidenteHandler : ICommandHandler<AnularIncidenteCommand>
    {
        private readonly IRepository<Incidencia> _IncidenteRepository;


        public AnularIncidenteHandler(IRepository<Incidencia> pIncidenteRepository)
        {
            this._IncidenteRepository = pIncidenteRepository;
        }

        public CommandContracts.Common.CommandResult Handle(AnularIncidenteCommand command)
        {
            if (command == null) throw new ArgumentException("Ocurrio un error");

           Incidencia dominio = null;
           if (command.idincidencia.HasValue)
               dominio = _IncidenteRepository.Get(x => x.idincidencia == command.idincidencia).LastOrDefault();
            else
               throw new ArgumentException("Ocurrio un error");

    
           dominio.activo = false;


            try
            {
                _IncidenteRepository.SaveChanges();
                return new InsertarActualizarIncidenteOutput() { idincidencia = dominio.idincidencia };

            }
            catch (Exception ex)
            {
                // _ValortablaRepository.Delete(dominio);
                //_ValortablaRepository.Commit();
                throw;
            }

        }
    }
}
