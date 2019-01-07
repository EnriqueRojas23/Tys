
using CommandContracts.TYS.Pago;
using CommandContracts.TYS.Pago.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Pago;
using System;
using System.Linq;
namespace CommandHandlers.TYS.Pago
{
    public class InsertarModificarAsignacionHandler : ICommandHandler<InsertarActualizarAsignacionCommand>
    {
        private readonly IRepository<Asignacion> _AsinacionRepository;


        public InsertarModificarAsignacionHandler(IRepository<Asignacion> pAsignacionRepository)
        {
            this._AsinacionRepository = pAsignacionRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarAsignacionCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una etapa");


            Asignacion  dominio = null;
            if (command.idasignacion.HasValue)
            {
                dominio = _AsinacionRepository.Get(x => x.idasignacion == command.idasignacion).LastOrDefault();
                if(dominio == null)
                    dominio = new Asignacion();
            }

            else
                dominio = new Asignacion();
            dominio.idetapa = command.idetapa;
            dominio.idmoneda = command.idmoneda;
            dominio.idproveedor = command.idproveedor;
            dominio.idtipocomprobante = command.idtipocomprobante;
            dominio.idtipotransporte = command.idtipotransporte;


            try
            {
                if (!command.idasignacion.HasValue)
                    _AsinacionRepository.Add(dominio);
                _AsinacionRepository.Commit();


                return new InsertarAsignacionOutput() {   idasignacion  = dominio.idasignacion};

            }
            catch (Exception ex)
            {
                _AsinacionRepository.Delete(dominio);
                _AsinacionRepository.Commit();
                throw;
            }

        }
    }
}
