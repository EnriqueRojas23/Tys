

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
    public class InsertarDetalleEtapaHandler : ICommandHandler<InsertarDetalleEtapaCommand> 
    {

        private readonly IRepository<DetalleEtapa> _EtapaRepository;

        public InsertarDetalleEtapaHandler(IRepository<DetalleEtapa> pEtapaRepository)
        {
            this._EtapaRepository = pEtapaRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarDetalleEtapaCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");

            DetalleEtapa dominio = null;
            dominio = new DetalleEtapa();


            dominio.cantidad = command.cantidad;
            dominio.idguiaremision = command.idguiaremision;
            dominio.idetapa = command.idetapa;

            try
            {
                if (!command.iddetalleetapa.HasValue)
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
