

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Collections.Generic;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class ActualizarGuiaOrdenHandler : ICommandHandler<ActualizarGuiaOrdenCommand>
    {
        private readonly IRepository<OrdenTrabajo> _ActualizarGuiaOrdenRepository;


        public ActualizarGuiaOrdenHandler(IRepository<OrdenTrabajo> pActualizarGuiaOrdenRepository)
        {
            this._ActualizarGuiaOrdenRepository = pActualizarGuiaOrdenRepository;
        }

        public CommandContracts.Common.CommandResult Handle(ActualizarGuiaOrdenCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que seleccionar una orden");

            
            //Actualiza la guia de remision transportista
            OrdenTrabajo orden = null;
            orden = _ActualizarGuiaOrdenRepository.Get(x => x.idordentrabajo == command.idordentransporte).LastOrDefault();
            orden.guiatransportista = command.guiatransportista;

            try
            {
                _ActualizarGuiaOrdenRepository.SaveChanges();
                return new TransbordarVehiculoOutput() {  idvehiculo = 1 };

            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
