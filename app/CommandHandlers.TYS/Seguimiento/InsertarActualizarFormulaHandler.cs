

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
namespace CommandHandlers.TYS
{
    public class InsertarActualizarFormulaHandler : ICommandHandler<InsertarActualizarFormulaCommand>
    {
        private readonly IRepository<Formula> _FormulaRepository;


        public InsertarActualizarFormulaHandler(IRepository<Formula> pFormulaRepository)
        {
            this._FormulaRepository = pFormulaRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarFormulaCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");


           Formula  dominio = null;
           if (command.idformula.HasValue)
               dominio = _FormulaRepository.Get(x => x.idformula == command.idformula).LastOrDefault();
            else
               dominio = new Formula();
           if (!command.activo)
           {
               dominio.activo = false;
               if(command.nombre != null)
               {
                   dominio.formula = command.formula;
                   dominio.nombre = command.nombre;
               }
           }
           else
           {
               dominio.formula = command.formula;
               dominio.nombre = command.nombre;
               dominio.activo = true;
           }
            


            try
            {
                if (!command.idformula.HasValue)
                    _FormulaRepository.Add(dominio);
                _FormulaRepository.Commit();


                return new InsertarActualizarFormulaOutput() { idformula = dominio.idformula };

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
