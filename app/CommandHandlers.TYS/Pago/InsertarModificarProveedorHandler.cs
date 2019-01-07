
using CommandContracts.TYS.Pago.Output;
using CommandContracts.TYS.Pago;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Pago;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandHandlers.TYS.Facturacion.Pago
{
    public class InsertarModificarProveedorHandler : ICommandHandler<InsertarActualizarProveedorCommand>
    {
        private readonly IRepository<Proveedor> _ProveedorRepository;

        public InsertarModificarProveedorHandler(IRepository<Proveedor> pProveedorRepository)
        {
            this._ProveedorRepository = pProveedorRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarProveedorCommand command)
        {
            Proveedor dominio = null;
            if (command.idproveedor.HasValue)
                dominio = _ProveedorRepository.Get(x => x.idproveedor == command.idproveedor).LastOrDefault();
            else
                dominio = new Proveedor();
            if (!command.activo)
                dominio.activo = command.activo;
            if (command.razonsocial != null)
            {
                dominio.razonsocial = command.razonsocial;
                dominio.placaasociada = command.placaasociada;
                dominio.ruc = command.ruc;
                dominio.activo = command.activo;
                dominio.direccion = command.direccion;
            }
            
            
            try
            {
                if (!command.idproveedor.HasValue)
                  _ProveedorRepository.Add(dominio);
                _ProveedorRepository.Commit();


                return new InsertarProveedorOutput() {   idproveedor = dominio.idproveedor };

            }
            catch (Exception ex) {
           //     _ProveedorRepository.Delete(dominio);
                _ProveedorRepository.Commit();
                throw;
            }

        }
    }
}
