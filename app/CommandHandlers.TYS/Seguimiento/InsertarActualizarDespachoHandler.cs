

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarDespachoHandler : ICommandHandler<InsertarActualizarDespachoCommand>
    {
        private readonly IRepository<Despacho> _DespachoRepository;


        public InsertarActualizarDespachoHandler(IRepository<Despacho> pDespachoRepository)
        {
            this._DespachoRepository = pDespachoRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarDespachoCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");


            Despacho dominio = null;
           if (command.iddespacho.HasValue)
               dominio = _DespachoRepository.Get(x => x.iddespacho == command.iddespacho).LastOrDefault();
            else
               dominio = new Despacho();

           switch (command._tipoop)
           {
               case 1:
                   dominio.idvehiculo = command.idvehiculo;
                   dominio.idestado = command.idestado;
                   break;

               case 2:
                   dominio.fechasalida = command.fechasalida;
                   dominio.idusuariosalida = command.idusuariosalida;
                   dominio.idestado = command.idestado;
                   break;
               case 3: // eliminar
                    _DespachoRepository.Delete(dominio);
                    _DespachoRepository.Commit();
                    return new InsertarActualizarDespachoOutput() { iddespacho = -1 };
                    break;
               default:
                   break;
           }
            
               
               
        
    
           
            


            try
            {
                if (!command.iddespacho.HasValue)
                    _DespachoRepository.Add(dominio);
                _DespachoRepository.Commit();


                return new InsertarActualizarDespachoOutput() { iddespacho = dominio.iddespacho };

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
