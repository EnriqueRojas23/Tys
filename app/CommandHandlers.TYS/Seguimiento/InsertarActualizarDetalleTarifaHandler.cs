

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarDetalleTarifaHandler : ICommandHandler<InsertarActualizarDetalleTarifaCommand>
    {
        private readonly IRepository<DetalleTarifa> _DetalleTarifaRepository;


        public InsertarActualizarDetalleTarifaHandler(IRepository<DetalleTarifa> pDetalleTarifaRepository)
        {
            this._DetalleTarifaRepository = pDetalleTarifaRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarDetalleTarifaCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");


           


            DetalleTarifa dominio = null;
           if (command.iddetalletarifa.HasValue)
               dominio = _DetalleTarifaRepository.Get(x => x.iddetalletarifa == command.iddetalletarifa).LastOrDefault();
            else
               dominio = new DetalleTarifa();

           dominio.idconceptocobro = command.idconceptocobro;
           dominio.idtarifa = command.idtarifa;

            try
            {
                if (!command.iddetalletarifa.HasValue)
                    _DetalleTarifaRepository.Add(dominio);
                _DetalleTarifaRepository.Commit();


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
