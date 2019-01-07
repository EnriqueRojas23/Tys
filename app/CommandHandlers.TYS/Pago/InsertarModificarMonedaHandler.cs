
using CommandContracts.TYS.Pago;
using CommandContracts.TYS.Pago.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Pago;
using System;
using System.Linq;
namespace CommandHandlers.TYS.Pago
{
    public class InsertarModificarMonedaHandler : ICommandHandler<InsertarActualizarMonedaCommand>
    {
        private readonly IRepository<Moneda> _MonedaRepository;


        public InsertarModificarMonedaHandler(IRepository<Moneda> pMonedaRepository)
        {
            this._MonedaRepository = pMonedaRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarMonedaCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una etapa");


            Moneda dominio = null;
            if (command.idmoneda.HasValue)
                dominio = _MonedaRepository.Get(x => x.idmoneda == command.idmoneda).LastOrDefault();
            else
                dominio = new Moneda();
            if (command.activo)
            {
                dominio.activo = command.activo;
            }
            else
            {
                dominio.moneda = command.moneda;
            }


            try
            {
                if (!command.idmoneda.HasValue)
                    _MonedaRepository.Add(dominio);
                _MonedaRepository.Commit();


                return new InsertarMonedaOutput() {   idmoneda = dominio.idmoneda };

            }
            catch (Exception ex)
            {
                _MonedaRepository.Delete(dominio);
                _MonedaRepository.Commit();
                throw;
            }

        }
    }
}
