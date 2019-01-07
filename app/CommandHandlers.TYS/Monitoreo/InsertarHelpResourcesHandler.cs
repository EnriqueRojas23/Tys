

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
using System.Collections.Generic;

using Domain.TYS.Monitoreo;


namespace CommandHandlers.TYS
{
    public class InsertarHelpResourcesHandler : ICommandHandler<InsertarHelpResourcesCommand>
    {
        private readonly IRepository<HelpResources> _HelpResourcesRepository;


        public InsertarHelpResourcesHandler(IRepository<HelpResources> pHelpResourcesRepository)
        {
            this._HelpResourcesRepository = pHelpResourcesRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarHelpResourcesCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");


            HelpResources dominio = null;
           
            dominio = _HelpResourcesRepository.Get(x => x.help == command.help).LastOrDefault();
            if(dominio!=null)return new InsertarHelpResourcesOutput() {  idhelp = dominio.idhelp };
            dominio = new HelpResources();
            dominio.help = command.help;
            dominio.idcampo = command.idcampo;
            


            try
            {
                _HelpResourcesRepository.Add(dominio);
                _HelpResourcesRepository.SaveChanges();

                return new InsertarHelpResourcesOutput() { idhelp = dominio.idhelp };

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
