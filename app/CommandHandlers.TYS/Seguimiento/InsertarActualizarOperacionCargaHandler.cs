

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarOperacionCargaHandler : ICommandHandler<InsertarActualizarOperacionCargaCommand>
    {
        private readonly IRepository<OperacionCarga> _OperacionCargaRepository;
        private readonly IRepository<ServicioOperacion> _ServiciosRepository;


        public InsertarActualizarOperacionCargaHandler(IRepository<OperacionCarga> pOperacionCargaRepository
            , IRepository<ServicioOperacion> pServiciosRepository)
        {
            this._OperacionCargaRepository = pOperacionCargaRepository;
            this._ServiciosRepository = pServiciosRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarOperacionCargaCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");

            OperacionCarga dominio = null;
            if (command.idcarga.HasValue)
                dominio = _OperacionCargaRepository.Get(x => x.idcarga == command.idcarga).LastOrDefault();
            else
               dominio = new OperacionCarga();

            switch (command.tipooperacion)
            {
                case 1:
                     _OperacionCargaRepository.Delete(dominio);
                     _OperacionCargaRepository.Commit();
                     return new InsertarActualizarOperacionCargaOutput() { idcarga = 1 };
                     break;

                case 2: // asignar vehiculo
                        dominio.activo = command.activo;
                        dominio.idvehiculo = command.idvehiculo;
                        dominio.idmuelle = command.idmuelle;
                        break;
                case 3: // Actualizar
                        dominio.numcarga = command.numcarga;
                        dominio.fecharegistro = DateTime.Now;
                        dominio.idplanificador = command.idplanificador;
                        dominio.idproveedor = command.idproveedor;

                        dominio.numcarga = command.numcarga;
                        dominio.observacion = command.observacion;
                        dominio.peso = command.peso;
                        dominio.idvehiculo = command.idvehiculo;

                        dominio.idestado = command.idestado;
                        dominio.idagencia = command.idagencia;
                        dominio.idestacion = command.idestacion;
                        dominio.idruta = command.idruta;
                        dominio.activo = command.activo;
                        dominio.vol = command.vol;
                        dominio.idusuarioregistro = command.idplanificador;
                        break;

                case 4: //Confirmar
                        dominio.fechaconfirmacion = command.fechaconfirmacion;
                        dominio.idestado = command.idestado;
                        break;
                case 5: //Confirmar
                        dominio.fechasalida = command.fechasalida;
                        dominio.idestado = command.idestado;
                        break;

                case 6: //Actualizar
                        dominio.peso = command.peso;
                        dominio.vol = command.vol;
                        break;
                default:
                    break;
            }



            try
            {
                if (!command.idcarga.HasValue)
                    _OperacionCargaRepository.Add(dominio);
                _OperacionCargaRepository.Commit();


                return new InsertarActualizarOperacionCargaOutput() { idcarga = dominio.idcarga };

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
