

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
    public class ActualizarDetalleEmbarqueHandler : ICommandHandler<ActualizarDetalleEmbarqueCommand> 
    {

        private readonly IRepository<DetalleEmbarque> _DetalleEmbarqueRepository;

        public ActualizarDetalleEmbarqueHandler(IRepository<DetalleEmbarque> pDetalleEmbarqueRepository)
        {
            this._DetalleEmbarqueRepository = pDetalleEmbarqueRepository;
        }

        public CommandContracts.Common.CommandResult Handle(ActualizarDetalleEmbarqueCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");

            DetalleEmbarque dominio = null;
            dominio = _DetalleEmbarqueRepository.Get(x => x.idordentrabajo == command.idordentrabajo).LastOrDefault();
            dominio.fechacontrolsunat = command.fechacontrolsunat;
            dominio.fechadescarga = command.fechadescarga;
            dominio.idordentrabajo = command.idordentrabajo;
            dominio.embarquecompleto = command.embarquecompleto;
            dominio.idusuariodescarga = command.idusuariodescarga;
            dominio.idpuerto = command.idpuerto;
          

            try
            {
            
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
