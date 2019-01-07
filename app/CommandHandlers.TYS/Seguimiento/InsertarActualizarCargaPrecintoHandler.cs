

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarCargaPrecintoHandler : ICommandHandler<InsertarActualizarCargaPrecintoCommand>
    {
        private readonly IRepository<CargaPrecinto> _CargaPrecintoRepository;


        public InsertarActualizarCargaPrecintoHandler(IRepository<CargaPrecinto> pCargaPrecintoRepository)
        {
            this._CargaPrecintoRepository = pCargaPrecintoRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarCargaPrecintoCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una Chofer");

            var multiple = _CargaPrecintoRepository.Get(x=>x.iddespacho.Value.Equals(command.iddespacho.Value)).ToList();
            if (command.eliminar)
            {
                foreach (var item in multiple)
                {
                    _CargaPrecintoRepository.Delete(item);
                    _CargaPrecintoRepository.Commit();
                }
                return new InsertarActualizarCargaPrecintoOutput() { idcargaprecinto = 1 };
            }


            CargaPrecinto dominio = null;
            if (command.idcargaprecinto.HasValue)
               dominio = _CargaPrecintoRepository.Get(x => x.idcargaprecinto == command.idcargaprecinto).LastOrDefault();
            else
               dominio = new CargaPrecinto();
           
                
           dominio.iddespacho = command.iddespacho;
           dominio.idprecinto = command.idprecinto;

            try
            {
                if (!command.idcargaprecinto.HasValue)
                        _CargaPrecintoRepository.Add(dominio);
                _CargaPrecintoRepository.Commit();


                return new InsertarActualizarCargaPrecintoOutput() {  idcargaprecinto = dominio.idcargaprecinto };

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
