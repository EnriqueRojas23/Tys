

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
    public class InsertarArchivoHandler : ICommandHandler<InsertarArchivoCommand> 
    {

        private readonly IRepository<Archivo> _ArchivoRepository;

        public InsertarArchivoHandler(IRepository<Archivo> pArchivoRepository)
        {
            this._ArchivoRepository = pArchivoRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarArchivoCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");


            Archivo dominio = new Archivo();
            dominio.idordentrabajo = command.idordentrabajo;
            dominio.nombrearchivo = command.nombrearchivo;
            dominio.rutaacceso = command.rutaacceso;
            dominio.extension = command.extension;

            try
            {

                _ArchivoRepository.Add(dominio);
                _ArchivoRepository.SaveChanges();

                return new InsertarArchivoOutput() {  idarchivo = dominio.idarchivo };

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
