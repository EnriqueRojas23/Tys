

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarServicioOperacionHandler : ICommandHandler<InsertarActualizarServicioOperacionCommand>
    {
        private readonly IRepository<ServicioOperacion> _ServicioOperacionRepository;


        public InsertarActualizarServicioOperacionHandler(IRepository<ServicioOperacion> pServicioOperacionRepository)
        {
            this._ServicioOperacionRepository = pServicioOperacionRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarServicioOperacionCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una etapa");


            ServicioOperacion dominio = null;
            if (command.idserviciooperacion.HasValue)
                dominio = _ServicioOperacionRepository.Get(x => x.idserviciooperacion == command.idserviciooperacion).LastOrDefault();
            else
                dominio = new ServicioOperacion();

            dominio.idservicio = command.idservicio;
            dominio.idcarga = command.idcarga;
            dominio.cantidad = command.cantidad;

            try
            {
                if (!command.idserviciooperacion.HasValue)
                    _ServicioOperacionRepository.Add(dominio);
                else
                {
                     _ServicioOperacionRepository.Delete(dominio);
                    _ServicioOperacionRepository.Commit();
             
                }

                return new InsertarActualizarServicioOperacionOutput() { idserviciooperacion = dominio.idserviciooperacion };

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
