

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
using System.Collections.Generic;

using Domain.TYS.Monitoreo;


namespace CommandHandlers.TYS.Monitoreo
{
    public class EliminarDetalleEmbarqueHandler : ICommandHandler<EliminarDetalleEmbarqueCommand> 
    {

        private readonly IRepository<DetalleEmbarque> _DetalleEmbarqueRepository;

        public EliminarDetalleEmbarqueHandler(IRepository<DetalleEmbarque> pDetalleEmbarqueRepository)
        {
            this._DetalleEmbarqueRepository = pDetalleEmbarqueRepository;
        }

        public CommandContracts.Common.CommandResult Handle(EliminarDetalleEmbarqueCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");

            DetalleEmbarque dominio = null;
            dominio = _DetalleEmbarqueRepository.Get(x => x.idordentrabajo == command.idordentrabajo).LastOrDefault();

            try
            {
            
                _DetalleEmbarqueRepository.Delete(dominio);
                _DetalleEmbarqueRepository.SaveChanges();
                return new ActualizarDetalleEmbarqueOutput() { iddetalleembarque = dominio.iddetalleembarque };

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
