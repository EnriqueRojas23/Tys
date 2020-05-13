using System;
using System.Linq;
using CommandContracts.Common;
using CommandContracts.TYS.Seguridad;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguridad;
using CommandContracts.TYS.Seguridad.Output;


namespace CommandHandlers.TYS.Seguridad
{
    public class InsertarModificarAlertaHandler : ICommandHandler<InsertarModificarAlertaCommand>
    {
        private readonly IRepository<Alerta> _alerta;


        public InsertarModificarAlertaHandler(IRepository<Alerta> pAlerta)
        {

            this._alerta = pAlerta;
        }

        public CommandResult Handle(InsertarModificarAlertaCommand command)
        {
            Alerta dominio = null;
            if (command.idalerta.HasValue) {
                dominio = _alerta.Get(x => x.idalerta == command.idalerta).LastOrDefault();
            }
            if (dominio == null) dominio = new Alerta();


            dominio.estados = command.estados;
            dominio.idperiodicidad = command.idperiodicidad;
            dominio.usr_int_id = command.usr_int_id;
            dominio.idmedio = command.idmedio;



            if (!command.idalerta.HasValue)
            {
                _alerta.Add(dominio);
            }
            _alerta.Commit();

            return new InsertarModificarAlertaOutput() { idalerta = dominio.idalerta }; 
        }
    }
}
