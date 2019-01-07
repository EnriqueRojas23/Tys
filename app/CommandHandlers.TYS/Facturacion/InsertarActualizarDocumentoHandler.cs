
using CommandHandlers.Common;
using Domain.Common.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using QueryHandlers.Common;
using QueryContracts.Common;

namespace CommandHandlers.TYS.Facturacion
{
    using CommandContracts.TYS.Facturacion;
    using CommandContracts.TYS.Facturacion.Output;
    using Domain.TYS.Facturacion;

    public class InsertarActualizarDocumentoHandler :   ICommandHandler<InsertarActualizarDocumentoCommand> 
    {
        private readonly IRepository<Documento> _OrdenTrabajoRepository;
        
        public InsertarActualizarDocumentoHandler(IRepository<Documento> pOrdenTrabajoRepository)
        {
            this._OrdenTrabajoRepository = pOrdenTrabajoRepository;
        }
        public CommandContracts.Common.CommandResult Handle(InsertarActualizarDocumentoCommand command)
        {
            Documento dominio = null;
            if (command.idnumerodocumento.HasValue)
                dominio = _OrdenTrabajoRepository.Get(x => x.idnumerodocumento == command.idnumerodocumento).LastOrDefault();
            else
                dominio = new Documento();

            if (command._tipooperacion == 1)
            {
                dominio.ultimonumero = command.ultimonumero;
            }
            else
            {
                dominio.idestacion = command.idestacion;
                dominio.idtipocomprobante = command.idtipocomprobante;
                dominio.idusuarioautorizado = command.idusuarioautorizado;
                dominio.primernumero = command.primernumero;
                dominio.serie = command.serie;
                dominio.ultimonumero = command.ultimonumero;
            }
           
            try
            {
                if (!command.idnumerodocumento.HasValue)
                    _OrdenTrabajoRepository.Add(dominio);
                _OrdenTrabajoRepository.SaveChanges();
                return new InsertarActualizarDocumentoOutput() {   idnumerodocumento = dominio.idnumerodocumento };

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
