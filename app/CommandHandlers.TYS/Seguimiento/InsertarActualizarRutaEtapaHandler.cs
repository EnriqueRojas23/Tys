

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarRutaEtapaHandler : ICommandHandler<InsertarActualizarRutaEtapaCommand>
    {
        private readonly IRepository<RutaEtapa> _RutaEtapaRepository;


        public InsertarActualizarRutaEtapaHandler(IRepository<RutaEtapa> pRutaEtapaRepository)
        {
            this._RutaEtapaRepository = pRutaEtapaRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarRutaEtapaCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una Ruta");


            RutaEtapa dominio = null;
           if (command.idrutasetapas.HasValue)
               dominio = _RutaEtapaRepository.Get(x => x.idrutasetapas == command.idrutasetapas).LastOrDefault();
            else
               dominio = new RutaEtapa();

           dominio.iddetalleruta = command.iddetalleruta;
           dominio.idetapa = command.idetapa;
            
            


            try
            {
                if (!command.idrutasetapas.HasValue)
                    _RutaEtapaRepository.Add(dominio);
                _RutaEtapaRepository.Commit();


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
