

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarVehiculoHandler : ICommandHandler<InsertarActualizarVehiculoCommand>
    {
        private readonly IRepository<Vehiculo> _VehiculoRepository;


        public InsertarActualizarVehiculoHandler(IRepository<Vehiculo> pVehiculoRepository)
        {
            this._VehiculoRepository = pVehiculoRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarVehiculoCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una Vehiculo");


            Vehiculo dominio = null;
            if (command.idvehiculo.HasValue)
                dominio = _VehiculoRepository.Get(x => x.idvehiculo == command.idvehiculo).LastOrDefault();
            else
                dominio = new Vehiculo();
           

                switch (command._tipoop)
                {
                    case 1:
                        dominio.idproveedor = command.idproveedor;
                        dominio.idtipo = command.idtipo;
                        dominio.idmarca = command.idmarca;
                        dominio.idmodelo = command.idmodelo;
                        dominio.idanio = command.idanio;
                        dominio.idcolor = command.idcolor;
                        dominio.idcombustible = command.idcombustible;
                        dominio.regmtc = command.regmtc;
                        dominio.placa = command.placa;
                        dominio.confveh = command.confveh;
                        dominio.pesobruto = command.pesobruto;
                        dominio.cargautil = command.cargautil;
                        dominio.seriemotor = command.seriemotor;
                        dominio.kilometraje = command.kilometraje;
                        dominio.idorigen = command.idorigen;
                        if (!command.idvehiculo.HasValue)
                            dominio.idestado = command.idestado;
                        dominio.activo = true;

                        break;
                    case 2:
                        dominio.idestado = command.idestado;
                        dominio.inspecciones = command.inspecciones;
                        dominio.fechainspeccion = command.fechainspeccion;
                        dominio.usuarioinspeccion = command.usuarioinspeccion;
                        break;
                    case 3: //Asignado a una carga
                        dominio.idestado = command.idestado;
                        dominio.fechaasignado = command.fechaasignado;
                        dominio.usuarioasignado = command.usuarioasignado;
                        break;
                    case 4:
                        dominio.idchofer = command.idchofer;
                        break;
                    case 5:
                        dominio.idestado = command.idestado;
                        break;

                    case 6:
                        dominio.activo = false;
                        dominio.idestado = 4;
                        break;

                }
            


                try
                {
                    if (!command.idvehiculo.HasValue)
                        _VehiculoRepository.Add(dominio);
                    _VehiculoRepository.Commit();


                    return new InsertarActualizarVehiculoOutput() { idvehiculo = dominio.idvehiculo };

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
