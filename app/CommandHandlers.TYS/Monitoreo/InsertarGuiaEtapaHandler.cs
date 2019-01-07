

using CommandContracts.TYS.Seguimiento;
using CommandContracts.TYS.Seguimiento.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Seguimiento;
using System;
using System.Linq;
using System.Collections.Generic;

using Domain.TYS.Monitoreo;


namespace CommandHandlers.TYS.Monitoreo
{
    public class InsertarGuiaEtapaHandler : ICommandHandler<InsertarGuiaEtapaCommand> 
    {

        private readonly IRepository<GuiaEtapa> _GuiaEtapaRepository;

        public InsertarGuiaEtapaHandler(IRepository<GuiaEtapa> pGuiaEtapaRepository)
        {
            this._GuiaEtapaRepository = pGuiaEtapaRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarGuiaEtapaCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");

           GuiaEtapa dominio = null;
           dominio = new GuiaEtapa();
            

           dominio.cantidad = command.cantidad;
           dominio.idguiaremisioncliente = command.idguiaremisioncliente;
           dominio.idordentrabajo = command.idordentrabajo;   


            try
            {
                _GuiaEtapaRepository.Add(dominio);
                _GuiaEtapaRepository.SaveChanges();



                return new InsertarEtapaOutput() {  idetapa = dominio.idordentrabajo };

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
