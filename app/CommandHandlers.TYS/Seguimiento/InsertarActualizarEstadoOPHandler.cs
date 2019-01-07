

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarEstadoOperacionHandler : ICommandHandler<InsertarActualizarEstadoOperacionCommand>
    {
        private readonly IRepository<OperacionCarga> _OperacionCargaRepository;
        
        public InsertarActualizarEstadoOperacionHandler(IRepository<OperacionCarga> pOperacionCargaRepository)
        {
            this._OperacionCargaRepository = pOperacionCargaRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarEstadoOperacionCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");

            OperacionCarga dominio = null;
            if (command.idcarga.HasValue)
               dominio = _OperacionCargaRepository.Get(x => x.idcarga == command.idcarga).LastOrDefault();
            else
               dominio = new OperacionCarga();
               dominio.idestado = command.idestado;

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
