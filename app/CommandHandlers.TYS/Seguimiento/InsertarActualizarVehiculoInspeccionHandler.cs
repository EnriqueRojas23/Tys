

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarVehiculoInspeccionHandler : ICommandHandler<InsertarActualizarVehiculoInspeccionCommand>
    {
        private readonly IRepository<VehiculoInspeccion> _VehiculoInspeccionRepository;


        public InsertarActualizarVehiculoInspeccionHandler(IRepository<VehiculoInspeccion> pVehiculoInspeccionRepository)
        {
            this._VehiculoInspeccionRepository = pVehiculoInspeccionRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarVehiculoInspeccionCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una etapa");


            VehiculoInspeccion dominio = null;
            if (command.idvehiculoinspeccion.HasValue)
                dominio = _VehiculoInspeccionRepository.Get(x => x.idvehiculoinspeccion == command.idvehiculoinspeccion).LastOrDefault();
            else
                dominio = new VehiculoInspeccion();

            dominio.checkeado = command.checkeado;
            dominio.idinspeccion = command.idinspeccion;
            dominio.idvehiculo = command.idvehiculo;
            

            try
            {
                if (!command.idvehiculoinspeccion.HasValue)
                    _VehiculoInspeccionRepository.Add(dominio);
                _VehiculoInspeccionRepository.Commit();


                return new InsertarActualizarVehiculoOutput() {  idvehiculo = dominio.idvehiculoinspeccion };

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
