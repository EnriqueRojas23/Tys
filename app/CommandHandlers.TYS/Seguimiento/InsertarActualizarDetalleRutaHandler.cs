

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarDetalleRutaHandler : ICommandHandler<InsertarActualizarDetalleRutaCommand>
    {
        private readonly IRepository<DetalleRuta> _DetalleRutaRepository;


        public InsertarActualizarDetalleRutaHandler(IRepository<DetalleRuta> pDetalleRutaRepository)
        {
            this._DetalleRutaRepository = pDetalleRutaRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarDetalleRutaCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una Ruta");


            DetalleRuta dominio = null;
           if (command.iddetalleruta.HasValue)
               dominio = _DetalleRutaRepository.Get(x => x.iddetalleruta == command.iddetalleruta).LastOrDefault();
            else
               dominio = new DetalleRuta();

           dominio.etapas = command.etapas;
           dominio.idorigen = command.idorigen;
           dominio.km = command.km;
           dominio.iddistrito = command.iddistrito;
           dominio.idorden = command.idorden;
           dominio.idruta = command.idruta;
           dominio.idtipotransporte = command.idtipotransporte;
           dominio.leaddocumentario = command.leaddocumentario;
           dominio.leadida = command.leadida;
           dominio.leadretorno = command.leadretorno;
           dominio.iddepartamento = command.iddepartamento;
           dominio.idprovincia = command.idprovincia;
            
            


            try
            {
                if (!command.iddetalleruta.HasValue)
                    _DetalleRutaRepository.Add(dominio);
                _DetalleRutaRepository.Commit();


                return new InsertarActualizarDetalleRutaOutput() { iddetalleruta = dominio.iddetalleruta };

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
