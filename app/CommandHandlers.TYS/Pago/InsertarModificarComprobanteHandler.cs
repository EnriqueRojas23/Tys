
using CommandContracts.TYS.Pago;
using CommandContracts.TYS.Pago.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Pago;
using System;
using System.Linq;
namespace CommandHandlers.TYS.Pago
{
    public class InsertarModificarComprobanteHandler : ICommandHandler<InsertarActualizarComprobanteProveedorCommand>
    {
        private readonly IRepository<ComprobanteProveedor> _ComprobanteRepository;


        public InsertarModificarComprobanteHandler(IRepository<ComprobanteProveedor> pComprobanteRepository)
        {
            this._ComprobanteRepository = pComprobanteRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarComprobanteProveedorCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una etapa");


            ComprobanteProveedor dominio = null;
            if (command.idcomprobante.HasValue)
                dominio = _ComprobanteRepository.Get(x => x.idcomprobante == command.idcomprobante).LastOrDefault();
            else
                dominio = new ComprobanteProveedor();
            if (!command.activo)
                dominio.activo = false;
            else dominio.activo = true; 
            if (command.serienumero != null)
            {
                dominio.idetapa = command.idetapa;
                dominio.idmoneda = command.idmoneda;
                dominio.idproveedor = command.idproveedor;
                dominio.idtipocomprobante = command.idtipocomprobante;
                dominio.idtipotransporte = command.idtipotransporte;
                dominio.idorigen = command.idorigen;
                dominio.iddestino = command.iddestino;
                dominio.monto = command.monto;
                dominio.serienumero = command.serienumero;
                dominio.concepto = command.concepto;
                dominio.fecharegistro = DateTime.Now;
                dominio.fechacomprobante = command.fechacomprobante;
                dominio.observacion = command.observacion;
                //dominio.placa = command.placa;
                dominio.idvehiculo = command.idvehiculo;
                dominio.idtipofacturacion = command.idtipofacturacion;
                dominio.actainforme = command.actainforme;

            }
            


            try
            {
                if (!command.idcomprobante.HasValue)
                    _ComprobanteRepository.Add(dominio);
                _ComprobanteRepository.Commit();


                return new InsertarComprobanteOutput() {   idcomprobante = dominio.idcomprobante };

            }
            catch (Exception ex)
            {
                //_ComprobanteRepository.Delete(dominio);
                //_ComprobanteRepository.Commit();
                throw;
            }

        }
    }
}
