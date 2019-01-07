

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarDetalleValijaHandler : ICommandHandler<InsertarActualizarDetalleValijaCommand>
    {
        private readonly IRepository<DetalleValija> _DetalleValijaRepository;


        public InsertarActualizarDetalleValijaHandler(IRepository<DetalleValija> pDetalleValijaRepository)
        {
            this._DetalleValijaRepository = pDetalleValijaRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarDetalleValijaCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");


            DetalleValija dominio = null;
            if (command.iddespachovalija.HasValue)
                dominio = _DetalleValijaRepository.Get(x => x.iddespachovalija == command.iddespachovalija).LastOrDefault();
            else
                dominio = new DetalleValija();


            dominio.iddespacho = command.iddespacho;
            dominio.idordentransporte = command.idordentransporte;


            try
            {
                if (!command.iddespachovalija.HasValue)
                    _DetalleValijaRepository.Add(dominio);
                _DetalleValijaRepository.Commit();


                return new InsertarActualizarDetalleValijaOutput() {  iddespachovalija = dominio.iddespachovalija };

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
