

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
    public class InsertarEtapaHandler : ICommandHandler<InsertarEtapaCommand> 
    {

        private readonly IRepository<Etapa> _EtapaRepository;

        public InsertarEtapaHandler(IRepository<Etapa> pEtapaRepository)
        {
            this._EtapaRepository = pEtapaRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarEtapaCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");

            Etapa dominio = null;
           dominio = new Etapa();
            

           dominio.descripcion = command.descripcion;
           dominio.fechaetapa = command.fechaetapa;
           dominio.fecharegistro = command.fecharegistro;
           dominio.idmanifiesto = command.idmanifiesto;
           dominio.idusuarioregistro = command.idusuarioregistro;
           dominio.recurso = command.recurso;
           dominio.documento = command.documento;
           dominio.visible = command.visible;
           dominio.idmaestroetapa = command.idmaestroetapa;

           dominio.idordentrabajo = command.idordentrabajo;

            try
            {
                if (!command.idetapa.HasValue)
                    _EtapaRepository.Add(dominio);
                _EtapaRepository.SaveChanges();



                return new InsertarEtapaOutput() { idetapa = dominio.idetapa };

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
