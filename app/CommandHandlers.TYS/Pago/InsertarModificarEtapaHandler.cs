
using CommandContracts.TYS.Pago;
using CommandContracts.TYS.Pago.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Pago;
using System;
using System.Linq;
namespace CommandHandlers.TYS.Pago
{
    public class InsertarModificarEtapaHandler : ICommandHandler<InsertarActualizarEtapaCommand>
    {
        private readonly IRepository<EtapaProveedor> _EtapaRepository;


        public InsertarModificarEtapaHandler(IRepository<EtapaProveedor> pEtapaRepository)
        {
            this._EtapaRepository = pEtapaRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarEtapaCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una etapa");


            EtapaProveedor dominio = null;
            if (command.idetapa.HasValue)
                dominio = _EtapaRepository.Get(x => x.idetapa == command.idetapa).LastOrDefault();
            else
                dominio = new EtapaProveedor();
            if (!command.activo)
                dominio.activo = false;
            if (command.etapa != null)
            {
                dominio.etapa = command.etapa;
                dominio.requiereot = command.requiereot;
                dominio.activo = command.activo;
            }
            

            try
            {
                if (!command.idetapa.HasValue)
                    _EtapaRepository.Add(dominio);
                _EtapaRepository.Commit();


                return new InsertarEtapaOutput() {  idetapa = dominio.idetapa };

            }
            catch (Exception ex)
            {
                //_EtapaRepository.Delete(dominio);
                //_EtapaRepository.Commit();
                throw;
            }

        }
    }
}
