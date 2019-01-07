

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
    public class EliminarTarifaHandler : ICommandHandler<EliminarTarifaCommand>
    {
        private readonly IRepository<Tarifa> _TarifaRepository;
        private readonly IRepository<DetalleTarifa> _DetalleTarifaRepository;


        public EliminarTarifaHandler(IRepository<Tarifa> pTarifaRepository, IRepository<DetalleTarifa> pDetalleTarifaRepository)
        {
            this._TarifaRepository = pTarifaRepository;
            this._DetalleTarifaRepository = pDetalleTarifaRepository;
        }

        public CommandContracts.Common.CommandResult Handle(EliminarTarifaCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una Ruta");


            List<DetalleTarifa> dominioDet = null;
            dominioDet = _DetalleTarifaRepository.Get(x => x.idtarifa.Equals(command.idtarifa)).ToList();

            foreach (var item in dominioDet)
            {
                _DetalleTarifaRepository.Delete(item);
                _DetalleTarifaRepository.Commit();
            }


            Tarifa dominio = null;
            dominio = _TarifaRepository.Get(x => x.idtarifa == command.idtarifa).LastOrDefault();
            try
            {
                _TarifaRepository.Delete(dominio);
                _TarifaRepository.Commit();
                return new InsertarActualizarTarifaOutput() { idtarifa = dominio.idtarifa };

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
