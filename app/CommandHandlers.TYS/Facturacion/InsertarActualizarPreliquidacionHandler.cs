
using CommandHandlers.Common;
using Domain.Common.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using QueryHandlers.Common;
using QueryContracts.Common;



namespace CommandHandlers.TYS
{
    using CommandContracts.TYS.Facturacion;
    using Domain.TYS.Facturacion;
    using CommandContracts.TYS.Facturacion.Output;

    public class InsertarActualizarPreliquidacionHandler :   ICommandHandler<InsertarActualizarPreliquidacionCommand> 
    {

        private readonly IRepository<Preliquidacion> _PreliquidacionRepository;
        private readonly IRepository<Comprobante> _ComprobanteRepository;

        public InsertarActualizarPreliquidacionHandler(IRepository<Preliquidacion> pPreliquidacionRepository,
            IRepository<Comprobante> pComprobanteRepository)
        {
            this._PreliquidacionRepository = pPreliquidacionRepository;
            this._ComprobanteRepository = pComprobanteRepository;
        }


        public CommandContracts.Common.CommandResult Handle(InsertarActualizarPreliquidacionCommand command)
        {
            if (command == null) throw new ArgumentException("Tiene que ingresar una cliente");

            Preliquidacion dominio = null;
            Comprobante dominio_com = null;
            if (command.idpreliquidacion.HasValue)
                dominio = _PreliquidacionRepository.Get(x => x.idpreliquidacion == command.idpreliquidacion).LastOrDefault();
            else
                dominio = new Preliquidacion();


            if (command._tipoop == 1)
            {

                dominio.fecharegistro = command.fecharegistro;
                dominio.idcliente = command.idcliente;
                dominio.idcomprobantepago = command.idcomprobantepago;
                dominio.idusuarioregistro = command.idusuarioregistro;
                dominio.igv = command.igv;
                dominio.numeropreliquidacion = command.numeropreliquidacion;
                dominio.recargo = command.recargo;
                dominio.subtotal = command.subtotal;
                dominio.total = command.total;
                dominio.totalbulto = command.totalbulto;
                dominio.totalpeso = command.totalpeso;
                dominio.totalvolumen = command.totalvolumen;
                dominio.idestado = command.idestado;
            }
            else if(command._tipoop == 2) // desvincular comprobante
            {
                dominio_com = _ComprobanteRepository.Get(x => x.idpreliquidacion == command.idpreliquidacion).LastOrDefault();
                dominio_com.idpreliquidacion = null;

                _ComprobanteRepository.SaveChanges();
                
                    
                dominio.idestado = command.idestado;
                dominio.idcomprobantepago = null;
            }
            else if(command._tipoop == 3)// Generar comprobante
            {
                dominio.idestado = command.idestado;
                dominio.idcomprobantepago = command.idcomprobantepago;
            }
            else if (command._tipoop == 4) //eliminar preliquidacion
            {
                //_PreliquidacionRepository.Delete(dominio);
                //_PreliquidacionRepository.Commit();
                dominio.idestado = command.idestado;
            }
            

            try
            {
                if (!command.idpreliquidacion.HasValue)
                    _PreliquidacionRepository.Add(dominio);
                _PreliquidacionRepository.SaveChanges();


                return new InsertarActualizarPreliquidacionOutput() { idpreliquidacion = dominio.idpreliquidacion };

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
