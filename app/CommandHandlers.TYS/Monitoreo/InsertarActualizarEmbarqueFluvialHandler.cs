

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
    public class InsertarActualizarEmbarqueFluvialHandler : ICommandHandler<InsertarActualizarEmbarqueFluvialCommand> 
    {

        private readonly IRepository<Embarque> _EmbarqueRepository;

        public InsertarActualizarEmbarqueFluvialHandler(IRepository<Embarque> pEmbarqueRepository)
        {
            this._EmbarqueRepository = pEmbarqueRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarEmbarqueFluvialCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");

            Embarque dominio = null;
            if (command.idembarque.HasValue)
                dominio = _EmbarqueRepository.Get(x => x.idembarque == command.idembarque).LastOrDefault();
            else
                dominio = new Embarque();

            if (!command.idembarque.HasValue)
            {
                dominio.idusuarioregistro = command.idusuarioregistro;
                dominio.fecharegistro = command.fecharegistro;
                dominio.numeroembarque = command.numeroembarque;
            }
            dominio.conocimientoembarque = command.conocimientoembarque;
            dominio.embarquecompleto = command.embarquecompleto;
            dominio.fechaatraque = command.fechaatraque;
            dominio.fechafincarga = command.fechafincarga;
            dominio.fechainiciocarga = command.fechainiciocarga;
            dominio.fechallegada = command.fechallegada;
            dominio.fechazarpe = command.fechazarpe;
            dominio.idpuerto = command.idpuerto;
            dominio.idtransporte = command.idtransporte;
        
            

            try
            {
                if (!command.idembarque.HasValue)
                    _EmbarqueRepository.Add(dominio);
                _EmbarqueRepository.SaveChanges();



                return new InsertarActualizarEmbarqueFluvialOutput() { idembarque = dominio.idembarque };

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
