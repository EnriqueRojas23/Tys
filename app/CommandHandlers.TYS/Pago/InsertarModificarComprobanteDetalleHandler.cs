
using CommandContracts.TYS.Pago;
using CommandContracts.TYS.Pago.Output;
using CommandHandlers.Common;
using Domain.Common.Contracts;
using Domain.TYS.Pago;
using System;
using System.Linq;
namespace CommandHandlers.TYS.Pago
{
    public class InsertarModificarComprobanteDetalleHandler : ICommandHandler<InsertarActualizarComprobanteDetalleCommand>
    {
        private readonly IRepository<ComprobanteProveedorDetalle> _ComprobanteDetalleRepository;


        public InsertarModificarComprobanteDetalleHandler(IRepository<ComprobanteProveedorDetalle> pComprobanteDetalleRepository)
        {
            this._ComprobanteDetalleRepository = pComprobanteDetalleRepository;
        }

        public CommandContracts.Common.CommandResult Handle(InsertarActualizarComprobanteDetalleCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una etapa");

            System.Collections.Generic.List<ComprobanteProveedorDetalle> dominioedit = null;

            if (command.alledit)
            {
                dominioedit = _ComprobanteDetalleRepository.Get(x => x.idcomprobante == command.idcomprobante).ToList();
                foreach (var item in dominioedit)
                {
                    _ComprobanteDetalleRepository.Delete(item);
                    _ComprobanteDetalleRepository.Commit();
                }
                return new InsertarComprobanteOutput() { idcomprobante = 1 };
            }
            else
            {


                ComprobanteProveedorDetalle dominio = null;
                if (command.PKID.HasValue)
                    dominio = _ComprobanteDetalleRepository.Get(x => x.PKID == command.PKID).LastOrDefault();
                else
                    dominio = new ComprobanteProveedorDetalle();




                if (dominio == null) dominio = new ComprobanteProveedorDetalle();
                else command.idcomprobantedetalle = dominio.idcomprobantedetalle;

                dominio.idcomprobante = command.idcomprobante;
                dominio.numcp = command.numcp;
                dominio.PKID = command.PKID.Value;
                dominio.Precio = command.Precio;
                dominio.SubTotal = command.SubTotal;
                dominio.Total = command.Total;
                dominio.TotalBultos = command.TotalBultos;
                dominio.TotalPeso = command.TotalPeso;
                dominio.ValorVenta = command.ValorVenta;
                dominio.nroguia = command.nroguia;
                dominio.manifiesto = command.manifiesto;


                try
                {

                    if (!command.idcomprobantedetalle.HasValue)
                        _ComprobanteDetalleRepository.Add(dominio);
                    _ComprobanteDetalleRepository.Commit();


                    return new InsertarComprobanteOutput() { idcomprobante = dominio.idcomprobante };

                }
                catch (Exception ex)
                {
                    //_ComprobanteDetalleRepository.Delete(dominio);
                    //_ComprobanteDetalleRepository.Commit();
                    throw;
                }
            }

        } 
    }
}
